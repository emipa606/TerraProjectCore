using System.Collections.Generic;
using Verse;
using Verse.Noise;

namespace TerraCore;

public static class IslandNoises
{
    private static ModExt_Biome_GenStep_Islands extIslands;

    private static ModuleBase noiseElevation;

    private static ModuleBase noiseFertility;

    private static List<IslandCone> islandCones;

    public static void Init(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_Islands>();
        if (modExtension == null)
        {
            return;
        }

        extIslands = modExtension;
        var text = $"islands {map.Biome.defName}";
        ModuleBase moduleBase = new Perlin(modExtension.baseFrequency, 2.0, 0.5, 5, Rand.Range(0, int.MaxValue),
            QualityMode.High);
        NoiseDebugUI.StoreNoiseRender(moduleBase, $"{text} base");
        noiseElevation = new ScaleBias(modExtension.noiseElevationPreScale, modExtension.noiseElevationPreOffset,
            moduleBase);
        NoiseDebugUI.StoreNoiseRender(noiseElevation, $"{text} elevation");
        noiseFertility = new ScaleBias(modExtension.noiseFertilityPreScale, modExtension.noiseFertilityPreOffset,
            moduleBase);
        NoiseDebugUI.StoreNoiseRender(noiseFertility, $"{text} fertility");
        islandCones = new List<IslandCone>();
        var num = Rand.Range(modExtension.islandCountMin, modExtension.islandCountMax);
        for (var i = 0; i < num; i++)
        {
            var islandNoise = new IslandCone();
            SetIslandOnRandomLocation(ref islandNoise, map);
            islandCones.Add(islandNoise);
        }
    }

    public static void Reset()
    {
        extIslands = null;
        noiseElevation = null;
        noiseFertility = null;
        islandCones = null;
    }

    public static void WorkOnMapGenerator(Map map)
    {
        var elevation = MapGenerator.Elevation;
        var fertility = MapGenerator.Fertility;
        SimpleCurve elevationCurve = null;
        SimpleCurve fertilityCurve = null;
        if (extIslands.elevationPostCurves != null)
        {
            elevationCurve = extIslands.elevationPostCurves.RandomElementWithFallback();
        }

        if (extIslands.fertilityPostCurves != null)
        {
            fertilityCurve = extIslands.fertilityPostCurves.RandomElementWithFallback();
        }

        foreach (var allCell in map.AllCells)
        {
            var elevationValue = (GetValueAt(allCell, noiseElevation) * extIslands.elevationPostScale) +
                                 extIslands.elevationPostOffset;
            var fertilityValue = (GetValueAt(allCell, noiseFertility) * extIslands.fertilityPostScale) +
                                 extIslands.fertilityPostOffset;
            if (elevationCurve != null)
            {
                elevationValue = elevationCurve.Evaluate(elevationValue);
            }

            if (fertilityCurve != null)
            {
                fertilityValue = fertilityCurve.Evaluate(fertilityValue);
            }

            switch (extIslands.calcElevationType)
            {
                case GenStepCalculationType.Set:
                    elevation[allCell] = elevationValue;
                    break;
                case GenStepCalculationType.Add:
                    elevation[allCell] += elevationValue;
                    break;
            }

            switch (extIslands.calcFertilityType)
            {
                case GenStepCalculationType.Set:
                    fertility[allCell] = fertilityValue;
                    break;
                case GenStepCalculationType.Add:
                    fertility[allCell] += fertilityValue;
                    break;
            }
        }
    }

    public static TerrainDef TerrainAtFromTerrainPatchMakerByFertility(IntVec3 c, Map map, TerrainDef current)
    {
        if (extIslands.terrainPatchMakerByIslandFertility == null)
        {
            return null;
        }

        return TerrainThresholdWEO.TerrainAtValue(extIslands.terrainPatchMakerByIslandFertility,
            GetValueAt(c, noiseFertility), current);
    }

    private static void SetIslandOnRandomLocation(ref IslandCone islandNoise, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_Islands>();
        var min = (int)(map.Size.x * modExtension.minSizeX);
        var max = (int)(map.Size.x * modExtension.maxSizeX);
        var min2 = (int)(map.Size.z * modExtension.minSizeZ);
        var max2 = (int)(map.Size.z * modExtension.maxSizeZ);
        var num = Rand.Range(min, max) / 2;
        var num2 = Rand.Range(min2, max2) / 2;
        islandNoise.radiusX = num;
        islandNoise.radiusZ = num2;
        islandNoise.centerX = Rand.Range(num + 5, map.Size.x - num - 5);
        islandNoise.centerZ = Rand.Range(num2 + 5, map.Size.z - num2 - 5);
        islandNoise.distZFactor = num / (float)num2;
        islandNoise.radiusSquared = num * num;
    }

    private static float GetValueAt(IntVec3 cell, ModuleBase noise)
    {
        var num = -1000f;
        foreach (var islandCone in islandCones)
        {
            float num2 = islandCone.centerX - cell.x;
            var num3 = islandCone.distZFactor * (islandCone.centerZ - cell.z);
            var x = (num2 * num2) + (num3 * num3);
            var num4 = GenMath.LerpDouble(0f, islandCone.radiusSquared, extIslands.centerHeightValue, 0f, x);
            if (num4 > num)
            {
                num = num4;
            }
        }

        num += noise.GetValue(cell);
        if (num > extIslands.invertOver)
        {
            num -= (num - extIslands.invertOver) * 2f;
        }

        if (num < extIslands.invertUnder)
        {
            num += (extIslands.invertUnder - num) * 2f;
            if (num > extIslands.centerHeightValue)
            {
                num = extIslands.centerHeightValue;
            }
        }

        if (num < 0f)
        {
            num = 0f;
        }

        if (extIslands.invertSwap)
        {
            num = extIslands.centerHeightValue - num;
        }

        return num;
    }

    public class IslandCone
    {
        public int centerX;

        public int centerZ;

        public float distZFactor;

        public int radiusSquared;

        public int radiusX;

        public int radiusZ;
    }
}