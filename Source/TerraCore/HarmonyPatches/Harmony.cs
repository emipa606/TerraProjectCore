using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace TerraCore.HarmonyPatches;

[StaticConstructorOnStartup]
internal class Harmony
{
    static Harmony()
    {
        var harmonyInstance = new HarmonyLib.Harmony("rimworld.lanilor.terracore");
        harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        harmonyInstance.Patch(
            AccessTools.Method(AccessTools.TypeByName("CachedTileTemperatureData"),
                "CalculateOutdoorTemperatureAtTile"), null,
            new HarmonyMethod(typeof(Harmony_CachedTileTemperatureData_CalculateOutdoorTemperatureAtTile), "Postfix"));
        harmonyInstance.Patch(
            AccessTools.Method(
                typeof(WorldLayer_Hills).GetNestedTypes(BindingFlags.Instance | BindingFlags.NonPublic).First(),
                "MoveNext"), null, null, new HarmonyMethod(typeof(Harmony_WorldLayer_Hills_Regenerate), "Transpiler"));
    }
}