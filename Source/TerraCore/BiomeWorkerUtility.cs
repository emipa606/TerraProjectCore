using UnityEngine;
using Verse;

namespace TerraCore;

public static class BiomeWorkerUtility
{
    private static readonly float fixedThresholdVarianceTemperature = 3f;

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

    private static readonly float fixedThresholdVarianceSwampiness = 0.08f;

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

    public static float ScoreFactorLow(BWScoreFactorType type, float value, float threshold)
    {
        var num = ThresholdVarianceForType(type, threshold);
        return Mathf.Clamp01((value - threshold - num) / num / 2f);
    }

    public static float ScoreFactorHigh(BWScoreFactorType type, float value, float threshold)
    {
        var num = ThresholdVarianceForType(type, threshold);
        return Mathf.Clamp01((threshold + num - value) / num / 2f);
    }

    public static float ScoreFactorLow(float value, float low, float high)
    {
        return Mathf.Clamp01((value - low) / (high - low));
    }

    public static float ScoreFactorHigh(float value, float low, float high)
    {
        return Mathf.Clamp01((high - value) / (high - low));
    }
}
