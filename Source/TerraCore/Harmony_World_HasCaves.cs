using HarmonyLib;
using RimWorld.Planet;

namespace TerraCore;

[HarmonyPatch("HasCaves")]
[HarmonyPatch(typeof(World))]
public class Harmony_World_HasCaves
{
    public static bool Prefix(World __instance, ref bool __result, int tile)
    {
        var biome = __instance.grid[tile].biome;
        var modExtension = biome.GetModExtension<ModExt_Biome_GenStep_BetterCaves>();
        if (modExtension != null)
        {
            __result = true;
            return false;
        }

        var modExtension2 = biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension2 == null)
        {
            return true;
        }

        if (modExtension2.overwriteCaves == GenFeatureTristate.Add)
        {
            __result = true;
            return false;
        }

        if (modExtension2.overwriteCaves != GenFeatureTristate.Remove)
        {
            return true;
        }

        __result = false;
        return false;
    }
}