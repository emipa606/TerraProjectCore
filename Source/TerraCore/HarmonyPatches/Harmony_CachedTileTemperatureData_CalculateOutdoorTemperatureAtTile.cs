using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

public class Harmony_CachedTileTemperatureData_CalculateOutdoorTemperatureAtTile
{
    public static void Postfix(object __instance, ref float __result)
    {
        var value = Traverse.Create(__instance).Field("tile").GetValue<int>();
        var modExtension = Find.WorldGrid[value].biome.GetModExtension<ModExt_Biome_Temperature>();
        if (modExtension != null)
        {
            __result = (__result * (1f - modExtension.tempStableWeight)) +
                       (modExtension.tempStableValue * modExtension.tempStableWeight) + modExtension.tempOffset;
        }
    }
}