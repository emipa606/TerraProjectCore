using HarmonyLib;
using RimWorld.Planet;

namespace TerraCore;

[HarmonyPatch("GenerateTileFor")]
[HarmonyPatch(typeof(WorldGenStep_Terrain))]
public class Harmony_WorldGenStep_Terrain_GenerateTileFor
{
    public static void Postfix(ref Tile __result)
    {
        GenWorldGen.UpdateTileByBiomeModExts(__result);
    }
}