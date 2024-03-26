using RimWorld.Planet;
using Verse;

namespace TerraCore;

public class ModExt_Biome_Replacement : DefModExtension
{
    public static readonly ModExt_Biome_Replacement Default = new ModExt_Biome_Replacement();

    public readonly int elevationMax = 100;

    public readonly int elevationMin = 10;

    public readonly bool replaceElevation = false;

    public TerrainDef gravelReplacement;

    public Hilliness? replaceHilliness = null;

    public TerrainDef sandReplacement;

    public void EnsureDefaultValues()
    {
        if (gravelReplacement == null)
        {
            gravelReplacement = TerrainDefOf.NormalGravel;
        }

        if (sandReplacement == null)
        {
            sandReplacement = TerrainDefOf.NormalSand;
        }
    }
}