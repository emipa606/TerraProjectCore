using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class BiomeWorkerSpecial_SpikeMountains : BiomeWorkerSpecial
{
    private const int BiomeChangeDigLengthMin = 0;

    public static readonly FloatRange WLSizeFactor = new FloatRange(1f, 1.2f);

    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.1f);

    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(3, 11);

    protected override float InitialGenChance => 0.06f;

    protected override float GenChanceOffsetAfterFirstHit => 0.035f;

    protected override float GenChancePerHitFactor => 0.7f;

    public override bool PreRequirements(Tile tile)
    {
        if (tile.WaterCovered)
        {
            return false;
        }

        if (tile.hilliness is Hilliness.Flat or Hilliness.Impassable)
        {
            return false;
        }

        if (tile.temperature < -10f)
        {
            return false;
        }

        if (tile.rainfall < 1200f)
        {
            return false;
        }

        if (tile.swampiness >= 0.6f)
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
        tile.biome = BiomeDefOf.SpikeMountains;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.SpikeMountains, WLSizeFactor, WLPosOffset);
    }
}