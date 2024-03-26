using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;

namespace TerraCore.HarmonyPatches;

[HarmonyPatch(typeof(Designator_ZoneAdd_Growing), nameof(Designator_ZoneAdd_Growing.CanDesignateCell))]
public class Harmony_Designator_ZoneAdd_Growing_CanDesignateCell
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
        var i = 0;
        for (var iLen = instructions.Count(); i < iLen; i++)
        {
            var ci = instructions.ElementAt(i);
            if (ci.opcode == OpCodes.Ldsfld && instructions.ElementAt(i - 1).opcode == OpCodes.Callvirt &&
                instructions.ElementAt(i + 3).opcode == OpCodes.Bge_Un)
            {
                yield return new CodeInstruction(OpCodes.Ldc_R4, 0f);
                yield return new CodeInstruction(OpCodes.Bgt_Un, instructions.ElementAt(i + 3).operand);
                i += 3;
            }
            else
            {
                yield return ci;
            }
        }
    }
}