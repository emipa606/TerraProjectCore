using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(GenTemperature), nameof(GenTemperature.GetTemperatureFromSeasonAtTile))]
public class Harmony_GenTemperature_GetTemperatureFromSeasonAtTile
{
    public static void Postfix(ref float __result, int tile)
    {
        var modExtension = Find.WorldGrid[tile].biome.GetModExtension<ModExt_Biome_Temperature>();
        if (modExtension != null)
        {
            __result = (__result * (1f - modExtension.tempStableWeight)) +
                       (modExtension.tempStableValue * modExtension.tempStableWeight) + modExtension.tempOffset;
        }
    }
}