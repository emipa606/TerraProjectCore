using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(TradeUtility))]
[HarmonyPatch("SpawnDropPod")]
public class Harmony_TradeUtility_SpawnDropPod
{
    public static bool Prefix(IntVec3 dropSpot, Map map, Thing t)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteRoof != RoofOverwriteType.FullStable)
        {
            return true;
        }

        GenPlace.TryPlaceThing(t, dropSpot, map, ThingPlaceMode.Near);
        return false;
    }
}