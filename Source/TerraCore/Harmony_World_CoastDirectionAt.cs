using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(World))]
[HarmonyPatch("CoastDirectionAt")]
public class Harmony_World_CoastDirectionAt
{
    public static bool Prefix(World __instance, ref Rot4 __result, int tileID)
    {
        var tile = __instance.grid[tileID];
        var modExtension = tile.biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || !modExtension.removeBeach)
        {
            return true;
        }

        __result = Rot4.Invalid;
        return false;
    }
}