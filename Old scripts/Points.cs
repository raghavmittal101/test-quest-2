using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Points
{
    private List<Vector3>  pointsList = new List<Vector3>();
    private List<float> pathBetasList = new List<float>();
    private const float pi = Mathf.PI;
    private float upperBetaLimit, lowerBetaLimit;
    private int zoneId;

    /* Constructors*/
    public Points()
    {
    }

    /* Methods */
    private void GeneratePointAndAddToList(bool startOfGame=false) {
        /* On start of the game */
        if (startOfGame)
        {
            this.pointsList.Add(SceneManager.playerStartingPosition_static);
            float alpha; // angle between z-axis and player's initial position vector in radians
            alpha = Mathf.Atan(SceneManager.playerStartingPosition_static.x / SceneManager.playerStartingPosition_static.z);
            this.pathBetasList.Add(alpha);
            GenerateBetaAndAddToList(); // add beta0 to the list
            pathBetasList.RemoveAt(0); // remove alpha because beta0 is now available in the list
        }
        else
        {
            GenerateBetaAndAddToList();
        }
            Vector3 nextPoint = Vector3.zero;
            nextPoint.x = MetadataManager.pathLength_static * Mathf.Sin(this.pathBetasList[SceneManager.numberOfPathSegmentsCovered_static - 1]) + nextPoint.x;
            nextPoint.z = MetadataManager.pathLength_static * Mathf.Cos(this.pathBetasList[SceneManager.numberOfPathSegmentsCovered_static - 1]) + nextPoint.z;
            this.pointsList.Add(nextPoint);
    }

    private void GenerateBetaAndAddToList()
    {
        this.zoneId = this.GetZoneId();
        this.UpdateBetaRange();
        var beta = UnityEngine.Random.Range(this.lowerBetaLimit, this.upperBetaLimit);
        this.pathBetasList.Add(beta);
    }


    private void UpdateBetaRange()
    {
        var B = this.pathBetasList[this.pathBetasList.Count - 1];
        var zoneId = this.zoneId;

        if (this.zoneId == 1) {
            if(MathExtension.InRangeUpperInclusive(B, 0, (pi/2))) {
                this.lowerBetaLimit = (pi/2); this.upperBetaLimit = B + (pi/2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi/2), pi))
            {
                this.lowerBetaLimit = (pi/2); this.upperBetaLimit = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3*(pi/2)))
            {
                this.lowerBetaLimit = B - (pi/2); this.upperBetaLimit = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3*(pi/2), 2*pi-(pi/4)))
            {
                this.lowerBetaLimit = pi; this.upperBetaLimit = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 2*pi-(pi/4), 2*pi))
            {
                this.lowerBetaLimit = (pi/2); this.upperBetaLimit = (pi/2);
            }
        }
        else if (this.zoneId == 2) {
            if (MathExtension.InRangeUpperInclusive(B, 0, pi))
            {
                this.lowerBetaLimit = (pi/2); this.upperBetaLimit = B + (pi/2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi , 2 * pi))
            {
                this.lowerBetaLimit = B - (pi/2); this.upperBetaLimit = 3*(pi/2);
            }
        }
        else if (this.zoneId == 3) {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 4)))
            {
                this.lowerBetaLimit = 3*(pi/2); this.upperBetaLimit = 3 * (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi/4), (pi/2))) {
                this.lowerBetaLimit = pi; this.upperBetaLimit = pi;
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi/2), pi))
            {
                this.lowerBetaLimit = pi; this.upperBetaLimit = B + (pi/2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3*(pi/2)))
            {
                this.lowerBetaLimit = pi; this.upperBetaLimit = 3*(pi/2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi/2), 2*pi))
            {
                this.lowerBetaLimit = B-(pi/2); this.upperBetaLimit = 3*(pi/2);
            }
        }
        else if (this.zoneId == 4) {
            if (MathExtension.InRangeUpperInclusive(B, -(pi/2), (pi/2)))
            {
                this.lowerBetaLimit = B - (pi / 2); this.upperBetaLimit = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi/2, 3 * (pi/2)))
            {
                this.lowerBetaLimit = pi; this.upperBetaLimit = B + (pi / 2);
            }
        }
        else if (this.zoneId == 5) {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 2)))
            {
                this.lowerBetaLimit = B - (pi / 2); this.upperBetaLimit = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), 3*(pi/4)))
            {
                this.lowerBetaLimit = 0; this.upperBetaLimit = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3*(pi / 4), pi))
            {
                this.lowerBetaLimit = 3*(pi/2); this.upperBetaLimit = 3 * (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 3*(pi/2)))
            {
                this.lowerBetaLimit = 3 * (pi / 2); this.upperBetaLimit = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2*pi))
            {
                this.lowerBetaLimit = 3 * (pi / 2); this.upperBetaLimit = pi;
            }
        }
        else if (this.zoneId == 6) {
            if (MathExtension.InRangeUpperInclusive(B, 0, pi))
            {
                this.lowerBetaLimit = B - (pi / 2); this.upperBetaLimit = (pi/2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi / 2, 2*pi))
            {
                this.lowerBetaLimit = 3*(pi/2); this.upperBetaLimit = B + (pi / 2);
            }
        }
        else if (this.zoneId == 7) {
            if (MathExtension.InRangeUpperInclusive(B, 0, (pi / 2)))
            {
                this.lowerBetaLimit = 0; this.upperBetaLimit = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, (pi / 2), pi))
            {
                this.lowerBetaLimit = B - (pi / 2); this.upperBetaLimit = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi, 5*(pi/4)))
            {
                this.lowerBetaLimit = (pi / 2); this.upperBetaLimit = (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, 5 * (pi / 4), 3 * (pi / 2)))
            {
                this.lowerBetaLimit = 0; this.upperBetaLimit = 0;
            }
            else if (MathExtension.InRangeUpperInclusive(B, 3 * (pi / 2), 2*pi))
            {
                this.lowerBetaLimit = 0; this.upperBetaLimit = B + (pi / 2);
            }
        }
        else if (this.zoneId == 8)
        {
            if (MathExtension.InRangeUpperInclusive(B, -(pi/2), (pi/2)))
            {
                this.lowerBetaLimit = 0; this.upperBetaLimit = B + (pi / 2);
            }
            else if (MathExtension.InRangeUpperInclusive(B, pi / 2, 3 * (pi/2)))
            {
                this.lowerBetaLimit = B-(pi/4); this.upperBetaLimit = (pi / 2);
            }
        }
        else if (this.zoneId == 9) { this.lowerBetaLimit = B - (pi / 4); this.upperBetaLimit = B + (pi / 4); }
    }

    private int GetZoneId()
    {
        var p = this.pointsList[this.pointsList.Count-1];
        var d = SceneManager.playAreaDimensions_static;
        var l = MetadataManager.pathLength_static;

        if (p.z < l)
        {
            if (p.x < l) return 7;
            if (p.x <= d.x - l) return 6;
            if (p.x <= d.x) return 5;
        }
        
        if(p.z <= d.z - l)
        {
            if (p.z < l) return 8;
            if (p.x <= d.x - l) return 9;
            if (p.x <= d.x) return 4;
        }

        if (p.z <= d.z)
        {
            if (p.x<l) return 1;
            if (p.x <= d.x - l) return 2;
            if (p.x <= d.x) return 3;
        }

        return -1;
    }

    public Vector3 GetNextPoint(bool startOfGame = false)
    {
        this.GeneratePointAndAddToList(startOfGame);
        return this.pointsList[this.pointsList.Count-1];
    }
}
