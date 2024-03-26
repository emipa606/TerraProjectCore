using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenStep_CaveHives), "TrySpawnHive")]
internal class Harmony_GenStep_CaveHives_TrySpawnHive
{
    public static bool Prefix(GenStep_CaveHives __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        return modExtension is not { overwriteHives: HiveOverwriteType.Remove };
    }

    public static void Postfix(GenStep_CaveHives __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is not { overwriteHives: HiveOverwriteType.AddActive })
        {
            return;
        }

        var value = Traverse.Create(__instance).Field("spawnedHives").GetValue<List<Hive>>();
        var hive = value.Last();
        hive.GetComp<CompSpawnerPawn>().canSpawnPawns = true;
        hive.GetComp<CompSpawnerHives>().canSpawnHives = true;
    }
}