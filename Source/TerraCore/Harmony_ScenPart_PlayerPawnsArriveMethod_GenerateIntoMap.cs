using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch("GenerateIntoMap")]
[HarmonyPatch(typeof(ScenPart_PlayerPawnsArriveMethod))]
public class Harmony_ScenPart_PlayerPawnsArriveMethod_GenerateIntoMap
{
    public static void Prefix(ScenPart_PlayerPawnsArriveMethod __instance, Map map)
    {
        if (Find.GameInitData == null)
        {
            return;
        }

        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is { overwriteRoof: RoofOverwriteType.FullStable })
        {
            Traverse.Create(__instance).Field("method").SetValue(PlayerPawnsArriveMethod.Standing);
        }
    }
}