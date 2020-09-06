using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points
{
    private List<Vector3> pointsList = new List<Vector3>();
    private List<float> pathBetasList = new List<float>();
    private List<Vector3> visiblePointsList = new List<Vector3>();
    private List<float> visiblePathBetasList = new List<float>();
    private const float piByTwo = Mathf.PI / 2;
    private float upperBetaLimit, lowerBetaLimit;

    /* Constructors*/
    public Points()
    {
    }

    /* Methods */
    private void GeneratePoints() {
        /* On start of the game */
        if (this.pointsList.Count == 0) // to make sure that this is the starting of the game
        {
            float alpha; // angle between z-axis and player's initial position vector in radians
            alpha = Mathf.Atan(SceneManager.playerStartingPosition_static.x / SceneManager.playerStartingPosition_static.z);
            this.UpdateBetaRange();
            float beta0 = Random.Range(alpha + this.lowerBetaLimit, alpha + this.upperBetaLimit);
        }
    }
    private void UpdateBetaRange()
    {
        var zoneId = this.GetZoneId();
        getBetaRange(zoneId);
    }

    private getBetaRange(int zoneId)
    {

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
    public List<Vector3> GetPoints()
    {
        this.GeneratePoints();
        return this.presentPoints;
    }

    public List<float> GetPresentBetas()
    {
        return this.presentPathBetas;
    }

    public List<Vector3> GetAllPoints()
    {
        return this.allPoints;
    }
}
