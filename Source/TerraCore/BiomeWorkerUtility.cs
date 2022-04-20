using UnityEngine;
using Verse;

namespace TerraCore;

// Token: 0x0200017B RID: 379
public static class BiomeWorkerUtility
{
    // Token: 0x0400042F RID: 1071
    private static readonly float fixedThresholdVarianceTemperature = 3f;

    // Token: 0x04000430 RID: 1072
    private static readonly SimpleCurve thresholdVarianceCurveRainfall = new SimpleCurve
    {
        {
            0f,
            0f
        },
        {
            340f,
            40f
        },
        {
            600f,
            75f
        },
        {
            10000f,
            800f
        }
    };

    // Token: 0x04000431 RID: 1073
    private static readonly float fixedThresholdVarianceSwampiness = 0.08f;

    // Token: 0x04000432 RID: 1074
    private static readonly SimpleCurve thresholdVarianceCurveElevation = new SimpleCurve
    {
        {
            0f,
            0f
        },
        {
            1.5f,
            0.1f
        },
        {
            1500f,
            100f
        },
        {
            5000f,
            300f
        }
    };

    // Token: 0x0600066C RID: 1644 RVA: 0x00021ED4 File Offset: 0x000200D4
    private static float ThresholdVarianceForType(BWScoreFactorType type, float threshold)
    {
        float result;
        switch (type)
        {
            case BWScoreFactorType.Temperature:
                result = fixedThresholdVarianceTemperature;
                break;
            case BWScoreFactorType.Rainfall:
                result = thresholdVarianceCurveRainfall.Evaluate(threshold);
                break;
            case BWScoreFactorType.Swampiness:
                result = fixedThresholdVarianceSwampiness;
                break;
            case BWScoreFactorType.Elevation:
                result = thresholdVarianceCurveElevation.Evaluate(threshold);
                break;
            default:
                Log.Error(
                    $"ThresholdVarianceForType tried to get threshold variance for non-existing type {type} and threshold {threshold}");
                result = 0f;
                break;
        }

        return result;
    }

    // Token: 0x0600066D RID: 1645 RVA: 0x00021F68 File Offset: 0x00020168
    public static float ScoreFactorLow(BWScoreFactorType type, float value, float threshold)
    {
        var num = ThresholdVarianceForType(type, threshold);
        return Mathf.Clamp01((value - threshold - num) / num / 2f);
    }

    // Token: 0x0600066E RID: 1646 RVA: 0x00021F94 File Offset: 0x00020194
    public static float ScoreFactorHigh(BWScoreFactorType type, float value, float threshold)
    {
        var num = ThresholdVarianceForType(type, threshold);
        return Mathf.Clamp01((threshold + num - value) / num / 2f);
    }

    // Token: 0x0600066F RID: 1647 RVA: 0x00021FC0 File Offset: 0x000201C0
    public static float ScoreFactorLow(float value, float low, float high)
    {
        return Mathf.Clamp01((value - low) / (high - low));
    }

    // Token: 0x06000670 RID: 1648 RVA: 0x00021FE0 File Offset: 0x000201E0
    public static float ScoreFactorHigh(float value, float low, float high)
    {
        return Mathf.Clamp01((high - value) / (high - low));
    }
}