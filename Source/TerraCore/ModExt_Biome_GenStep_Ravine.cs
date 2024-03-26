using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Ravine : DefModExtension
{
    public readonly GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public readonly GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public readonly float elevationPostOffset = 0f;

    public readonly float fertilityPostOffset = 0f;

    public readonly bool invert = false;

    public readonly float modAMax = 0f;

    public readonly float modAMin = 0f;

    public readonly float modBMax = 0f;

    public readonly float modBMin = 0f;

    public readonly float modCMax = 0f;

    public readonly float modCMin = 0f;

    public readonly float noiseElevationPreOffset = 0f;

    public readonly float noiseElevationPreScale = 1f;

    public readonly float noiseFertilityPreOffset = 0f;

    public readonly float noiseFertilityPreScale = 1f;

    public readonly float ravineWidthMax = 0.2f;
    public readonly float ravineWidthMin = 0.1f;

    public readonly float relativeOffsetFixed = 0f;

    public readonly float relativeOffsetVariance = 0f;

    public readonly int rotationVariance = 0;

    public readonly float slopeFactor = 1f;
}