using RimWorld;
using RimWorld.Planet;

namespace TerraCore;

public class BiomeWorker_DesertHighPlains : BiomeWorker
{
    public override float GetScore(Tile tile, int tileID)
    {
        if (tile.WaterCovered)
        {
            return -100f;
        }

        if (tile.temperature < -10f)
        {
            return 0f;
        }

        if (tile.elevation < 1500f)
        {
            return 0f;
        }

        return 10f + (tile.elevation / 100f) + ((600f - tile.rainfall) / 10f);
    }
}