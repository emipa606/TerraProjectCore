using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;

namespace TerraCore;

[HarmonyPatch(typeof(BeachMaker))]
[HarmonyPatch("BeachTerrainAt")]
public class Harmony_BeachMaker_BeachTerrainAt
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
        var i = 0;
        for (var iLen = instructions.Count(); i < iLen; i++)
        {
            var ci = instructions.ElementAt(i);
            if (ci.opcode == OpCodes.Ldc_R4 && ci.operand is 0.45f)
            {
                var jumpTarget = il.DefineLabel();
                yield return new CodeInstruction(OpCodes.Ldc_R4, 0.25f);
                yield return new CodeInstruction(OpCodes.Bge_Un, jumpTarget);
                yield return new CodeInstruction(OpCodes.Ldsfld,
                    AccessTools.Field(typeof(TerrainDefOf), "WaterOceanChestDeep"));
                yield return new CodeInstruction(OpCodes.Ret);
                yield return new CodeInstruction(OpCodes.Ldloc_0)
                {
                    labels = new List<Label> { jumpTarget }
                };
            }

            yield return ci;
        }
    }
}