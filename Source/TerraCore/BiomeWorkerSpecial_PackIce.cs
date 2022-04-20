using RimWorld;
using RimWorld.Planet;
using Verse;

namespace TerraCore;

// Token: 0x02000166 RID: 358
public class BiomeWorkerSpecial_PackIce : BiomeWorkerSpecial
{
    // Token: 0x040003DF RID: 991
    private const int BiomeChangeDigLengthMin = 5;

    // Token: 0x040003E1 RID: 993
    private const float BiomeChangeChance = 0.3f;

    // Token: 0x040003DD RID: 989
    public static readonly FloatRange WLSizeFactor = new FloatRange(1f, 1.2f);

    // Token: 0x040003DE RID: 990
    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.06f);

    // Token: 0x040003E0 RID: 992
    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(8, 20);

    // Token: 0x17000135 RID: 309
    // (get) Token: 0x0600061E RID: 1566 RVA: 0x00020318 File Offset: 0x0001E518
    protected override float InitialGenChance => 0.05f;

    // Token: 0x17000136 RID: 310
    // (get) Token: 0x0600061F RID: 1567 RVA: 0x00020330 File Offset: 0x0001E530
    protected override float GenChanceOffsetAfterFirstHit => 0.042f;

    // Token: 0x17000137 RID: 311
    // (get) Token: 0x06000620 RID: 1568 RVA: 0x00020348 File Offset: 0x0001E548
    protected override float GenChancePerHitFactor => 0.7f;

    // Token: 0x06000621 RID: 1569 RVA: 0x00020360 File Offset: 0x0001E560
    public override bool MinPreRequirements(Tile tile)
    {
        return tile.WaterCovered;
    }

    // Token: 0x06000622 RID: 1570 RVA: 0x00020378 File Offset: 0x0001E578
    public override bool PreRequirements(Tile tile)
    {
        bool result;
        if (!tile.WaterCovered || tile.elevation > -100f)
        {
            result = false;
        }
        else
        {
            if (tile.temperature > -5f)
            {
                result = false;
            }
            else
            {
                result = !(BiomeWorker_IceSheet.PermaIceScore(tile) > 23f);
            }
        }

        return result;
    }

    // Token: 0x06000623 RID: 1571 RVA: 0x000203D8 File Offset: 0x0001E5D8
    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 5, randomInRange, 1);
    }

    // Token: 0x06000624 RID: 1572 RVA: 0x00020400 File Offset: 0x0001E600
    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        if (!(Rand.Value < 0.3f))
        {
            return;
        }

        tile.biome = BiomeDefOf.PackIce;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    // Token: 0x06000625 RID: 1573 RVA: 0x00020434 File Offset: 0x0001E634
    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.PackIce, WLSizeFactor, WLPosOffset);
    }
}