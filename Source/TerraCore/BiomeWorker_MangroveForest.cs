using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

// Token: 0x020000E9 RID: 233
public class BiomeWorker_MangroveForest : BiomeWorker
{
    // Token: 0x0600039F RID: 927 RVA: 0x00012A40 File Offset: 0x00010C40
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
            num *= BiomeWorkerUtility.ScoreFactorHigh(BWScoreFactorType.Elevation, tile.elevation, 7.5f);
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = (num * (35f + ((tile.temperature - 20f) * 1.5f) + ((tile.rainfall - 600f) / 165f))) -
                         tile.elevation;
            }
        }

        return result;
    }
}