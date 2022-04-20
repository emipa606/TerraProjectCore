namespace TerraCore;

public static class FertilityLevel
{
    public static readonly float DiffThresholdMinMax = 0.02f;

    public static readonly float None = 0f;

    public static readonly float Min = 0.0001f;

    public static readonly float Max = 1.9999f;

    public static readonly float Unreached = 2f;

    public static readonly float BaseSand = 0.1f;

    public static readonly float BaseLaterite = 0.45f;

    public static readonly float BaseBarren = 0.75f;

    public static readonly float BaseNormal = 1f;

    public static readonly float BaseFertile = 1.2f;

    public static readonly float BaseRich = 1.35f;

    public static readonly float ThresholdMin = None;

    public static readonly float ThresholdSandLaterite = 0.3f;

    public static readonly float ThresholdLateriteBarren = 0.6f;

    public static readonly float ThresholdBarrenNormal = 0.9f;

    public static readonly float ThresholdNormalFertile = 1.1f;

    public static readonly float ThresholdFertileRich = 1.25f;

    public static readonly float ThresholdMax = Unreached;

    public static readonly float SandMin = ThresholdMin;

    public static readonly float SandMax = ThresholdSandLaterite + DiffThresholdMinMax;

    public static readonly float LateriteMin = ThresholdSandLaterite - DiffThresholdMinMax;

    public static readonly float LateriteMax = ThresholdLateriteBarren + DiffThresholdMinMax;

    public static readonly float BarrenMin = ThresholdLateriteBarren - DiffThresholdMinMax;

    public static readonly float BarrenMax = ThresholdBarrenNormal + DiffThresholdMinMax;

    public static readonly float NormalMin = ThresholdBarrenNormal - DiffThresholdMinMax;

    public static readonly float NormalMax = ThresholdNormalFertile + DiffThresholdMinMax;

    public static readonly float FertileMin = ThresholdNormalFertile - DiffThresholdMinMax;

    public static readonly float FertileMax = ThresholdFertileRich + DiffThresholdMinMax;

    public static readonly float RichMin = ThresholdFertileRich - DiffThresholdMinMax;

    public static readonly float RichMax = ThresholdMax;

    public static float ThresholdEnumToFloat(FertilityLevelMinMaxEnum e)
    {
        return e switch
        {
            FertilityLevelMinMaxEnum.AlwaysMin => None,
            FertilityLevelMinMaxEnum.SandMin => SandMin,
            FertilityLevelMinMaxEnum.SandMax => SandMax,
            FertilityLevelMinMaxEnum.LateriteMin => LateriteMin,
            FertilityLevelMinMaxEnum.LateriteMax => LateriteMax,
            FertilityLevelMinMaxEnum.BarrenMin => BarrenMin,
            FertilityLevelMinMaxEnum.BarrenMax => BarrenMax,
            FertilityLevelMinMaxEnum.NormalMin => NormalMin,
            FertilityLevelMinMaxEnum.NormalMax => NormalMax,
            FertilityLevelMinMaxEnum.FertileMin => FertileMin,
            FertilityLevelMinMaxEnum.FertileMax => FertileMax,
            FertilityLevelMinMaxEnum.RichMin => RichMin,
            FertilityLevelMinMaxEnum.RichMax => RichMax,
            FertilityLevelMinMaxEnum.AlwaysMax => Unreached,
            _ => None
        };
    }

    public static float FertilityEnumToFloat(FertilityLevelEnum e)
    {
        return e switch
        {
            FertilityLevelEnum.Sand => BaseSand,
            FertilityLevelEnum.Laterite => BaseLaterite,
            FertilityLevelEnum.Barren => BaseBarren,
            FertilityLevelEnum.Normal => BaseNormal,
            FertilityLevelEnum.Fertile => BaseFertile,
            FertilityLevelEnum.Rich => BaseRich,
            _ => None
        };
    }
}