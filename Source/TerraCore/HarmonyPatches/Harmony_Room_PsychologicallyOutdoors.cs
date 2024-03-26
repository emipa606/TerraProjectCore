using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(Room), nameof(Room.PsychologicallyOutdoors), MethodType.Getter)]
public class Harmony_Room_PsychologicallyOutdoors
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