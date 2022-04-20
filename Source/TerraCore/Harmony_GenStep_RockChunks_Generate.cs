using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch("Generate")]
[HarmonyPatch(typeof(GenStep_RockChunks))]
public class Harmony_GenStep_RockChunks_Generate
{
    public static bool Prefix(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is { overwriteRockChunks: RockChunksOverwriteType.Remove })
        {
            return false;
        }

        return true;
    }
}