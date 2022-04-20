using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch("Generate")]
[HarmonyPatch(typeof(GenStep_Caves))]
public class Harmony_GenStep_Caves_Generate
{
    public static bool Prefix(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is { overwriteCaves: GenFeatureTristate.Remove })
        {
            return false;
        }

        return true;
    }
}