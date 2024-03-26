using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_BetterCaves : DefModExtension
{
    public readonly int allowBranchingAfterSteps = 30;

    public readonly float branchChance = 0.015f;

    public readonly float branchChanceTypeRoom = 0.15f;

    public readonly float branchChanceTypeTunnel = 0.25f;

    public readonly float branchRoomFixedWidthMax = 30f;

    public readonly float branchRoomFixedWidthMin = 20f;

    public readonly int branchRoomMaxLength = 25;

    public readonly float branchTunnelFixedWidthMax = 7f;

    public readonly float branchTunnelFixedWidthMin = 4.5f;

    public readonly float branchWidthFactorMax = 0.9f;

    public readonly float branchWidthFactorMin = 0.6f;

    public readonly float directionChangeSpeed = 8f;

    public readonly int maxStartTunnelsPerRockGroup = 6;

    public readonly int minDistToOutsideForBranching = 15;

    public readonly int minRocksToGenerateAnyTunnel = 600;

    public readonly float minTunnelWidth = 3.4f;
    public readonly float startTunnelsPer10k = 2f;

    public readonly List<TerrainThresholdWEO> terrainPatchMakerCaveGravel = null;

    public readonly List<TerrainThresholdWEO> terrainPatchMakerCaveWater = null;

    public readonly float terrainPatchMakerFrequencyCaveGravel = 0.16f;

    public readonly float terrainPatchMakerFrequencyCaveWater = 0.08f;

    public readonly float tunnelStartWidthFactorMax = 0.9f;

    public readonly float tunnelStartWidthFactorMin = 0.6f;

    public readonly float widthOffsetPerCellMax = 0.044f;

    public readonly float widthOffsetPerCellMin = 0.019f;

    public readonly float widthOffsetPerCellTunnelFactor = 0.2f;
}