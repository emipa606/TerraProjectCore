using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

// Token: 0x020000EB RID: 235
public class BiomeWorker_TropicalSwampDeep : BiomeWorker
{
    // Token: 0x060003A3 RID: 931 RVA: 0x00012BA0 File Offset: 0x00010DA0
    public override float GetScore(Tile tile, int tileID)
    {
        var waterCovered = tile.WaterCovered;
        float result;
        if (waterCovered)
        {
            result = -100f;
        }
        else
        {
            var num = BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Temperature, tile.temperature, 15f);
            num *= BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Rainfall, tile.rainfall, 2000f);
            num *= BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Swampiness, tile.swampiness, 0.5f);
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = num * (28f + ((tile.temperature - 20f) * 1.5f) + ((tile.rainfall - 600f) / 165f) +
                    (tile.swampiness * 53f) - 45f);
            }
        }

        return result;
    }
}