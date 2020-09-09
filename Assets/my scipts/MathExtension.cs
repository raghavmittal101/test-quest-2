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
}
