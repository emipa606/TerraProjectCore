using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TerraCore;

public class ScattererValidator_ExcludeBiomes : ScattererValidator
{
    public List<BiomeDef> biomes;

    public override bool Allows(IntVec3 c, Map map)
    {
        var biome = map.Biome;
        foreach (var biome2 in biomes)
        {
            if (biome == biome2)
            {
                return false;
            }
        }

        return true;
    }
}