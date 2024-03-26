using System.Collections.Generic;
using Verse;

namespace TerraCore;

public class ModExt_Terrain : DefModExtension
{
    private readonly MoistureLevelMinMaxEnum dryAtByKey = MoistureLevelMinMaxEnum.AlwaysMin;

    private readonly WaterLevelMinMaxEnum ebbAtByKey = WaterLevelMinMaxEnum.AlwaysMin;

    private readonly FertilityLevelMinMaxEnum enrichAtByKey = FertilityLevelMinMaxEnum.AlwaysMax;

    private readonly WaterLevelMinMaxEnum floodAtByKey = WaterLevelMinMaxEnum.AlwaysMax;

    public readonly MoistureLevelEnum moistureByKey = MoistureLevelEnum.None;

    private readonly FertilityLevelMinMaxEnum parchAtByKey = FertilityLevelMinMaxEnum.AlwaysMin;

    public readonly WaterLevelEnum waterByKey = WaterLevelEnum.None;

    private readonly MoistureLevelMinMaxEnum wetAtByKey = MoistureLevelMinMaxEnum.AlwaysMax;
    public TerrainDef destoneTo = null;

    public float dryAt = -1f;

    public TerrainDef dryTo = null;

    public float ebbAt = -1f;

    public TerrainDef ebbTo = null;

    public float enrichAt = -1f;

    public TerrainDef enrichTo = null;
    public float fertilityStorageFactor = 1f;

    public float floodAt = -1f;

    public TerrainDef floodTo = null;

    public float freezeThawOffset = 0f;

    public TerrainDef freezeTo = null;

    public float moisture = -1f;

    public float moistureStorageFactor = 1f;

    public float normalizeAfterDays = 15f;

    public TerrainDef normalizeTo = null;

    public float parchAt = -1f;

    public TerrainDef parchTo = null;

    public TerrainDef plowTo = null;

    public TerrainDef thawTo = null;

    public float water = -1f;

    public float wetAt = -1f;

    public TerrainDef wetTo = null;

    public override IEnumerable<string> ConfigErrors()
    {
        ResolveReferences();
        yield break;
    }

    private void ResolveReferences()
    {
        if (water < 0f)
        {
            water = WaterLevel.EnumToFloat(waterByKey);
        }

        if (moisture < 0f)
        {
            moisture = MoistureLevel.EnumToFloat(moistureByKey);
        }

        if (dryAt < 0f)
        {
            dryAt = MoistureLevel.ThresholdEnumToFloat(dryAtByKey);
        }

        if (wetAt < 0f)
        {
            wetAt = MoistureLevel.ThresholdEnumToFloat(wetAtByKey);
        }

        if (parchAt < 0f)
        {
            parchAt = FertilityLevel.ThresholdEnumToFloat(parchAtByKey);
        }

        if (enrichAt < 0f)
        {
            enrichAt = FertilityLevel.ThresholdEnumToFloat(enrichAtByKey);
        }

        if (ebbAt < 0f)
        {
            ebbAt = WaterLevel.ThresholdEnumToFloat(ebbAtByKey);
        }

        if (floodAt < 0f)
        {
            floodAt = WaterLevel.ThresholdEnumToFloat(floodAtByKey);
        }
    }
}