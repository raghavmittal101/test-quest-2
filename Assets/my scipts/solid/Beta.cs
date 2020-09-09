using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beta {
    private float previousBeta; // can be calculate from point;
    private float[] betaRange = { 0f, 0f};
    private Vector3 pointLocation;
    private double beta;
    private int pointZoneId;
    private const float pi = Mathf.PI;

    public Beta(Vector3 pointLocation, int pointZoneId)
    {
        this.pointLocation = pointLocation;
        this.pointZoneId = pointZoneId;
        this.previousBeta = Mathf.Atan(pointLocation.x / pointLocation.z);
        this.GenerateBeta();
    }

    public double GetBeta()
    {
        return this.beta;
    }

    private void GenerateBeta()
    {
        GenerateBetaRange();
        Debug.Log("previous beta: " + this.previousBeta);
        Debug.Log("zoneId: " + this.pointZoneId);
        Debug.Log("Beta: "+ betaRange[0]);
        this.beta = Random.Range((float)this.betaRange[0], (float)this.betaRange[1]);
    }

    private void GenerateBetaRange()
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
}