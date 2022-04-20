using RimWorld.Planet;
using Verse;

namespace TerraCore;

// Token: 0x02000167 RID: 359
public class BiomeWorkerSpecial_FrozenLake : BiomeWorkerSpecial
{
    // Token: 0x040003E4 RID: 996
    private const int BiomeChangeDigLengthMin = 0;

    // Token: 0x040003E2 RID: 994
    public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);

    // Token: 0x040003E3 RID: 995
    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.15f);

    // Token: 0x040003E5 RID: 997
    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(1, 4);

    // Token: 0x17000138 RID: 312
    // (get) Token: 0x06000628 RID: 1576 RVA: 0x00020494 File Offset: 0x0001E694
    protected override float InitialGenChance => 0.06f;

    // Token: 0x17000139 RID: 313
    // (get) Token: 0x06000629 RID: 1577 RVA: 0x000204AC File Offset: 0x0001E6AC
    protected override float GenChanceOffsetAfterFirstHit => 0.04f;

    // Token: 0x1700013A RID: 314
    // (get) Token: 0x0600062A RID: 1578 RVA: 0x000204C4 File Offset: 0x0001E6C4
    protected override float GenChancePerHitFactor => 0.7f;

    // Token: 0x0600062B RID: 1579 RVA: 0x000204DC File Offset: 0x0001E6DC
    public override bool PreRequirements(Tile tile)
    {
        var waterCovered = tile.WaterCovered;
        if (waterCovered)
        {
            return false;
        }

        if (tile.hilliness == Hilliness.Impassable)
        {
            return false;
        }

        if (tile.temperature > -10f)
        {
            return false;
        }

        return !(tile.rainfall < 600f);
    }

    // Token: 0x0600062C RID: 1580 RVA: 0x00020540 File Offset: 0x0001E740
    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 0, randomInRange, 2);
    }

    // Token: 0x0600062D RID: 1581 RVA: 0x00020568 File Offset: 0x0001E768
    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        tile.biome = BiomeDefOf.FrozenLake;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    // Token: 0x0600062E RID: 1582 RVA: 0x00020580 File Offset: 0x0001E780
    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.FrozenLake, WLSizeFactor, WLPosOffset);
    }
}