using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenStep_Scatterer), nameof(GenStep_Scatterer.Generate))]
public class Harmony_GenStep_Scatterer_Generate
{
    private static readonly List<Type> debrisTypes =
    [
        typeof(GenStep_ScatterAncientFences),
        typeof(GenStep_ScatterAncientLandingPad),
        typeof(GenStep_ScatterAncientMechs),
        typeof(GenStep_ScatterAncientTurret),
        typeof(GenStep_ScatterAncientUtilityBuilding),
        typeof(GenStep_ScatterRoadDebris),
        typeof(GenStep_ScatterCaveDebris)
    ];

    public static bool Prefix(GenStep_Scatterer __instance, Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null)
        {
            return true;
        }

        if (__instance is GenStep_ScatterRuinsSimple && modExtension.removeRuinsSimple)
        {
            return false;
        }

        if (__instance is GenStep_ScatterShrines && modExtension.removeShrines)
        {
            return false;
        }

        return !debrisTypes.Contains(__instance.GetType()) || !modExtension.removeDebris;
    }
}