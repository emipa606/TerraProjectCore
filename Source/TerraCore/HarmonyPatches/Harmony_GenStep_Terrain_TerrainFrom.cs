using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch("TerrainFrom")]
[HarmonyPatch(typeof(GenStep_Terrain))]
public class Harmony_GenStep_Terrain_TerrainFrom
{
    public static void Postfix(ref TerrainDef __result, IntVec3 c, Map map)
    {
        var biome = map.Biome;
        var modExt_Biome_Replacement =
            biome.GetModExtension<ModExt_Biome_Replacement>() ?? ModExt_Biome_Replacement.Default;
        modExt_Biome_Replacement.EnsureDefaultValues();
        if (__result == RimWorld.TerrainDefOf.Sand)
        {
            __result = modExt_Biome_Replacement.sandReplacement;
        }

        if (__result == RimWorld.TerrainDefOf.Gravel)
        {
            __result = modExt_Biome_Replacement.gravelReplacement;
        }

        if (biome.HasModExtension<ModExt_Biome_GenStep_Islands>())
        {
            var terrainDef = IslandNoises.TerrainAtFromTerrainPatchMakerByFertility(c, map, __result);
            if (terrainDef != null)
            {
                __result = terrainDef;
            }
        }

        if (__result == TerrainDefOf.FillerStone)
        {
            __result = GenStep_RocksFromGrid.RockDefAt(c).building.naturalTerrain;
        }
    }
}