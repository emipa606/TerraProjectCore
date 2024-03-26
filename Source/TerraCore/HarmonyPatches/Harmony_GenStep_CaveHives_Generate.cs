using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenStep_CaveHives), nameof(GenStep_CaveHives.Generate))]
public class Harmony_GenStep_CaveHives_Generate
{
    public static void Postfix(GenStep_CaveHives __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is not { overwriteHives: HiveOverwriteType.Add or HiveOverwriteType.AddActive })
        {
            return;
        }

        var num = map.ScaleValueOnSize(modExtension.additionalHivesScaling);
        var traverse = Traverse.Create(__instance).Method("TrySpawnHive", map);
        for (var i = 0; i < num; i++)
        {
            traverse.GetValue();
        }
    }
}