using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenStep_Caves), nameof(GenStep_Caves.Generate))]
public class Harmony_GenStep_Caves_Generate
{
    public static bool Prefix(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        return modExtension is not { overwriteCaves: GenFeatureTristate.Remove };
    }
}