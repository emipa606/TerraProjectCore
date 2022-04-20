using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Ravine : DefModExtension
{
    public GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public float elevationPostOffset = 0f;

    public float fertilityPostOffset = 0f;

    public bool invert = false;

    public float modAMax = 0f;

    public float modAMin = 0f;

    public float modBMax = 0f;

    public float modBMin = 0f;

    public float modCMax = 0f;

    public float modCMin = 0f;

    public float noiseElevationPreOffset = 0f;

    public float noiseElevationPreScale = 1f;

    public float noiseFertilityPreOffset = 0f;

    public float noiseFertilityPreScale = 1f;

    public float ravineWidthMax = 0.2f;
    public float ravineWidthMin = 0.1f;

    public float relativeOffsetFixed = 0f;

    public float relativeOffsetVariance = 0f;

    public int rotationVariance = 0;

    public float slopeFactor = 1f;
}