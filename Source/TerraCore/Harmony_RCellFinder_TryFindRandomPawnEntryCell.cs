using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(RCellFinder))]
[HarmonyPatch("TryFindRandomPawnEntryCell")]
public class Harmony_RCellFinder_TryFindRandomPawnEntryCell
{
    public static bool Prefix(ref bool __result, out IntVec3 result, Map map, float roadChance,
        Predicate<IntVec3> extraValidator = null)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteRoof != RoofOverwriteType.FullStable)
        {
            result = IntVec3.Invalid;
            return true;
        }

        __result = CellFinder.TryFindRandomEdgeCellWith(
            c => c.Standable(map) && map.reachability.CanReachColony(c) &&
                 c.GetRoom(map).TouchesMapEdge &&
                 (extraValidator == null || extraValidator(c)), map, roadChance, out result);
        return false;
    }
}