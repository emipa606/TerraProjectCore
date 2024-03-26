using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Buildable : DefModExtension
{
    public readonly MoistureLevelEnum moistureByKey = MoistureLevelEnum.None;

    public readonly WaterLevelEnum waterByKey = WaterLevelEnum.None;
    public float moisture = -1f;

    public float water = -1f;

    public override IEnumerable<string> ConfigErrors()
    {
        ResolveReferences();
        yield break;
    }

    private void ResolveReferences()
    {
        if (moisture < 0f)
        {
            moisture = MoistureLevel.EnumToFloat(moistureByKey);
        }

        if (water < 0f)
        {
            water = WaterLevel.EnumToFloat(waterByKey);
        }
    }
}