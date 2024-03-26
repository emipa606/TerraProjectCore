using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore;

public static class WorldGridExtension
{
    private static readonly List<int> tmpNeighbors = [];

    private static readonly List<Vector3> tmpVertsSelf = [];

    private static readonly List<Vector3> tmpVertsNeighbor = [];

    public static int GetDirection6WayIntFromTo(this WorldGrid grid, int fromTileID, int toTileID)
    {
        var headingFromTo = grid.GetHeadingFromTo(fromTileID, toTileID);
        if (headingFromTo is >= 330f or < 30f)
        {
            return 0;
        }

        if (headingFromTo < 90f)
        {
            return 1;
        }

        if (headingFromTo < 150f)
        {
            return 2;
        }

        if (headingFromTo < 210f)
        {
            return 3;
        }

        return headingFromTo < 270f ? 4 : 5;
    }

    public static int GetTileNeighborByDirection6WayInt(this WorldGrid grid, int tileID, int dir)
    {
        grid.GetTileNeighbors(tileID, tmpNeighbors);
        foreach (var tmpNeighbor in tmpNeighbors)
        {
            if (grid.GetDirection6WayIntFromTo(tileID, tmpNeighbor) == dir)
            {
                return tmpNeighbor;
            }
        }

        return tileID;
    }

    public static void GetTileDirsOfNeighborsByValidator(this WorldGrid wg, int tileID, List<int> outNeighborDirs,
        List<int> outAdjacentIds, TileNeightborPredicate validator)
    {
        var tileFrom = wg[tileID];
        wg.GetTileNeighbors(tileID, tmpNeighbors);
        for (var i = 0; i < tmpNeighbors.Count; i++)
        {
            if (!validator(tileFrom, wg[tmpNeighbors[i]]))
            {
                continue;
            }

            outNeighborDirs.Add(wg.GetDirection6WayIntFromTo(tileID, tmpNeighbors[i]));
            outAdjacentIds.Add(i);
        }
    }

    public static void GetTileGraphicDataFromNeighbors(this WorldGrid grid, int tileID, out int atlasX, out int atlasZ,
        out int rotDir, TileNeightborPredicate validator)
    {
        atlasX = 0;
        atlasZ = 0;
        rotDir = 0;
        var list = new List<int>();
        var list2 = new List<int>();
        grid.GetTileDirsOfNeighborsByValidator(tileID, list, list2, validator);
        var num = list.Count;
        if (num <= 0)
        {
            return;
        }

        var num2 = list.Sum();
        var num3 = list.Min();
        var num4 = list.Max();
        var item = num3;
        switch (num)
        {
            case 1:
                atlasZ = 1;
                break;
            case 2:
            {
                atlasX = 1;
                var num12 = num4 - num3;
                if (num12 is 1 or 5)
                {
                    atlasX++;
                }

                if (num12 is 2 or 4)
                {
                    atlasZ = 1;
                }

                if (num12 is 4 or 5)
                {
                    item = num4;
                }

                break;
            }
            case 3:
            {
                atlasX = 3;
                var num5 = Math.Abs(list[0] - list[1]);
                var num6 = Math.Abs(list[1] - list[2]);
                var num7 = Math.Abs(list[0] - list[2]);
                if (num5 == 3 || num6 == 3 || num7 == 3)
                {
                    atlasZ = 1;
                    if ((num2 - 1) % 3 == 0)
                    {
                        atlasX++;
                    }

                    if (num5 is 1 or 5)
                    {
                        item = list[2];
                    }
                    else if (num6 is 1 or 5)
                    {
                        item = list[0];
                    }
                    else if (num7 is 1 or 5)
                    {
                        item = list[1];
                    }
                }
                else if (num5 is 2 or 4 && num6 is 2 or 4 && num7 is 2 or 4)
                {
                    atlasX++;
                }
                else if (num3 == 0 && num4 == 5)
                {
                    item = num2 != 9 ? 5 : 4;
                }

                break;
            }
            case 4:
            {
                atlasX = 5;
                var num5 = Math.Abs(list[0] - list[1]);
                var num6 = Math.Abs(list[0] - list[2]);
                var num7 = Math.Abs(list[0] - list[3]);
                var num8 = Math.Abs(list[1] - list[2]);
                var num9 = Math.Abs(list[1] - list[3]);
                var num10 = Math.Abs(list[3] - list[3]);
                var num11 = 0;
                if (num5 == 3)
                {
                    num11++;
                }

                if (num6 == 3)
                {
                    num11++;
                }

                if (num7 == 3)
                {
                    num11++;
                }

                if (num8 == 3)
                {
                    num11++;
                }

                if (num9 == 3)
                {
                    num11++;
                }

                if (num10 == 3)
                {
                    num11++;
                }

                if (num11 == 2)
                {
                    atlasZ = 1;
                    if (num3 == 0 && num4 == 5)
                    {
                        item = 5;
                    }
                }
                else if (num2 % 2 == 0)
                {
                    atlasX++;
                    if (num3 == 0 && num4 == 5)
                    {
                        item = 9 - (num2 / 2);
                    }
                }
                else if (list.Contains(5) || list.Contains(1))
                {
                    item = list.Contains(4) || list.Contains(0) ? (15 - num2) / 2 : 5;
                }

                break;
            }
            case 5:
                atlasX = 6;
                atlasZ = 1;
                if (num2 > 10)
                {
                    item = 16 - num2;
                }

                break;
            default:
                atlasX = 2;
                atlasZ = 1;
                break;
        }

        var num13 = list.IndexOf(item);
        if (num13 < 0)
        {
            return;
        }

        if (list2.Count <= num13)
        {
            Log.Error("GetTileGraphicDataFromNeighbors: adjacentIds[neighborIndex] out of range");
        }

        var adjacentId = list2[num13];
        var list3 = new List<int>();
        grid.GetTileVertices(tileID, tmpVertsSelf);
        grid.GetTileVertices(grid.GetTileNeighbor(tileID, adjacentId), tmpVertsNeighbor);
        for (var i = 0; i < tmpVertsSelf.Count; i++)
        {
            if (tmpVertsSelf.Count <= i)
            {
                Log.Error("GetTileGraphicDataFromNeighbors: tmpVertsSelf[i] out of range");
            }

            if (tmpVertsNeighbor.Contains(tmpVertsSelf[i]))
            {
                list3.Add(i);
            }
        }

        if (list3.Contains(0) && list3.Contains(5))
        {
            rotDir = 1;
        }
        else
        {
            rotDir = list3.Max() + 1;
        }

        rotDir %= 6;
    }
}