using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class BiomeWorkerSpecial_Oasis : BiomeWorkerSpecial
{
    private const int BiomeChangeDigLengthMin = 0;

    public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);

    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.15f);

    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(1, 4);

    protected override float InitialGenChance => 0.06f;

    protected override float GenChanceOffsetAfterFirstHit => 0.04f;

    protected override float GenChancePerHitFactor => 0.7f;

    public override bool PreRequirements(Tile tile)
    {
        if (tile.WaterCovered)
        {
            return false;
        }

        if (tile.hilliness == Hilliness.Impassable)
        {
            return false;
        }

        if (tile.temperature < 15f)
        {
            return false;
        }

        if (tile.rainfall > 600f)
        {
            return false;
        }

        if (tile.swampiness >= 0.5f)
        {
            return false;
        }

        return true;
    }

    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 0, randomInRange, 2);
    }

    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        tile.biome = BiomeDefOf.Oasis;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.Oasis, WLSizeFactor, WLPosOffset);
    }
}