﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beta
{
    /*
    private float previousBeta; // can be calculate from point;
    private float[] betaRange = { 0f, 0f};
    private Vector3 pointLocation;
    private float beta;
    private int pointZoneId;
    private const float pi = 180f;
    */
    public static float GenerateBeta(Vector3 pointLocation, int pointZoneId)
    {
        //Debug.Log("Beta.cs: GenerateBeta: pointLocation: " + pointLocation.x + "," + pointLocation.z);
        float previousBeta = Mathf.Round(Mathf.Atan(pointLocation.x / pointLocation.z)*10000)/10000; // rounded to 4 places after decimal to avoid any floating point errors
        Debug.Log("Beta.cs: GenerateBeta: previousBeta: " + previousBeta*Mathf.Rad2Deg);
        float[] betaRange = Beta.GenerateBetaRange(previousBeta, pointZoneId);
        Debug.Log("Beta.cs: GenerateBeta: BetaRange: " + betaRange[0] + "," + betaRange[1]);
        return Random.Range(betaRange[0]*Mathf.Rad2Deg, betaRange[1]*Mathf.Rad2Deg);
    }

    private float NormalizedRad(float theta)
    {
        float _theta;
        _theta = Mathf.Deg2Rad * (theta % 360);
        return _theta;
    }

    private static float[] GenerateBetaRange(float previousBeta, int pointZoneId)
    {
        float[] betaRange = new float[2];

        int a; // category of previous beta based on it's position in unit circle
        const float pi = Mathf.PI;
        if (MathExtension.InRangeUpperInclusive(previousBeta, 0, pi / 2)) a = 1;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, pi / 2, pi)) a = 2;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, pi, 3 * (pi / 2))) a = 3;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 0, 2 * pi)) a = 4;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, pi / 2, 3 * (pi / 4))) a = 5;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 5 * (pi / 4), 3 * (pi / 2))) a = 6;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, pi, 5 * (pi / 4))) a = 7;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 7 * (pi / 4), 2 * pi)) a = 8;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 3 * (pi / 2), 7 * (pi / 4))) a = 9;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, pi / 4, pi / 2)) a = 10;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 3 * (pi / 4), pi)) a = 11;
        else if (MathExtension.InRangeUpperInclusive(previousBeta, 0, pi / 4)) a = 12;

        else { a = 1; Debug.Log("Beta.cs: GenerateBetaRange(): unable to calculate 'a'. Set to 1 for now !"); }

        // FOR ZONE 9
        if (pointZoneId == 9)
        {
            betaRange[0] = previousBeta - (pi / 2);
            betaRange[1] = previousBeta + (pi / 2);
        }
        // FOR ZONES 1 to 4 and a = 1 to 4
        else if (pointZoneId > 0 && pointZoneId < 5 && a > 0 && a < 5)
        {

            if (a == pointZoneId)
            {
                betaRange[0] = previousBeta - (pi / 2);
                betaRange[1] = (a - 1) * (pi / 2);
            }
            else if (Mathf.Abs(a - pointZoneId) == 2)
            {
                betaRange[0] = (a - 1) * (pi / 2);
                betaRange[1] = previousBeta + (pi / 2);
            }
            else if (Mathf.Abs(pointZoneId - a) % 2 != 0)
            {
                betaRange[0] = (a - 1) * (pi / 2);
                betaRange[1] = a * (pi / 2);
            }
        }

        // FOR ZONES 5 to 8 and a = 1 to 4
        else if (pointZoneId > 4 && pointZoneId < 9 && a > 0 && a < 5)
        {
            if (Mathf.Abs(a - pointZoneId) == 4)
            {
                betaRange[0] = (a - 1) * (pi / 2);
                betaRange[1] = previousBeta + (pi / 2);
            }
            else if (Mathf.Abs(a - pointZoneId) % 2 == 0)
            {
                betaRange[0] = previousBeta - (pi / 2);
                betaRange[1] = (a - 1) * (pi / 2);
            }
            else if (Mathf.Abs(a - pointZoneId) % 2 != 0)
            {
                betaRange[0] = previousBeta - (pi / 2);
                betaRange[1] = a * (pi / 2);
            }
        }

        // FOR ZONES 1 to 4 and a = 5 to 8
        else if (pointZoneId > 0 && pointZoneId < 5 && a > 4 && a < 9)
        {
            betaRange[0] = pi / 2 * (Mathf.Abs(a - pointZoneId) - 8);
            betaRange[1] = pi / 2 * (Mathf.Abs(a - pointZoneId) - 8);
        }

        else { betaRange[0] = 0f; betaRange[1] = pi; Debug.Log("Beta.cs: GenerateBetaRange(): Unable to find a valid betaRange. Set to [0, pi] for now !"); }

        return betaRange;

    }
}
    /*
    private void OldGenerateBetaRange()
    {
        float B = this.previousBeta;
        var zoneId = this.pointZoneId;

        if (zoneId == 1)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 2)))
            {
                this.betaRange[0] = (pi / 2); this.betaRange[1] = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), pi))
            {
                this.betaRange[0] = (pi / 2); this.betaRange[1] = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3 * (pi / 2)))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2 * pi - (pi / 4)))
            {
                this.betaRange[0] = pi; this.betaRange[1] = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 2 * pi - (pi / 4), 2 * pi))
            {
                this.betaRange[0] = (pi / 2); this.betaRange[1] = (pi / 2);
            }
        }
        else if (zoneId == 2)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, pi))
            {
                this.betaRange[0] = (pi / 2); this.betaRange[1] = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 2 * pi))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = 3 * (pi / 2);
            }
        }
        else if (zoneId == 3)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 4)))
            {
                this.betaRange[0] = 3 * (pi / 2); this.betaRange[1] = 3 * (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 4), (pi / 2)))
            {
                this.betaRange[0] = pi; this.betaRange[1] = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), pi))
            {
                this.betaRange[0] = pi; this.betaRange[1] = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3 * (pi / 2)))
            {
                this.betaRange[0] = pi; this.betaRange[1] = 3 * (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2 * pi))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = 3 * (pi / 2);
            }
        }
        else if (zoneId == 4)
        {
            if (MathExtension.InRangeUpperInclusive(B, -(pi / 2), (pi / 2)))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi / 2, 3 * (pi / 2)))
            {
                this.betaRange[0] = pi; this.betaRange[1] = B + (pi / 2);
            }
        }
        else if (zoneId == 5)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 2)))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), 3 * (pi / 4)))
            {
                this.betaRange[0] = 0; this.betaRange[1] = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 4), pi))
            {
                this.betaRange[0] = 3 * (pi / 2); this.betaRange[1] = 3 * (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3 * (pi / 2)))
            {
                this.betaRange[0] = 3 * (pi / 2); this.betaRange[1] = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2 * pi))
            {
                this.betaRange[0] = 3 * (pi / 2); this.betaRange[1] = pi;
            }
        }
        else if (zoneId == 6)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, pi))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi / 2, 2 * pi))
            {
                this.betaRange[0] = 3 * (pi / 2); this.betaRange[1] = B + (pi / 2);
            }
        }
        else if (zoneId == 7)
        {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 2)))
            {
                this.betaRange[0] = 0; this.betaRange[1] = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), pi))
            {
                this.betaRange[0] = B - (pi / 2); this.betaRange[1] = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 5 * (pi / 4)))
            {
                this.betaRange[0] = (pi / 2); this.betaRange[1] = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 5 * (pi / 4), 3 * (pi / 2)))
            {
                this.betaRange[0] = 0; this.betaRange[1] = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2 * pi))
            {
                this.betaRange[0] = 0; this.betaRange[1] = B + (pi / 2);
            }
        }
        else if (zoneId == 8)
        {
            if (MathExtension.InRangeUpperInclusive(B, -(pi / 2), (pi / 2)))
            {
                this.betaRange[0] = 0; this.betaRange[1] = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi / 2, 3 * (pi / 2)))
            {
                this.betaRange[0] = B - (pi / 4); this.betaRange[1] = (pi / 2);
            }
        }
        else if (zoneId == 9) { this.betaRange[0] = B - (pi / 4); this.betaRange[1] = B + (pi / 4); }
    }
    */