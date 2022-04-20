using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch("Generate")]
[HarmonyPatch(typeof(GenStep_Terrain))]
public class Harmony_GenStep_Terrain_Generate
{
    public static void Postfix(Map map)
    {
        var adjacentCells = GenAdj.AdjacentCells;
        var topGrid = map.terrainGrid.topGrid;
        int i;
        if (map.Biome == BiomeDefOf.TundraSkerries)
        {
            i = 0;
            for (var num = topGrid.Length; i < num; i++)
            {
                var num2 = 0;
                if (topGrid[i] != TerrainDefOf.NormalSand && topGrid[i] != TerrainDefOf.Sandstone_Rough &&
                    topGrid[i] != TerrainDefOf.Granite_Rough && topGrid[i] != TerrainDefOf.Limestone_Rough &&
                    topGrid[i] != TerrainDefOf.Slate_Rough && topGrid[i] != TerrainDefOf.Marble_Rough)
                {
                    continue;
                }

                var intVec = map.cellIndices.IndexToCell(i);
                for (var j = 0; j < 8; j++)
                {
                    var c = intVec + adjacentCells[j];
                    if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterOceanShallow)
                    {
                        num2++;
                    }
                }

                if (num2 > 5)
                {
                    map.terrainGrid.SetTerrain(intVec, TerrainDefOf.WaterOceanShallow);
                }
            }
        }

        i = 0;
        for (var num = topGrid.Length; i < num; i++)
        {
            if (topGrid[i] != TerrainDefOf.WaterOceanDeep)
            {
                continue;
            }

            var intVec = map.cellIndices.IndexToCell(i);
            for (var j = 0; j < 8; j++)
            {
                var c = intVec + adjacentCells[j];
                if (!c.InBounds(map) || map.terrainGrid.TerrainAt(c) != TerrainDefOf.WaterOceanShallow)
                {
                    continue;
                }

                map.terrainGrid.SetTerrain(c, TerrainDefOf.WaterOceanChestDeep);
                map.terrainGrid.SetTerrain(intVec, TerrainDefOf.WaterOceanChestDeep);
            }
        }

        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null)
        {
            return;
        }

        if (modExtension.overwriteRoof == RoofOverwriteType.FullStable)
        {
            GenRoof.SetRoofComplete(map, RoofDefOf.RoofRockUncollapsable);
        }
        else if (modExtension.overwriteRoof == RoofOverwriteType.DeepOnlyStable)
        {
            GenRoof.SetStableDeepRoof(map);
        }
    }
}