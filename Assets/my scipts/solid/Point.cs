using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Point
{
    private Vector3 location;
    private int zoneId;
    private float pathLength;
    private Vector3 playAreaDimension;


    public int GetZoneId(Vector3 location, Vector3 playAreaDimension, float pathLength)
    {
        this.location = location;
        this.pathLength = pathLength;
        this.playAreaDimension = playAreaDimension;
        this.zoneId = this.CalculateZoneId();
        return this.zoneId;
    }

    private int CalculateZoneId()
    {
        var p = this.location;
        var d = this.playAreaDimension;
        var l = this.pathLength;

        Debug.Log("location: " + p);
        Debug.Log("play area dimension: " + d);
        Debug.Log("path length: " + l);

        if (p.z < l)
        {
            if (p.x < l) return 7;
            if (p.x <= d.x - l) return 6;
            if (p.x <= d.x) return 5;
        }

        if (p.z <= d.z - l)
        {
            if (p.z < l) return 8;
            if (p.x <= d.x - l) return 9;
            if (p.x <= d.x) return 4;
        }

        if (p.z <= d.z)
        {
            if (p.x < l) return 1;
            if (p.x <= d.x - l) return 2;
            if (p.x <= d.x) return 3;
        }

        return -1;
    }
}