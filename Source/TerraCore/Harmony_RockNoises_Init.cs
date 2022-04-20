using HarmonyLib;
using Verse;

namespace TerraCore;

[HarmonyPatch("Init")]
[HarmonyPatch(typeof(RockNoises))]
public class Harmony_RockNoises_Init
{
    public static void Postfix(Map map)
    {
        IslandNoises.Init(map);
    }
}