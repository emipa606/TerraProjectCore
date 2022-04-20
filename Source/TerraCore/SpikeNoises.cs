using System;
using Verse;
using Verse.Noise;

namespace TerraCore;

public static class SpikeNoises
{
    public static void WorkOnMapGenerator(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_Spikes>();
        if (modExtension == null)
        {
            return;
        }

        var text = $"spikes {map.Biome.defName}";
        double num = (float)map.Size.x / 2;
        double num2 = (float)map.Size.z / 2;
        ModuleBase moduleBase = new Perlin(modExtension.baseFrequency, 2.0, 0.5, 6, Rand.Range(0, int.MaxValue),
            QualityMode.High);
        NoiseDebugUI.StoreNoiseRender(moduleBase, $"{text} base");
        ModuleBase moduleBase2 = new ScaleBias(modExtension.noiseElevationPreScale,
            modExtension.noiseElevationPreOffset, moduleBase);
        NoiseDebugUI.StoreNoiseRender(moduleBase2, $"{text} elevation");
        ModuleBase moduleBase3 = new ScaleBias(modExtension.noiseFertilityPreScale,
            modExtension.noiseFertilityPreOffset, moduleBase);
        NoiseDebugUI.StoreNoiseRender(moduleBase3, $"{text} fertility");
        var elevation = MapGenerator.Elevation;
        var fertility = MapGenerator.Fertility;
        foreach (var allCell in map.AllCells)
        {
            var num3 = (float)(Math.Sin((allCell.x - num) / modExtension.sineX) *
                               Math.Sin((allCell.z - num2) / modExtension.sineZ));
            num3 = (num3 * modExtension.sineScale) + modExtension.sineOffset;
            if (modExtension.calcElevationType == GenStepCalculationType.Set)
            {
                elevation[allCell] = (moduleBase2.GetValue(allCell) * num3) + modExtension.elevationPostOffset;
            }
            else if (modExtension.calcElevationType == GenStepCalculationType.Add)
            {
                elevation[allCell] += (moduleBase2.GetValue(allCell) * num3) + modExtension.elevationPostOffset;
            }

            if (modExtension.calcFertilityType == GenStepCalculationType.Set)
            {
                fertility[allCell] = (moduleBase3.GetValue(allCell) * num3) + modExtension.fertilityPostOffset;
            }
            else if (modExtension.calcFertilityType == GenStepCalculationType.Add)
            {
                fertility[allCell] += (moduleBase3.GetValue(allCell) * num3) + modExtension.fertilityPostOffset;
            }
        }
    }
}