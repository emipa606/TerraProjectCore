using System;
using System.Collections;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class WorldLayer_Mountains : WorldLayer
{
    private static readonly IntVec2 TexturesInAtlas = new IntVec2(7, 2);

    public override IEnumerable Regenerate()
    {
        var enumerator = base.Regenerate().GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        finally
        {
            IDisposable disposable;
            if ((disposable = enumerator as IDisposable) != null)
            {
                disposable.Dispose();
            }
        }

        Rand.PushState();
        Rand.Seed = Find.World.info.Seed;
        var grid = Find.WorldGrid;
        var tilesCount = grid.TilesCount;
        for (var disposable = 0; disposable < tilesCount; disposable++)
        {
            var tile = grid[disposable];
            if (tile.hilliness != Hilliness.Impassable && tile.biome != BiomeDefOf.CaveOasis &&
                tile.biome != BiomeDefOf.CaveEntrance && tile.biome != BiomeDefOf.TunnelworldCave)
            {
                continue;
            }

            var hillinessImpassable = WorldMaterials.HillinessImpassable;
            grid.GetTileGraphicDataFromNeighbors(disposable, out var atlasX, out var atlasZ, out var rotDir,
                (_, neighbor) => neighbor.hilliness == Hilliness.Impassable ||
                                 neighbor.biome == BiomeDefOf.CaveOasis ||
                                 neighbor.biome == BiomeDefOf.CaveEntrance ||
                                 neighbor.biome == BiomeDefOf.TunnelworldCave);
            WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(grid, GetSubMesh(hillinessImpassable),
                disposable, atlasX, atlasZ, TexturesInAtlas, rotDir);
            if (tile.hilliness == Hilliness.Impassable)
            {
                continue;
            }

            {
                hillinessImpassable = WorldMaterials.CaveSystem;
                grid.GetTileGraphicDataFromNeighbors(disposable, out atlasX, out atlasZ, out rotDir,
                    (_, neighbor) => neighbor.biome == BiomeDefOf.CaveOasis ||
                                     neighbor.biome == BiomeDefOf.CaveEntrance ||
                                     neighbor.biome == BiomeDefOf.TunnelworldCave);
                WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(grid, GetSubMesh(hillinessImpassable),
                    disposable, atlasX, atlasZ, TexturesInAtlas, rotDir);
            }
        }

        Rand.PopState();
        FinalizeMesh(MeshParts.All);
    }
}