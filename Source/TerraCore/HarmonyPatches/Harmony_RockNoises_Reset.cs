using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(RockNoises), nameof(RockNoises.Reset))]
public class Harmony_RockNoises_Reset
{
    public static void Postfix()
    {
        IslandNoises.Reset();
    }
}