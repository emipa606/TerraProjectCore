using HarmonyLib;
using RimWorld;
using Verse;
using Verse.Noise;

namespace TerraCore;

[HarmonyPatch(typeof(GenStep_CavesTerrain))]
[HarmonyPatch("Generate")]
public class Harmony_GenStep_CavesTerrain_Generate
{
    public static bool Prefix(Map map)
    {
        if (!Find.World.HasCaves(map.Tile))
        {
            return false;
        }

        var modExtension = map.Biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
        if (modExtension == null || modExtension.terrainPatchMakerCaveWater == null &&
            modExtension.terrainPatchMakerCaveGravel == null)
        {
            return true;
        }

        ModuleBase moduleBase = new Perlin(modExtension.terrainPatchMakerFrequencyCaveWater, 2.0, 0.5, 6, Rand.Int,
            QualityMode.Medium);
        ModuleBase moduleBase2 = new Perlin(modExtension.terrainPatchMakerFrequencyCaveGravel, 2.0, 0.5, 6, Rand.Int,
            QualityMode.Medium);
        var caves = MapGenerator.Caves;
        foreach (var allCell in map.AllCells)
        {
            if (!(caves[allCell] > 0f))
            {
                continue;
            }

            var terrain = allCell.GetTerrain(map);
            if (terrain.IsRiver)
            {
                continue;
            }

            var val = moduleBase.GetValue(allCell);
            var current2 = map.terrainGrid.TerrainAt(allCell);
            var terrainDef = TerrainThresholdWEO.TerrainAtValue(modExtension.terrainPatchMakerCaveWater, val, current2);
            if (terrainDef != null)
            {
                map.terrainGrid.SetTerrain(allCell, terrainDef);
                continue;
            }

            var val2 = moduleBase2.GetValue(allCell);
            var terrainDef2 =
                TerrainThresholdWEO.TerrainAtValue(modExtension.terrainPatchMakerCaveGravel, val2, current2);
            if (terrainDef2 != null)
            {
                map.terrainGrid.SetTerrain(allCell, terrainDef2);
            }
        }

        return false;
    }
}