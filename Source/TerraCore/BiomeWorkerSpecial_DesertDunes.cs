using RimWorld.Planet;
using Verse;

namespace TerraCore;

// Token: 0x020000DE RID: 222
public class BiomeWorkerSpecial_DesertDunes : BiomeWorkerSpecial
{
    // Token: 0x040001CF RID: 463
    private const int BiomeChangeDigLengthMin = 0;

    // Token: 0x040001CD RID: 461
    public static readonly FloatRange WLSizeFactor = new FloatRange(0.85f, 1f);

    // Token: 0x040001CE RID: 462
    public static readonly FloatRange WLPosOffset = new FloatRange(0f, 0.15f);

    // Token: 0x040001D0 RID: 464
    private static readonly IntRange BiomeChangeDigLengthMax = new IntRange(1, 4);

    // Token: 0x170000B7 RID: 183
    // (get) Token: 0x06000382 RID: 898 RVA: 0x00012338 File Offset: 0x00010538
    protected override float InitialGenChance => 0.06f;

    // Token: 0x170000B8 RID: 184
    // (get) Token: 0x06000383 RID: 899 RVA: 0x00012350 File Offset: 0x00010550
    protected override float GenChanceOffsetAfterFirstHit => 0.04f;

    // Token: 0x170000B9 RID: 185
    // (get) Token: 0x06000384 RID: 900 RVA: 0x00012368 File Offset: 0x00010568
    protected override float GenChancePerHitFactor => 0.7f;

    // Token: 0x06000385 RID: 901 RVA: 0x00012380 File Offset: 0x00010580
    public override bool PreRequirements(Tile tile)
    {
        var waterCovered = tile.WaterCovered;
        bool result;
        if (waterCovered)
        {
            result = false;
        }
        else
        {
            if (tile.hilliness != Hilliness.Flat)
            {
                result = false;
            }
            else
            {
                if (tile.temperature < 15f)
                {
                    result = false;
                }
                else
                {
                    if (tile.rainfall > 600f)
                    {
                        result = false;
                    }
                    else
                    {
                        result = !(tile.swampiness >= 0.5f);
                    }
                }
            }
        }

        return result;
    }

    // Token: 0x06000386 RID: 902 RVA: 0x00012400 File Offset: 0x00010600
    public override void PostGeneration(int tileID)
    {
        var randomInRange = BiomeChangeDigLengthMax.RandomInRange;
        DigTilesForBiomeChange(tileID, 0, randomInRange, 2);
    }

    // Token: 0x06000387 RID: 903 RVA: 0x00012428 File Offset: 0x00010628
    protected override void ChangeTileAfterSuccessfulDig(Tile tile, bool end)
    {
        tile.biome = BiomeDefOf.DesertDunes;
        GenWorldGen.UpdateTileByBiomeModExts(tile);
    }

    // Token: 0x06000388 RID: 904 RVA: 0x00012440 File Offset: 0x00010640
    public override WLTileGraphicData GetWLTileGraphicData(WorldGrid grid, int tileID)
    {
        return new WLTileGraphicData(WorldMaterials.DesertDunes, WLSizeFactor, WLPosOffset);
    }
}