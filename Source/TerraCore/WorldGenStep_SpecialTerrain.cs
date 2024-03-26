using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class WorldGenStep_SpecialTerrain : WorldGenStep
{
    private const float ImpassableChangeChance = 0.8f;

    private const float ImpassableChangeRecursiveChance = 0.2f;

    private const int MaxImpassableChangeDepth = 3;

    private static readonly List<int> tmpNeighbors = [];

    public override int SeedPart => 144374476;

    public override void GenerateFresh(string seed)
    {
        var allDefsListForReading =
            DefDatabase<BiomeDef>.AllDefsListForReading.Where(biomeDef => biomeDef.generatesNaturally).ToList();
        var list = new List<BiomeDef>();
        for (var num = allDefsListForReading.Count - 1; num >= 0; num--)
        {
            if (allDefsListForReading[num].WorkerSpecial() == null)
            {
                continue;
            }

            list.Add(allDefsListForReading[num]);
            allDefsListForReading[num].WorkerSpecial().ResetChance();
        }

        var worldGrid = Find.WorldGrid;
        var tiles = worldGrid.tiles;
        var tilesCount = worldGrid.TilesCount;
        for (var num = 0; num < tilesCount; num++)
        {
            if (list.Contains(tiles[num].biome))
            {
                continue;
            }

            foreach (var item in list)
            {
                var biomeWorkerSpecial = item.WorkerSpecial();
                var tile = tiles[num];
                if (!biomeWorkerSpecial.PreRequirements(tile) || !biomeWorkerSpecial.TryGenerateByChance())
                {
                    continue;
                }

                tile.biome = item;
                GenWorldGen.UpdateTileByBiomeModExts(tile);
                biomeWorkerSpecial.PostGeneration(num);
            }
        }

        for (var num = 0; num < tilesCount; num++)
        {
            var tile = tiles[num];
            if (tile.biome != BiomeDefOf.CaveOasis && tile.biome != BiomeDefOf.TunnelworldCave)
            {
                continue;
            }

            var list2 = new List<BiomeDef>(list)
            {
                RimWorld.BiomeDefOf.SeaIce,
                RimWorld.BiomeDefOf.Lake,
                RimWorld.BiomeDefOf.Ocean
            };
            MakeImpassableHillsAroundTile(worldGrid, num, list2, 1);
        }
    }

    private void MakeImpassableHillsAroundTile(WorldGrid grid, int tileID, List<BiomeDef> excludeBiomes, int currDepth)
    {
        var foundEntrance = false;
        grid.GetTileNeighbors(tileID, tmpNeighbors);
        foreach (var tileId in tmpNeighbors)
        {
            if (grid[tileId].biome != BiomeDefOf.CaveEntrance)
            {
                continue;
            }

            foundEntrance = true;
            break;
        }

        if (foundEntrance)
        {
            return;
        }

        // ReSharper disable once ForCanBeConvertedToForeach, Collection modifies
        for (var index = 0; index < tmpNeighbors.Count; index++)
        {
            var tileId = tmpNeighbors[index];
            var tile = grid[tileId];
            if (excludeBiomes.Contains(tile.biome) || !(Rand.Value < 0.8f))
            {
                continue;
            }

            tile.hilliness = Hilliness.Impassable;
            if (currDepth < 3 && Rand.Value < 0.2f)
            {
                MakeImpassableHillsAroundTile(grid, tileId, excludeBiomes, currDepth + 1);
            }
        }
    }
}