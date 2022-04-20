using System;
using System.Collections;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class WorldLayer_SpecialBiomes : WorldLayer
{
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
            var biomeWorkerSpecial = tile.biome.WorkerSpecial();
            if (biomeWorkerSpecial == null)
            {
                continue;
            }

            var wLTileGraphicData = biomeWorkerSpecial.GetWLTileGraphicData(grid, disposable);
            if (wLTileGraphicData == null)
            {
                continue;
            }

            var subMesh = GetSubMesh(wLTileGraphicData.material);
            if (wLTileGraphicData.drawAsQuad)
            {
                var tileCenter = grid.GetTileCenter(disposable);
                var pos = (tileCenter + (Rand.UnitVector3 * wLTileGraphicData.posOffset * grid.averageTileSize))
                    .normalized * tileCenter.magnitude;
                WorldRendererUtility.PrintQuadTangentialToPlanetWithRodation(pos, tileCenter,
                    wLTileGraphicData.sizeFactor * grid.averageTileSize, 0.005f, subMesh,
                    wLTileGraphicData.rotVector);
                RimWorld.Planet.WorldRendererUtility.PrintTextureAtlasUVs(wLTileGraphicData.atlasX,
                    wLTileGraphicData.atlasZ, wLTileGraphicData.texturesInAtlasX,
                    wLTileGraphicData.texturesInAtlasZ, subMesh);
            }
            else
            {
                WorldRendererUtility.DrawTileTangentialToPlanetWithRodation(
                    texturesInAtlas: new IntVec2(wLTileGraphicData.texturesInAtlasX,
                        wLTileGraphicData.texturesInAtlasZ), grid: grid, subMesh: subMesh, tileID: disposable,
                    atlasX: wLTileGraphicData.atlasX, atlasZ: wLTileGraphicData.atlasZ,
                    rotDir: wLTileGraphicData.rotDir);
            }
        }

        Rand.PopState();
        FinalizeMesh(MeshParts.All);
    }
}