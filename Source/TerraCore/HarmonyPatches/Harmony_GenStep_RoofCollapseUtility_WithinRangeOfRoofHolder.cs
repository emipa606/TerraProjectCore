using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(RoofCollapseUtility), nameof(RoofCollapseUtility.WithinRangeOfRoofHolder))]
public class Harmony_GenStep_RoofCollapseUtility_WithinRangeOfRoofHolder
{
    public static bool Prefix(ref bool __result, IntVec3 c, Map map)
    {
        if (map.roofGrid.RoofAt(c) != RoofDefOf.RoofRockUncollapsable)
        {
            return true;
        }

        __result = true;
        return false;
    }
}