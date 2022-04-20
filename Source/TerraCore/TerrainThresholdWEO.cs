using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class TerrainThresholdWEO
{
    public List<TerrainDef> excludeOverwrite;

    public float max = 1000f;

    public float min = -1000f;
    public TerrainDef terrain;

    public static TerrainDef TerrainAtValue(List<TerrainThresholdWEO> threshes, float val, TerrainDef current)
    {
        if (threshes == null)
        {
            return null;
        }

        foreach (var terrainThresholdWeo in threshes)
        {
            if (!(terrainThresholdWeo.min <= val) || !(terrainThresholdWeo.max >= val))
            {
                continue;
            }

            if (terrainThresholdWeo.excludeOverwrite != null && terrainThresholdWeo.excludeOverwrite.Contains(current))
            {
                return null;
            }

            return terrainThresholdWeo.terrain;
        }

        return null;
    }
}