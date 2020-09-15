using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _Point
{
    public static int GetZoneId(this Vector3 location, Vector3 playAreaDimension, float pathLength)
    {
        var p = location;
        var d = playAreaDimension;
        var l = pathLength;

       // Debug.Log("location: " + p);
      //  Debug.Log("play area dimension: " + d);
       // Debug.Log("path length: " + l);

        if (p.z < l)
        {
            if (p.x < l) return 1;
            if (p.x <= d.x - l) return 8;
            if (p.x <= d.x) return 2;
        }

        if (p.z <= d.z - l)
        {
            if (p.z < l) return 5;
            if (p.x <= d.x - l) return 9;
            if (p.x <= d.x) return 7;
        }

        if (p.z <= d.z)
        {
            if (p.x < l) return 3;
            if (p.x <= d.x - l) return 6;
            if (p.x <= d.x) return 4;
        }

        return -1;
    }

    public static int OldGetZoneId(this Vector3 location, Vector3 playAreaDimension, float pathLength)
    {
        var p = location;
        var d = playAreaDimension;
        var l = pathLength;

        // Debug.Log("location: " + p);
       // Debug.Log("play area dimension: " + d);
        // Debug.Log("path length: " + l);

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