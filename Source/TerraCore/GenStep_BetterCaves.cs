using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace TerraCore;

public class GenStep_BetterCaves : GenStep
{
    private const float DirectionNoiseFrequency = 0.00205f;

    private const float TunnelWidthNoiseFrequency = 0.02f;

    private static readonly SimpleCurve TunnelsWidthPerRockCount =
    [
        new CurvePoint(300f, 2.5f),
        new CurvePoint(600f, 4.2f),
        new CurvePoint(6000f, 9.5f),
        new CurvePoint(30000f, 15.5f)
    ];

    private static readonly HashSet<IntVec3> groupSet = [];

    private static readonly HashSet<IntVec3> groupVisited = [];

    private static readonly List<IntVec3> subGroup = [];

    private static readonly List<IntVec3> tmpCells = [];

    private static readonly HashSet<IntVec3> tmpGroupSet = [];

    private ModuleBase directionNoise;

    private ModExt_Biome_GenStep_BetterCaves extCaves;

    private ModuleBase tunnelWidthNoise;

    public override int SeedPart => 235321433;

    public override void Generate(Map map, GenStepParams parms)
    {
        if (!Find.World.HasCaves(map.Tile))
        {
            return;
        }

        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
        if (modExtension == null)
        {
            return;
        }

        extCaves = modExtension;
        directionNoise = new Perlin(0.002050000010058284, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium);
        tunnelWidthNoise = new Perlin(0.019999999552965164, 2.0, 0.5, 4, Rand.Int, QualityMode.Medium);
        tunnelWidthNoise = new ScaleBias(0.4, 1.0, tunnelWidthNoise);
        var elevation = MapGenerator.Elevation;
        var visited = new BoolGrid(map);
        var rockCells = new List<IntVec3>();
        foreach (var allCell in map.AllCells)
        {
            if (visited[allCell] || !IsRock(allCell, elevation, map))
            {
                continue;
            }

            rockCells.Clear();
            map.floodFiller.FloodFill(allCell, x => IsRock(x, elevation, map), delegate(IntVec3 x)
            {
                visited[x] = true;
                rockCells.Add(x);
            });
            Trim(rockCells, map);
            RemoveSmallDisconnectedSubGroups(rockCells, map);
            if (rockCells.Count >= modExtension.minRocksToGenerateAnyTunnel)
            {
                StartWithTunnel(rockCells, map);
            }
        }

        SmoothGenerated(map);
    }

    private void Trim(List<IntVec3> group, Map map)
    {
        GenMorphology.Open(group, 6, map);
    }

    private bool IsRock(IntVec3 c, MapGenFloatGrid elevation, Map map)
    {
        return c.InBounds(map) && elevation[c] > 0.7f;
    }

    private void StartWithTunnel(List<IntVec3> group, Map map)
    {
        var a = GenMath.RoundRandom(group.Count * extCaves.startTunnelsPer10k / 10000f);
        a = Mathf.Min(a, extCaves.maxStartTunnelsPerRockGroup);
        var num = TunnelsWidthPerRockCount.Evaluate(group.Count);
        for (var i = 0; i < a; i++)
        {
            var start = IntVec3.Invalid;
            var num2 = -1f;
            var dir = -1f;
            var num3 = -1f;
            for (var j = 0; j < 10; j++)
            {
                var intVec = FindRandomEdgeCellForTunnel(group, map);
                var distToCave = GetDistToCave(intVec, group, map, 40f, false);
                var num4 = FindBestInitialDir(intVec, group, out var dist);
                if (start.IsValid && !(distToCave > num2) && (distToCave != num2 || !(dist > num3)))
                {
                    continue;
                }

                start = intVec;
                num2 = distToCave;
                dir = num4;
                num3 = dist;
            }

            var width = num * Rand.Range(extCaves.tunnelStartWidthFactorMin, extCaves.tunnelStartWidthFactorMax);
            Dig(start, dir, width, group, map, BranchType.Normal, 1);
        }
    }

    private IntVec3 FindRandomEdgeCellForTunnel(List<IntVec3> group, Map map)
    {
        var caves = MapGenerator.Caves;
        var cardinalDirections = GenAdj.CardinalDirections;
        tmpCells.Clear();
        tmpGroupSet.Clear();
        tmpGroupSet.AddRange(group);
        for (var i = 0; i < group.Count; i++)
        {
            if (group[i].DistanceToEdge(map) < 3 || !(caves[group[i]] <= 0f))
            {
                continue;
            }

            for (var j = 0; j < 4; j++)
            {
                var item = group[i] + cardinalDirections[j];
                if (tmpGroupSet.Contains(item))
                {
                    continue;
                }

                tmpCells.Add(group[i]);
                break;
            }
        }

        if (tmpCells.Any())
        {
            return tmpCells.RandomElement();
        }

        Log.Warning("Could not find any valid edge cell.");
        return group.RandomElement();
    }

    private float FindBestInitialDir(IntVec3 start, List<IntVec3> group, out float dist)
    {
        float num = GetDistToNonRock(start, group, IntVec3.East, 40);
        float num2 = GetDistToNonRock(start, group, IntVec3.West, 40);
        float num3 = GetDistToNonRock(start, group, IntVec3.South, 40);
        float num4 = GetDistToNonRock(start, group, IntVec3.North, 40);
        float num5 = GetDistToNonRock(start, group, IntVec3.NorthWest, 40);
        float num6 = GetDistToNonRock(start, group, IntVec3.NorthEast, 40);
        float num7 = GetDistToNonRock(start, group, IntVec3.SouthWest, 40);
        float num8 = GetDistToNonRock(start, group, IntVec3.SouthEast, 40);
        dist = Mathf.Max(num, num2, num3, num4, num5, num6, num7, num8);
        return GenMath.MaxByRandomIfEqual(0f, num + (num8 / 2f) + (num6 / 2f), 45f, num8 + (num3 / 2f) + (num / 2f),
            90f, num3 + (num8 / 2f) + (num7 / 2f), 135f, num7 + (num3 / 2f) + (num2 / 2f), 180f,
            num2 + (num7 / 2f) + (num5 / 2f), 225f, num5 + (num4 / 2f) + (num2 / 2f), 270f,
            num4 + (num6 / 2f) + (num5 / 2f), 315f, num6 + (num4 / 2f) + (num / 2f));
    }

    private void Dig(IntVec3 start, float dir, float width, List<IntVec3> group, Map map, BranchType branchType,
        int depth, HashSet<IntVec3> visited = null)
    {
        var vect = start.ToVector3Shifted();
        var intVec = start;
        var num = 0f;
        var elevation = MapGenerator.Elevation;
        var caves = MapGenerator.Caves;
        var num2 = 0;
        var num3 = 0;
        var num4 = Rand.Range(extCaves.widthOffsetPerCellMin, extCaves.widthOffsetPerCellMax);
        if (visited == null)
        {
            visited = [];
        }

        tmpGroupSet.Clear();
        tmpGroupSet.AddRange(group);
        var num5 = 0;
        while (true)
        {
            if (branchType == BranchType.Normal)
            {
                if (num5 - num2 >= extCaves.allowBranchingAfterSteps && Rand.Chance(extCaves.branchChance))
                {
                    var branchType2 = RandomBranchTypeByChance();
                    var num6 = CalculateBranchWidth(branchType, width);
                    if (num6 > extCaves.minTunnelWidth)
                    {
                        DigInBestDirection(intVec, dir, new FloatRange(-90f, -40f), num6, group, map, branchType2,
                            depth, visited);
                        num2 = num5;
                    }
                }

                if (num5 - num3 >= extCaves.allowBranchingAfterSteps && Rand.Chance(extCaves.branchChance))
                {
                    var branchType2 = RandomBranchTypeByChance();
                    var num6 = CalculateBranchWidth(branchType, width);
                    if (num6 > extCaves.minTunnelWidth)
                    {
                        DigInBestDirection(intVec, dir, new FloatRange(40f, 90f), num6, group, map, branchType2, depth,
                            visited);
                        num3 = num5;
                    }
                }
            }

            SetCaveAround(intVec, width, map, visited, out var hitAnotherTunnel);
            if (hitAnotherTunnel || branchType == BranchType.Room && num5 >= extCaves.branchRoomMaxLength)
            {
                break;
            }

            while (vect.ToIntVec3() == intVec)
            {
                vect += Vector3Utility.FromAngleFlat(dir) * 0.5f;
                num += 0.5f;
            }

            var intVec2 = vect.ToIntVec3();
            if (!tmpGroupSet.Contains(intVec2))
            {
                break;
            }

            var intVec3 = new IntVec3(intVec.x, 0, intVec2.z);
            if (IsRock(intVec3, elevation, map))
            {
                caves[intVec3] = Math.Max(caves[intVec3], width);
                visited.Add(intVec3);
            }

            width = branchType != BranchType.Tunnel
                ? width - num4
                : width - (num4 * extCaves.widthOffsetPerCellTunnelFactor);
            if (width < extCaves.minTunnelWidth)
            {
                break;
            }

            intVec = intVec2;
            dir += (float)directionNoise.GetValue(num * 60f, start.x * 200f, start.z * 200f) *
                   extCaves.directionChangeSpeed;
            num5++;
        }
    }

    private void DigInBestDirection(IntVec3 curIntVec, float curDir, FloatRange dirOffset, float width,
        List<IntVec3> group, Map map, BranchType branchType, int depth, HashSet<IntVec3> visited = null)
    {
        var dir = -1f;
        var num = -1;
        for (var i = 0; i < 6; i++)
        {
            var num2 = curDir + dirOffset.RandomInRange;
            var distToNonRock = GetDistToNonRock(curIntVec, group, num2, 50);
            if (distToNonRock <= num)
            {
                continue;
            }

            num = distToNonRock;
            dir = num2;
        }

        if (num >= extCaves.minDistToOutsideForBranching)
        {
            Dig(curIntVec, dir, width, group, map, branchType, depth + 1, visited);
        }
    }

    private void SetCaveAround(IntVec3 center, float tunnelWidth, Map map, HashSet<IntVec3> visited,
        out bool hitAnotherTunnel)
    {
        hitAnotherTunnel = false;
        var num = GenRadial.NumCellsInRadius(tunnelWidth / 2f * tunnelWidthNoise.GetValue(center));
        var elevation = MapGenerator.Elevation;
        var caves = MapGenerator.Caves;
        for (var i = 0; i < num; i++)
        {
            var intVec = center + GenRadial.RadialPattern[i];
            if (!IsRock(intVec, elevation, map))
            {
                continue;
            }

            if (caves[intVec] > 0f && !visited.Contains(intVec))
            {
                hitAnotherTunnel = true;
            }

            caves[intVec] = Math.Max(caves[intVec], tunnelWidth);
            visited.Add(intVec);
        }
    }

    private BranchType RandomBranchTypeByChance()
    {
        var value = Rand.Value;
        if (value < extCaves.branchChanceTypeRoom)
        {
            return BranchType.Room;
        }

        return value < extCaves.branchChanceTypeRoom + extCaves.branchChanceTypeTunnel
            ? BranchType.Tunnel
            : BranchType.Normal;
    }

    private float CalculateBranchWidth(BranchType branchType, float prevWidth)
    {
        return branchType switch
        {
            BranchType.Room => Rand.Range(extCaves.branchRoomFixedWidthMin, extCaves.branchRoomFixedWidthMax),
            BranchType.Tunnel => Rand.Range(extCaves.branchTunnelFixedWidthMin, extCaves.branchTunnelFixedWidthMax),
            _ => prevWidth * Rand.Range(extCaves.branchWidthFactorMin, extCaves.branchWidthFactorMax)
        };
    }

    private int GetDistToNonRock(IntVec3 from, List<IntVec3> group, IntVec3 offset, int maxDist)
    {
        groupSet.Clear();
        groupSet.AddRange(group);
        for (var i = 0; i <= maxDist; i++)
        {
            var item = from + (offset * i);
            if (!groupSet.Contains(item))
            {
                return i;
            }
        }

        return maxDist;
    }

    private int GetDistToNonRock(IntVec3 from, List<IntVec3> group, float dir, int maxDist)
    {
        groupSet.Clear();
        groupSet.AddRange(group);
        var vector = Vector3Utility.FromAngleFlat(dir);
        for (var i = 0; i <= maxDist; i++)
        {
            var item = (from.ToVector3Shifted() + (vector * i)).ToIntVec3();
            if (!groupSet.Contains(item))
            {
                return i;
            }
        }

        return maxDist;
    }

    private float GetDistToCave(IntVec3 cell, List<IntVec3> group, Map map, float maxDist, bool treatOpenSpaceAsCave)
    {
        var caves = MapGenerator.Caves;
        tmpGroupSet.Clear();
        tmpGroupSet.AddRange(group);
        var num = GenRadial.NumCellsInRadius(maxDist);
        var radialPattern = GenRadial.RadialPattern;
        for (var i = 0; i < num; i++)
        {
            var intVec = cell + radialPattern[i];
            if (treatOpenSpaceAsCave && !tmpGroupSet.Contains(intVec) || intVec.InBounds(map) && caves[intVec] > 0f)
            {
                return cell.DistanceTo(intVec);
            }
        }

        return maxDist;
    }

    private void RemoveSmallDisconnectedSubGroups(List<IntVec3> group, Map map)
    {
        groupSet.Clear();
        groupSet.AddRange(group);
        groupVisited.Clear();
        // ReSharper disable once ForCanBeConvertedToForeach, groups change in the loop
        for (var i = 0; i < group.Count; i++)
        {
            if (groupVisited.Contains(group[i]) || !groupSet.Contains(group[i]))
            {
                continue;
            }

            subGroup.Clear();
            map.floodFiller.FloodFill(group[i], x => groupSet.Contains(x), delegate(IntVec3 x)
            {
                subGroup.Add(x);
                groupVisited.Add(x);
            });
            if (subGroup.Count >= extCaves.minRocksToGenerateAnyTunnel && !(subGroup.Count < 0.05f * group.Count))
            {
                continue;
            }

            // ReSharper disable once ForCanBeConvertedToForeach, group changes in loop
            for (var j = 0; j < subGroup.Count; j++)
            {
                groupSet.Remove(subGroup[j]);
            }
        }

        group.Clear();
        group.AddRange(groupSet);
    }

    private void SmoothGenerated(Map map)
    {
        var caves = MapGenerator.Caves;
        var list = new List<IntVec3>();
        foreach (var allCell in map.AllCells)
        {
            if (caves[allCell] > 0f)
            {
                list.Add(allCell);
            }
        }

        GenMorphology.Close(list, 3, map);
        foreach (var allCell2 in map.AllCells)
        {
            if (allCell2.CloseToEdge(map, 3))
            {
                continue;
            }

            if (list.Contains(allCell2))
            {
                if (caves[allCell2] <= 0f)
                {
                    caves[allCell2] = 1f;
                }
            }
            else if (caves[allCell2] > 0f)
            {
                caves[allCell2] = 0f;
            }
        }
    }

    private enum BranchType
    {
        Normal,
        Room,
        Tunnel
    }
}