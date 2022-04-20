using Verse;
using Verse.Noise;

namespace TerraCore;

public static class RavineNoises
{
    public static void WorkOnMapGenerator(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_Ravine>();
        if (modExtension == null)
        {
            return;
        }

        var text = $"ravine {map.Biome.defName}";
        double num = (float)map.Size.x / 2;
        double num2 = (float)map.Size.z / 2;
        var num3 = Rand.Range(modExtension.ravineWidthMin, modExtension.ravineWidthMax);
        double a = Rand.Range(modExtension.modAMin, modExtension.modAMax);
        double b = Rand.Range(modExtension.modBMin, modExtension.modBMax);
        double c = Rand.Range(modExtension.modCMin, modExtension.modCMax);
        ModuleBase source = new DistFromAxis(map.Size.x * num3);
        source = new CurveAxis(a, b, c, source);
        source = new ScaleBias(modExtension.slopeFactor, -1.0, source);
        if (modExtension.invert)
        {
            source = new Invert(source);
        }

        source = new Clamp(0.0, 999.0, source);
        NoiseDebugUI.StoreNoiseRender(source, $"{text} base");
        var num4 = ((Rand.Value - 0.5f) * modExtension.relativeOffsetVariance) + modExtension.relativeOffsetFixed;
        num += map.Size.x * num4;
        num2 += map.Size.z * num4;
        double num5 = (Rand.Value - 0.5f) * modExtension.rotationVariance;
        Rot4 random;
        do
        {
            random = Rot4.Random;
        } while (random == Find.World.CoastDirectionAt(map.Tile));

        if (random == Rot4.North)
        {
            source = new Rotate(0.0, 270.0 + num5, 0.0, source);
            source = new Translate(0.0, 0.0, num2, source);
        }
        else if (random == Rot4.West)
        {
            source = new Rotate(0.0, 180.0 + num5, 0.0, source);
            source = new Translate(num, 0.0, 0.0, source);
        }
        else if (random == Rot4.South)
        {
            source = new Rotate(0.0, 90.0 + num5, 0.0, source);
            source = new Translate(0.0, 0.0, 0.0 - num2, source);
        }
        else if (random == Rot4.East)
        {
            source = new Rotate(0.0, 0.0 + num5, 0.0, source);
            source = new Translate(0.0 - num, 0.0, 0.0, source);
        }

        ModuleBase moduleBase = new ScaleBias(modExtension.noiseElevationPreScale, modExtension.noiseElevationPreOffset,
            source);
        NoiseDebugUI.StoreNoiseRender(moduleBase, $"{text} elevation");
        ModuleBase moduleBase2 = new ScaleBias(modExtension.noiseFertilityPreScale,
            modExtension.noiseFertilityPreOffset, source);
        NoiseDebugUI.StoreNoiseRender(moduleBase2, $"{text} fertility");
        var elevation = MapGenerator.Elevation;
        var fertility = MapGenerator.Fertility;
        foreach (var allCell in map.AllCells)
        {
            if (modExtension.calcElevationType == GenStepCalculationType.Set)
            {
                elevation[allCell] = moduleBase.GetValue(allCell) + modExtension.elevationPostOffset;
            }
            else if (modExtension.calcElevationType == GenStepCalculationType.Add)
            {
                elevation[allCell] += moduleBase.GetValue(allCell) + modExtension.elevationPostOffset;
            }

            if (modExtension.calcFertilityType == GenStepCalculationType.Set)
            {
                fertility[allCell] = moduleBase2.GetValue(allCell) + modExtension.fertilityPostOffset;
            }
            else if (modExtension.calcFertilityType == GenStepCalculationType.Add)
            {
                fertility[allCell] += moduleBase2.GetValue(allCell) + modExtension.fertilityPostOffset;
            }
        }
    }
}