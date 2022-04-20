using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class BiomeWorkerSpecial_InfestedMountains : BiomeWorkerSpecial
{
    private const int BiomeChangeDigLengthMin = 1;

    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(3, 14);

    protected override float InitialGenChance => 0.06f;

    protected override float GenChanceOffsetAfterFirstHit => 0.045f;

    protected override float GenChancePerHitFactor => 0.6f;

    public override bool PreRequirements(Tile tile)
    {
        if (tile.WaterCovered)
        {
            return false;
        }

        if (tile.hilliness == Hilliness.Flat)
        {
            return false;
        }

        if (tile.temperature is < -10f or > 35f)
        {
            return false;
        }

        if (tile.rainfall < 600f)
        {
            return false;
        }

        return true;
    }

    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 1, randomInRange, 2);
    }

    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        tile.biome = BiomeDefOf.InfestedMountains;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        grid.GetTileGraphicDataFromNeighbors(tileID, out var atlasX, out var atlasZ, out var rotDir,
            (tileFrom, neighbor) => tileFrom.biome == neighbor.biome);
        return new WLTileGraphicData(WorldMaterials.InfestedMountains, atlasX, atlasZ, rotDir);
    }
}