using Verse;

namespace TerraCore;

public class ModExt_Biome_FeatureControl : DefModExtension
{
    public int additionalHivesScaling = 0;

    public GenFeatureTristate overwriteCaves = GenFeatureTristate.Default;

    public HiveOverwriteType overwriteHives = HiveOverwriteType.Default;

    public RockChunksOverwriteType overwriteRockChunks = RockChunksOverwriteType.Default;

    public RoofOverwriteType overwriteRoof = RoofOverwriteType.None;

    public bool removeBeach = false;

    public bool removeDebris = false;

    public bool removeRuinsSimple = false;

    public bool removeShrines = false;

    public RoomCalculationType roomCalculationType = RoomCalculationType.Default;
}