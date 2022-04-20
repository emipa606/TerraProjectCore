using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Islands : DefModExtension
{
    public float baseFrequency = 0.02f;

    public GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public float centerHeightValue = 3f;

    public List<SimpleCurve> elevationPostCurves = null;

    public float elevationPostOffset = 0f;

    public float elevationPostScale = 0.3333f;

    public List<SimpleCurve> fertilityPostCurves = null;

    public float fertilityPostOffset = 0f;

    public float fertilityPostScale = 1f;

    public float invertOver = 1000f;

    public bool invertSwap = false;

    public float invertUnder = -1000f;

    public int islandCountMax = 0;

    public int islandCountMin = 0;

    public float maxSizeX = 0.3f;

    public float maxSizeZ = 0.3f;

    public float minSizeX = 0.2f;

    public float minSizeZ = 0.2f;

    public float noiseElevationPreOffset = 0f;

    public float noiseElevationPreScale = 1f;

    public float noiseFertilityPreOffset = 0f;

    public float noiseFertilityPreScale = 1f;

    public List<TerrainThresholdWEO> terrainPatchMakerByIslandFertility = null;
}