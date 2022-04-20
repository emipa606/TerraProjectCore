using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class BiomeWorkerSpecial_Archipelago : BiomeWorkerSpecial
{
    private const int BiomeChangeDigLengthMin = 5;

    private const float BiomeChangeChance = 0.2f;

    public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);

    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.15f);

    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(9, 24);

    protected override float InitialGenChance => 0.05f;

    protected override float GenChanceOffsetAfterFirstHit => 0.045f;

    protected override float GenChancePerHitFactor => 0.6f;

    public override bool MinPreRequirements(Tile tile)
    {
        return tile.WaterCovered;
    }

    public override bool PreRequirements(Tile tile)
    {
        if (!tile.WaterCovered || tile.elevation > -40f)
        {
            return false;
        }

        if (tile.temperature is < -10f or >= 20f)
        {
            return false;
        }

        return true;
    }

    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 5, randomInRange, 2);
    }

    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        if (!(Rand.Value < 0.2f))
        {
            return;
        }

        tile.biome = BiomeDefOf.Archipelago;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.Archipelago, WLSizeFactor, WLPosOffset);
    }
}