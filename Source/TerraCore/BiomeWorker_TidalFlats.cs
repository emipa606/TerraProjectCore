using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

public class BiomeWorker_TidalFlats : BiomeWorker
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
            num *= BiomeWorkerUtility.ScoreFactorHigh(BWScoreFactorType.Elevation, tile.elevation, 7.5f);
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = (num * (22f + (tile.temperature - 7f) + ((tile.rainfall - 600f) / 180f))) - tile.elevation;
            }
        }

        return result;
    }
}
