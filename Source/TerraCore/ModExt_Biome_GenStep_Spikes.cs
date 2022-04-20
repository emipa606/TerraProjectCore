using Verse;

namespace TerraCore;

public class ModExt_Biome_GenStep_Spikes : DefModExtension
{
    public float baseFrequency = 0.0647f;

    public GenStepCalculationType calcElevationType = GenStepCalculationType.None;

    public GenStepCalculationType calcFertilityType = GenStepCalculationType.None;

    public float elevationPostOffset = 0f;

    public float fertilityPostOffset = 0f;

    public float noiseElevationPreOffset = 0f;

    public float noiseElevationPreScale = 1f;

    public float noiseFertilityPreOffset = 0f;

    public float noiseFertilityPreScale = 1f;

    public float sineOffset = 0f;

    public float sineScale = 1f;

    public float sineX = 5f;

    public float sineZ = 5f;
}