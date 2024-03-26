using HarmonyLib;
using Verse;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(RockNoises), nameof(RockNoises.Init))]
public class Harmony_RockNoises_Init
{
    public static void Postfix(Map map)
    {
        IslandNoises.Init(map);
    }
}