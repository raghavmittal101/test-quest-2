using UnityEngine;

public static class MathExtension
{
    public static bool InRangeUpperInclusive(this float thisValue, float value1, float value2)
    {
        return thisValue > Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
    }

    public static bool InRangeInclusive(this float thisValue, float value1, float value2)
    {
        return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
    }

    static double NormalizeToRange(double value, double start, double end)
    {
        double width = end - start;   // 
        double offsetValue = value - start;   // value relative to 0

        return (offsetValue - (System.Math.Floor(offsetValue / width) * width)) + start;
        // + start to reset back to start of original range
    }
}
