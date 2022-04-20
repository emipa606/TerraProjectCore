using Verse;

namespace TerraCore;

public class GenStep_ElevationFertilityPost : GenStep
{
    public override int SeedPart => 220686156;

    public override void Generate(Map map, GenStepParams parms)
    {
        var biome = map.Biome;
        if (biome.HasModExtension<ModExt_Biome_GenStep_Spikes>())
        {
            SpikeNoises.WorkOnMapGenerator(map);
        }

        if (biome.HasModExtension<ModExt_Biome_GenStep_Islands>())
        {
            IslandNoises.WorkOnMapGenerator(map);
        }

        if (biome.HasModExtension<ModExt_Biome_GenStep_Ravine>())
        {
            RavineNoises.WorkOnMapGenerator(map);
        }
    }
}