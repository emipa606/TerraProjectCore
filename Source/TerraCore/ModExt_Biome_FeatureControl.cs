using Verse;

namespace TerraCore;

public class ModExt_Biome_FeatureControl : DefModExtension
{
    public readonly int additionalHivesScaling = 0;

    public readonly GenFeatureTristate overwriteCaves = GenFeatureTristate.Default;

    public readonly HiveOverwriteType overwriteHives = HiveOverwriteType.Default;

    public readonly RockChunksOverwriteType overwriteRockChunks = RockChunksOverwriteType.Default;

    public readonly RoofOverwriteType overwriteRoof = RoofOverwriteType.None;

    public readonly bool removeBeach = false;

    public readonly bool removeDebris = false;

    public readonly bool removeRuinsSimple = false;

    public readonly bool removeShrines = false;

    public readonly RoomCalculationType roomCalculationType = RoomCalculationType.Default;
}