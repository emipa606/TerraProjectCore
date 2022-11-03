using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class BiomeWorkerSpecial_Atoll : BiomeWorkerSpecial
{
    private const int BiomeChangeDigLengthMin = 5;

    private const float BiomeChangeChance = 0.3f;

    public static readonly FloatRange WLSizeFactor = new FloatRange(0.95f, 1.25f);

    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.02f);

    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(10, 20);

    protected override float InitialGenChance => 0.06f;

    protected override float GenChanceOffsetAfterFirstHit => 0.055f;

    protected override float GenChancePerHitFactor => 0.38f;

    public override bool MinPreRequirements(Tile tile)
    {
        return tile.WaterCovered;
    }

    public override bool PreRequirements(Tile tile)
    {
        if (!tile.WaterCovered || tile.elevation > -90f)
        {
            return false;
        }

        return !(tile.temperature < 20f);
    }

    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 5, randomInRange, 1);
    }

    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        if (!(Rand.Value < 0.3f))
        {
            return;
        }

        tile.biome = BiomeDefOf.Atoll;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.Atoll, WLSizeFactor, WLPosOffset);
    }
}