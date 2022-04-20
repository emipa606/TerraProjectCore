using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_BetterCaves : DefModExtension
{
    public int allowBranchingAfterSteps = 30;

    public float branchChance = 0.015f;

    public float branchChanceTypeRoom = 0.15f;

    public float branchChanceTypeTunnel = 0.25f;

    public float branchRoomFixedWidthMax = 30f;

    public float branchRoomFixedWidthMin = 20f;

    public int branchRoomMaxLength = 25;

    public float branchTunnelFixedWidthMax = 7f;

    public float branchTunnelFixedWidthMin = 4.5f;

    public float branchWidthFactorMax = 0.9f;

    public float branchWidthFactorMin = 0.6f;

    public float directionChangeSpeed = 8f;

    public int maxStartTunnelsPerRockGroup = 6;

    public int minDistToOutsideForBranching = 15;

    public int minRocksToGenerateAnyTunnel = 600;

    public float minTunnelWidth = 3.4f;
    public float startTunnelsPer10k = 2f;

    public List<TerrainThresholdWEO> terrainPatchMakerCaveGravel = null;

    public List<TerrainThresholdWEO> terrainPatchMakerCaveWater = null;

    public float terrainPatchMakerFrequencyCaveGravel = 0.16f;

    public float terrainPatchMakerFrequencyCaveWater = 0.08f;

    public float tunnelStartWidthFactorMax = 0.9f;

    public float tunnelStartWidthFactorMin = 0.6f;

    public float widthOffsetPerCellMax = 0.044f;

    public float widthOffsetPerCellMin = 0.019f;

    public float widthOffsetPerCellTunnelFactor = 0.2f;
}