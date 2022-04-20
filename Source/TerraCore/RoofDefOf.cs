using RimWorld;
using Verse;

namespace TerraCore;

[DefOf]
public static class RoofDefOf
{
    public static RoofDef RoofRockUncollapsable;

    static RoofDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(RoofDefOf));
    }
}