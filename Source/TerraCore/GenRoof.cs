using System.Collections.Generic;
using Verse;

namespace TerraCore;

public static class GenRoof
{
    private const int deepRoofDistance = 11;

    public static void SetRoofComplete(Map map, RoofDef type)
    {
        foreach (var allCell in map.AllCells)
        {
            map.roofGrid.SetRoof(allCell, type);
        }

        map.roofGrid.RoofGridUpdate();
    }

    public static void SetStableDeepRoof(Map map)
    {
        var list = new List<IntVec3>();
        foreach (var allCell in map.AllCells)
        {
            if (!map.roofGrid.Roofed(allCell))
            {
                list.Add(allCell);
            }
        }

        GenMorphology.Dilate(list, deepRoofDistance, map);
        foreach (var allCell2 in map.AllCells)
        {
            if (!list.Contains(allCell2))
            {
                map.roofGrid.SetRoof(allCell2, RoofDefOf.RoofRockUncollapsable);
            }
        }

        map.roofGrid.RoofGridUpdate();
    }
}