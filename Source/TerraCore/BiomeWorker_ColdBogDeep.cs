using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

public class BiomeWorker_ColdBogDeep : BiomeWorker
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
            num *= BiomeWorkerUtility.ScoreFactorLow(BWScoreFactorType.Swampiness, tile.swampiness, 0.5f);
            if (num == 0f)
            {
                result = 0f;
            }
            else
            {
                result = num * (-tile.temperature + 13f + (tile.swampiness * 58f) - 45f);
            }
        }

        return result;
    }
}
