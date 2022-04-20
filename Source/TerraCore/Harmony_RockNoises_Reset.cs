using HarmonyLib;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(RockNoises))]
[HarmonyPatch("Reset")]
public class Harmony_RockNoises_Reset
{
    public static void Postfix()
    {
        IslandNoises.Reset();
    }
}