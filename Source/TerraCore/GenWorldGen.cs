using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace TerraCore;

public static class GenWorldGen
{
    public static void UpdateTileByBiomeModExts(Tile tile)
    {
        var modExtension = tile.biome.GetModExtension<ModExt_Biome_Replacement>();
        if (modExtension != null)
        {
            modExtension.EnsureDefaultValues();
            if (modExtension.replaceElevation)
            {
                tile.elevation = Rand.RangeInclusive(modExtension.elevationMin, modExtension.elevationMax);
            }

            if (modExtension.replaceHilliness.HasValue)
            {
                tile.hilliness = modExtension.replaceHilliness.Value;
            }
        }

        var modExtension2 = tile.biome.GetModExtension<ModExt_Biome_Temperature>();
        if (modExtension2 != null)
        {
            tile.temperature = (tile.temperature * (1f - modExtension2.tempStableWeight)) +
                               (modExtension2.tempStableValue * modExtension2.tempStableWeight) +
                               modExtension2.tempOffset;
        }
    }

    public static int NextRandomDigDir(int dir, int step)
    {
        step = Mathf.Clamp(step, 1, 3);
        dir += Rand.RangeInclusive(-step, step);
        if (dir < 0)
        {
            dir += 6;
        }

        if (dir > 5)
        {
            dir -= 6;
        }

        return dir;
    }

    public static int InvertDigDir(int dir)
    {
        dir += 3;
        if (dir > 5)
        {
            dir -= 6;
        }

        return dir;
    }
}