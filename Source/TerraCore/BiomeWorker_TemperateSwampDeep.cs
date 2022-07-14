using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

public class BiomeWorker_TemperateSwampDeep : BiomeWorker
{
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
            var num = BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Temperature, tile.temperature, -10f);
            num *= BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Rainfall, tile.rainfall, 600f);
            num *= BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Swampiness, tile.swampiness, 0.5f);
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = num * (15f + (tile.temperature - 7f) + ((tile.rainfall - 600f) / 180f) +
                    (tile.swampiness * 53f) - 45f);
            }
        }

        return result;
    }
}
