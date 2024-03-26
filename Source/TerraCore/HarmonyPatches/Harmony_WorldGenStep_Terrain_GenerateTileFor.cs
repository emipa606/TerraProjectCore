using HarmonyLib;
using RimWorld.Planet;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(WorldGenStep_Terrain), "GenerateTileFor")]
public class Harmony_WorldGenStep_Terrain_GenerateTileFor
{
    public static void Postfix(ref Tile __result)
    {
        GenWorldGen.UpdateTileByBiomeModExts(__result);
    }
}