using RimWorld;

namespace TerraCore;

[DefOf]
public static class BiomeDefOf
{
    public static BiomeDef FrozenLake;
    public static BiomeDef TropicalSwampDeep;
    public static BiomeDef DesertDunes;
    public static BiomeDef PackIce;

    public static BiomeDef SeaIce;

    public static BiomeDef IceSheet;

    public static BiomeDef Tundra;

    public static BiomeDef BorealForest;

    public static BiomeDef ColdBog;

    public static BiomeDef DesertHighPlains;

    public static BiomeDef Savanna;

    public static BiomeDef TemperateForest;

    public static BiomeDef TemperateSwamp;

    public static BiomeDef SpikeMountains;

    public static BiomeDef TropicalRainforest;

    public static BiomeDef TropicalSwamp;

    public static BiomeDef ExtremeDesert;

    public static BiomeDef Desert;

    public static BiomeDef AridShrubland;

    public static BiomeDef Oasis;

    public static BiomeDef CaveOasis;

    public static BiomeDef TunnelworldCave;

    public static BiomeDef CaveEntrance;

    public static BiomeDef InfestedMountains;

    public static BiomeDef DeepRavine;

    public static BiomeDef Archipelago;

    public static BiomeDef VolcanicIsland;

    public static BiomeDef TundraSkerries;

    public static BiomeDef Atoll;

    static BiomeDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BiomeDefOf));
    }
}