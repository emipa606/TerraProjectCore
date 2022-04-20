using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(DropCellFinder))]
[HarmonyPatch("CanPhysicallyDropInto")]
public class Harmony_DropCellFinder_CanPhysicallyDropInto
{
    private const int MaxDistanceToEdge = 5;

    public static bool Prefix(ref bool __result, IntVec3 c, Map map, bool canRoofPunch)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteRoof != RoofOverwriteType.FullStable)
        {
            return true;
        }

        if (!c.Walkable(map))
        {
            __result = false;
            return false;
        }

        if (c.CloseToEdge(map, 5))
        {
            __result = true;
            return false;
        }

        var roof = c.GetRoof(map);
        if (roof != null && !canRoofPunch)
        {
            __result = false;
        }
        else
        {
            __result = true;
        }

        return false;
    }
}