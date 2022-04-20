namespace TerraCore;

public static class WaterLevel
{
    public static readonly float DiffThresholdMinMax = 0.01f;

    public static readonly float None = 0f;

    public static readonly float Min = 0f;

    public static readonly float Max = 8f;

    public static readonly float BaseWet = 0.01f;

    public static readonly float BaseFlooded = 0.1f;

    public static readonly float BaseShallow = 0.35f;

    public static readonly float BaseHipDeep = 0.7f;

    public static readonly float BaseChestDeep = 1.15f;

    public static readonly float BaseSloping = 1.7f;

    public static readonly float BaseDeep = 3f;

    public static readonly float ThresholdMin = None;

    public static readonly float ThresholdWetFlooded = 0.02f;

    public static readonly float ThresholdFloodedShallow = 0.2f;

    public static readonly float ThresholdShallowHipDeep = 0.5f;

    public static readonly float ThresholdHipDeepChestDeep = 0.9f;

    public static readonly float ThresholdChestDeepSloping = 1.4f;

    public static readonly float ThresholdSlopingDeep = 2f;

    public static readonly float ThresholdMax = Max;

    public static readonly float WetMin = ThresholdMin;

    public static readonly float WetMax = ThresholdWetFlooded + DiffThresholdMinMax;

    public static readonly float FloodedMin = ThresholdWetFlooded - DiffThresholdMinMax;

    public static readonly float FloodedMax = ThresholdFloodedShallow + DiffThresholdMinMax;

    public static readonly float ShallowMin = ThresholdFloodedShallow - DiffThresholdMinMax;

    public static readonly float ShallowMax = ThresholdShallowHipDeep + DiffThresholdMinMax;

    public static readonly float HipDeepMin = ThresholdShallowHipDeep - DiffThresholdMinMax;

    public static readonly float HipDeepMax = ThresholdHipDeepChestDeep + DiffThresholdMinMax;

    public static readonly float ChestDeepMin = ThresholdHipDeepChestDeep - DiffThresholdMinMax;

    public static readonly float ChestDeepMax = ThresholdChestDeepSloping + DiffThresholdMinMax;

    public static readonly float SlopingMin = ThresholdChestDeepSloping - DiffThresholdMinMax;

    public static readonly float SlopingMax = ThresholdSlopingDeep + DiffThresholdMinMax;

    public static readonly float DeepMin = ThresholdSlopingDeep - DiffThresholdMinMax;

    public static readonly float DeepMax = ThresholdMax;

    public static float ThresholdEnumToFloat(WaterLevelMinMaxEnum e)
    {
        return e switch
        {
            WaterLevelMinMaxEnum.AlwaysMin => None,
            WaterLevelMinMaxEnum.WetMin => WetMin,
            WaterLevelMinMaxEnum.WetMax => WetMax,
            WaterLevelMinMaxEnum.FloodedMin => FloodedMin,
            WaterLevelMinMaxEnum.FloodedMax => FloodedMax,
            WaterLevelMinMaxEnum.ShallowMin => ShallowMin,
            WaterLevelMinMaxEnum.ShallowMax => ShallowMax,
            WaterLevelMinMaxEnum.HipDeepMin => HipDeepMin,
            WaterLevelMinMaxEnum.HipDeepMax => HipDeepMax,
            WaterLevelMinMaxEnum.ChestDeepMin => ChestDeepMin,
            WaterLevelMinMaxEnum.ChestDeepMax => ChestDeepMax,
            WaterLevelMinMaxEnum.SlopingMin => SlopingMin,
            WaterLevelMinMaxEnum.SlopingMax => SlopingMax,
            WaterLevelMinMaxEnum.DeepMin => DeepMin,
            WaterLevelMinMaxEnum.DeepMax => DeepMax,
            WaterLevelMinMaxEnum.AlwaysMax => Max,
            _ => None
        };
    }

    public static float EnumToFloat(WaterLevelEnum e)
    {
        return e switch
        {
            WaterLevelEnum.Wet => BaseWet,
            WaterLevelEnum.Flooded => BaseFlooded,
            WaterLevelEnum.Shallow => BaseShallow,
            WaterLevelEnum.HipDeep => BaseHipDeep,
            WaterLevelEnum.ChestDeep => BaseChestDeep,
            WaterLevelEnum.Sloping => BaseSloping,
            WaterLevelEnum.Deep => BaseDeep,
            _ => None
        };
    }
}