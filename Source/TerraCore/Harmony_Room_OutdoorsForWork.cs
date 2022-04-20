using HarmonyLib;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(Room))]
[HarmonyPatch("OutdoorsForWork", MethodType.Getter)]
public class Harmony_Room_OutdoorsForWork
{
    public static bool Prefix(Room __instance, ref bool __result)
    {
        var modExtension = __instance.Map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.roomCalculationType == RoomCalculationType.Default)
        {
            return true;
        }

        __result = __instance.OutdoorsByRCType(modExtension.roomCalculationType);
        return false;
    }
}