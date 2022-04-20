using RimWorld;
using Verse;
using Verse.Noise;

namespace TerraCore;

public class GenStep_CaveRockChunks : GenStep
{
    private const float ThreshLooseRock = 0.55f;

    private const float PlaceProbabilityPerCell = 0.006f;

    private const float RubbleProbability = 0.2f;

    private static readonly IntRange MaxRockChunksPerGroup = new IntRange(1, 6);

    private ModuleBase freqFactorNoise;

    public override int SeedPart => 488758298;

    public override void Generate(Map map, GenStepParams parms)
    {
        if (map.TileInfo.WaterCovered)
        {
            return;
        }

        var modExtension = map.Biome.GetModExtension<ModExt_Biome_FeatureControl>();
        if (modExtension == null || modExtension.overwriteRockChunks != RockChunksOverwriteType.AddToCaves)
        {
            return;
        }

        freqFactorNoise = new Perlin(0.014999999664723873, 2.0, 0.5, 6, Rand.Int, QualityMode.Medium);
        freqFactorNoise = new ScaleBias(1.0, 1.0, freqFactorNoise);
        NoiseDebugUI.StoreNoiseRender(freqFactorNoise, "cave_rock_chunks_freq_factor");
        var elevation = MapGenerator.Elevation;
        foreach (var allCell in map.AllCells)
        {
            var num = 0.006f * freqFactorNoise.GetValue(allCell);
            if (elevation[allCell] >= 0.55f && Rand.Value < num)
            {
                GrowLowRockFormationFrom(allCell, map);
            }
        }

        freqFactorNoise = null;
    }

    private void GrowLowRockFormationFrom(IntVec3 root, Map map)
    {
        var filth_RubbleRock = ThingDefOf.Filth_RubbleRock;
        var mineableThing = Find.World.NaturalRockTypesIn(map.Tile).RandomElement().building.mineableThing;
        var random = Rot4.Random;
        var elevation = MapGenerator.Elevation;
        var intVec = root;
        var randomInRange = MaxRockChunksPerGroup.RandomInRange;
        for (var i = 0; i < randomInRange; i++)
        {
            intVec += Rot4Utility.RandomButExclude(random).FacingCell;
            if (!intVec.InBounds(map) || intVec.GetEdifice(map) != null || intVec.GetFirstItem(map) != null ||
                elevation[intVec] < 0.55f)
            {
                break;
            }

            var affordances = map.terrainGrid.TerrainAt(intVec).affordances;
            if (!affordances.Contains(TerrainAffordanceDefOf.Medium) &&
                !affordances.Contains(TerrainAffordanceDefOf.Heavy))
            {
                break;
            }

            GenSpawn.Spawn(mineableThing, intVec, map);
            var adjacentCellsAndInside = GenAdj.AdjacentCellsAndInside;
            foreach (var intVec2 in adjacentCellsAndInside)
            {
                if (Rand.Value < 0.2f)
                {
                    continue;
                }

                var intVec3 = intVec + intVec2;
                if (!intVec3.InBounds(map))
                {
                    continue;
                }

                var emptyTile = false;
                var thingList = intVec3.GetThingList(map);
                foreach (var thing in thingList)
                {
                    if (thing.def.category is ThingCategory.Plant or ThingCategory.Item or ThingCategory.Pawn)
                    {
                        continue;
                    }

                    emptyTile = true;
                    break;
                }

                if (!emptyTile)
                {
                    FilthMaker.TryMakeFilth(intVec3, map, filth_RubbleRock);
                }
            }
        }
    }
}