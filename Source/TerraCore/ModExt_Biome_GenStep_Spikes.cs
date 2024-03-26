using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Spikes : DefModExtension
{
    public readonly float baseFrequency = 0.0647f;

    public readonly GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public readonly GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public readonly float elevationPostOffset = 0f;

    public readonly float fertilityPostOffset = 0f;

    public readonly float noiseElevationPreOffset = 0f;

    public readonly float noiseElevationPreScale = 1f;

    public readonly float noiseFertilityPreOffset = 0f;

    public readonly float noiseFertilityPreScale = 1f;

    public readonly float sineOffset = 0f;

    public readonly float sineScale = 1f;

    public readonly float sineX = 5f;

    public readonly float sineZ = 5f;
}