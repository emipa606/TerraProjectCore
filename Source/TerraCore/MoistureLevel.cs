namespace TerraCore;

public static class MoistureLevel
{
    public static readonly float DiffThresholdMinMax = 0.01f;

    public static readonly float None = 0f;

    public static readonly float Min = 0.0001f;

    public static readonly float Max = 0.9999f;

    public static readonly float Unreached = 1f;

    public static readonly float BaseParched = 0.2f;

    public static readonly float BaseDry = 0.4f;

    public static readonly float BaseNormal = 0.6f;

    public static readonly float BaseWet = 0.8f;

    public static readonly float ThresholdMin = None;

    public static readonly float ThresholdParchedDry = 0.3f;

    public static readonly float ThresholdDryNormal = 0.5f;

    public static readonly float ThresholdNormalWet = 0.7f;

    public static readonly float ThresholdMax = Unreached;

    public static readonly float ParchedMin = ThresholdMin;

    public static readonly float ParchedMax = ThresholdParchedDry + DiffThresholdMinMax;

    public static readonly float DryMin = ThresholdParchedDry - DiffThresholdMinMax;

    public static readonly float DryMax = ThresholdDryNormal + DiffThresholdMinMax;

    public static readonly float NormalMin = ThresholdDryNormal - DiffThresholdMinMax;

    public static readonly float NormalMax = ThresholdNormalWet + DiffThresholdMinMax;

    public static readonly float WetMin = ThresholdNormalWet - DiffThresholdMinMax;

    public static readonly float WetMax = ThresholdMax;

    public static float ThresholdEnumToFloat(MoistureLevelMinMaxEnum e)
    {
        return e switch
        {
            MoistureLevelMinMaxEnum.AlwaysMin => None,
            MoistureLevelMinMaxEnum.ParchedMin => ParchedMin,
            MoistureLevelMinMaxEnum.ParchedMax => ParchedMax,
            MoistureLevelMinMaxEnum.DryMin => DryMin,
            MoistureLevelMinMaxEnum.DryMax => DryMax,
            MoistureLevelMinMaxEnum.NormalMin => NormalMin,
            MoistureLevelMinMaxEnum.NormalMax => NormalMax,
            MoistureLevelMinMaxEnum.WetMin => WetMin,
            MoistureLevelMinMaxEnum.WetMax => WetMax,
            MoistureLevelMinMaxEnum.AlwaysMax => Unreached,
            _ => None
        };
    }

    public static float EnumToFloat(MoistureLevelEnum e)
    {
        return e switch
        {
            MoistureLevelEnum.Parched => BaseParched,
            MoistureLevelEnum.Dry => BaseDry,
            MoistureLevelEnum.Normal => BaseNormal,
            MoistureLevelEnum.Wet => BaseWet,
            _ => None
        };
    }
}