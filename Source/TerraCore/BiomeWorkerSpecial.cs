using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

public abstract class BiomeWorkerSpecial : BiomeWorker
{
    protected float genChance;

    protected virtual float InitialGenChance => 0f;

    protected virtual float GenChanceOffsetAfterFirstHit => 0f;

    protected virtual float GenChancePerHitFactor => 0f;

    protected virtual float GenChancePerHitOffset => 0f;

    public override float GetScore(Tile tile, int tileID)
    {
        return -100f;
    }

    public virtual bool MinPreRequirements(Tile tile)
    {
        return !tile.WaterCovered;
    }

    public virtual bool PreRequirements(Tile tile)
    {
        return false;
    }

    public virtual void PostGeneration(int tileID)
    {
    }

    public void ResetChance()
    {
        genChance = InitialGenChance;
    }

    public bool TryGenerateByChance()
    {
        if (!(Rand.Value < genChance))
        {
            return false;
        }

        if (genChance == InitialGenChance)
        {
            genChance -= GenChanceOffsetAfterFirstHit;
        }

        genChance = (genChance * GenChancePerHitFactor) - GenChancePerHitOffset;
        return true;
    }

    protected virtual void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
    }

    protected void DigTilesForBiomeChange(int startTileID, int digLengthMin, int digLengthMax, int maxDirChange,
        bool digBothDirections = true)
    {
        var worldGrid = Find.WorldGrid;
        var invertDigDir = false;
        var tileID = startTileID;
        var dir = Rand.RangeInclusive(0, 5);
        for (var i = 0; i < digLengthMax; i++)
        {
            var dir2 = GenWorldGen.NextRandomDigDir(dir, maxDirChange);
            tileID = worldGrid.GetTileNeighborByDirection6WayInt(tileID, dir2);
            var tile = worldGrid[tileID];
            if ((i >= digLengthMin || !MinPreRequirements(tile)) && !PreRequirements(tile))
            {
                if (invertDigDir)
                {
                    break;
                }

                tileID = startTileID;
                dir = GenWorldGen.InvertDigDir(dir);
                i = -1;
                invertDigDir = true;
                continue;
            }

            if (tile.biome.WorkerSpecial() == null)
            {
                var end = i == digLengthMax - 1;
                ChangeTileAfterSuccessfulDig(tile, end);
            }

            if (!digBothDirections || i != digLengthMax - 1 || invertDigDir)
            {
                continue;
            }

            tileID = startTileID;
            dir = GenWorldGen.InvertDigDir(dir);
            i = -1;
            invertDigDir = true;
        }
    }

    public virtual WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return null;
    }
}