using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(GenStep_CaveHives))]
[HarmonyPatch("TrySpawnHive")]
internal class Harmony_GenStep_CaveHives_TrySpawnHive
{
    public static bool Prefix(GenStep_CaveHives __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is { overwriteHives: HiveOverwriteType.Remove })
        {
            return false;
        }

        return true;
    }

    public static void Postfix(GenStep_CaveHives __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteHives != HiveOverwriteType.AddActive)
        {
            return;
        }

        var value = Traverse.Create(__instance).Field("spawnedHives").GetValue<List<Hive>>();
        var hive = value.Last();
        hive.GetComp<CompSpawnerPawn>().canSpawnPawns = true;
        hive.GetComp<CompSpawnerHives>().canSpawnHives = true;
    }
}