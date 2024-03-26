using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenStep_RockChunks), nameof(GenStep_RockChunks.Generate))]
public class Harmony_GenStep_RockChunks_Generate
{
    public static bool Prefix(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        return modExtension is not { overwriteRockChunks: RockChunksOverwriteType.Remove };
    }
}