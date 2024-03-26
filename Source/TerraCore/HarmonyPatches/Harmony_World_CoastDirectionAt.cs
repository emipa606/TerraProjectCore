using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(World), nameof(World.CoastDirectionAt))]
public class Harmony_World_CoastDirectionAt
{
    public static bool Prefix(World __instance, ref Rot4 __result, int tileID)
    {
        var tile = __instance.grid[tileID];
        var modExtension = tile.biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is not { removeBeach: true })
        {
            return true;
        }

        __result = Rot4.Invalid;
        return false;
    }
}