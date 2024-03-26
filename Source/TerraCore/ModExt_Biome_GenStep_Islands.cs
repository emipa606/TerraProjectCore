using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Islands : DefModExtension
{
    public readonly float baseFrequency = 0.02f;

    public readonly GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public readonly GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public readonly float centerHeightValue = 3f;

    public readonly List<SimpleCurve> elevationPostCurves = null;

    public readonly float elevationPostOffset = 0f;

    public readonly float elevationPostScale = 0.3333f;

    public readonly List<SimpleCurve> fertilityPostCurves = null;

    public readonly float fertilityPostOffset = 0f;

    public readonly float fertilityPostScale = 1f;

    public readonly float invertOver = 1000f;

    public readonly bool invertSwap = false;

    public readonly float invertUnder = -1000f;

    public readonly int islandCountMax = 0;

    public readonly int islandCountMin = 0;

    public readonly float maxSizeX = 0.3f;

    public readonly float maxSizeZ = 0.3f;

    public readonly float minSizeX = 0.2f;

    public readonly float minSizeZ = 0.2f;

    public readonly float noiseElevationPreOffset = 0f;

    public readonly float noiseElevationPreScale = 1f;

    public readonly float noiseFertilityPreOffset = 0f;

    public readonly float noiseFertilityPreScale = 1f;

    public readonly List<TerrainThresholdWEO> terrainPatchMakerByIslandFertility = null;
}