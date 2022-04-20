using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch("DropThingGroupsNear")]
[HarmonyPatch(typeof(DropPodUtility))]
public class Harmony_DropPodUtility_DropThingGroupsNear
{
    private const int MaxDistanceToEdge = 6;

    public static void Prefix(IntVec3 dropCenter, Map map, ref bool instaDrop)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension is { overwriteRoof: RoofOverwriteType.FullStable } && dropCenter.CloseToEdge(map, 6))
        {
            instaDrop = true;
        }
    }
}