using HarmonyLib;
using RimWorld;
using Verse;

namespace TerraCore;

[HarmonyPatch(typeof(GenStep_FindPlayerStartSpot))]
[HarmonyPatch("Generate")]
public class Harmony_GenStep_FindPlayerStartSpot_Generate
{
    private const int MaxDistanceToEdge = 6;

    private const int MinRoomCellCount = 30;

    public static bool Prefix(Map map)
    {
        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteRoof != RoofOverwriteType.FullStable)
        {
            return true;
        }

        DeepProfiler.Start("RebuildAllRegions");
        map.regionAndRoomUpdater.RebuildAllRegionsAndRooms();
        DeepProfiler.End();
        MapGenerator.PlayerStartSpot = CellFinderLoose.TryFindCentralCell(map, 7, 30, x => x.CloseToEdge(map, 6));
        return false;
    }
}