using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(RiverMaker))]
[HarmonyPatch("ValidatePassage")]
public class Harmony_RiverMaker_ValidatePassage
{
    public static void Postfix(object __instance, Map map)
    {
        var cardinalDirections = GenAdj.CardinalDirections;
        var topGrid = map.terrainGrid.topGrid;
        var i = 0;
        for (var num = topGrid.Length; i < num; i++)
        {
            if (topGrid[i] != TerrainDefOf.WaterMovingChestDeep)
            {
                continue;
            }

            var intVec = map.cellIndices.IndexToCell(i);
            for (var j = 0; j < 4; j++)
            {
                var c = intVec + cardinalDirections[j];
                if (c.InBounds(map) && map.terrainGrid.TerrainAt(c) == TerrainDefOf.WaterMovingShallow)
                {
                }
            }
        }
    }
}