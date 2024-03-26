using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

public class BiomeWorker_TemperateMeadow : BiomeWorker
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
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = num * (16f + (tile.temperature - 7f) + ((tile.rainfall - 600f) / 180f) -
                                (tile.swampiness * 50f));
            }
        }

        return result;
    }
}