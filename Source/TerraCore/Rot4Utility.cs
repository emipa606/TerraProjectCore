using Verse;

namespace TerraCore;

public static class Rot4Utility
{
    public static Rot4 RandomButExclude(Rot4 exclude)
    {
        Rot4 random;
        do
        {
            random = Rot4.Random;
        } while (random == exclude);

        return random;
    }
}