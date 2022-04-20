using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class Harmony_WorldLayer_Hills_Regenerate
{
    private static bool CheckSkipHelper(Tile tile)
    {
        if (tile.hilliness == Hilliness.Impassable)
        {
            return true;
        }

        if (tile.biome.WorkerSpecial() != null || tile.biome == BiomeDefOf.CaveEntrance ||
            tile.biome == BiomeDefOf.TunnelworldCave)
        {
            return true;
        }

        return false;
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
        Label? jumpTarget = null;
        var j = 0;
        for (var num = instructions.Count(); j < num; j++)
        {
            var codeInstruction = instructions.ElementAt(j);
            if (codeInstruction.opcode != OpCodes.Ldloc_S || instructions.ElementAt(j + 1).opcode != OpCodes.Ldc_I4_1 ||
                instructions.ElementAt(j + 2).opcode != OpCodes.Add ||
                instructions.ElementAt(j + 3).opcode != OpCodes.Stloc_S)
            {
                continue;
            }

            jumpTarget = codeInstruction.labels[0];
            break;
        }

        if (!jumpTarget.HasValue)
        {
            Log.Error("Transpiler Harmony_WorldLayer_Hills_Regenerate could not find needed jumpTarget.");
            yield break;
        }

        var i = 0;
        for (var iLen = instructions.Count(); i < iLen; i++)
        {
            var ci = instructions.ElementAt(i);
            if (ci.opcode == OpCodes.Stloc_S && instructions.ElementAt(i - 1).opcode == OpCodes.Callvirt &&
                instructions.ElementAt(i - 2).opcode == OpCodes.Ldloc_3 &&
                instructions.ElementAt(i - 3).opcode == OpCodes.Ldfld &&
                instructions.ElementAt(i - 4).opcode == OpCodes.Ldarg_0)
            {
                yield return ci;
                yield return new CodeInstruction(OpCodes.Ldloc_S, 4);
                yield return new CodeInstruction(OpCodes.Call,
                    AccessTools.Method(typeof(Harmony_WorldLayer_Hills_Regenerate), "CheckSkipHelper"));
                yield return new CodeInstruction(OpCodes.Brtrue, jumpTarget.Value);
            }
            else
            {
                yield return ci;
            }
        }
    }
}