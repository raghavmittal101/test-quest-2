using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment
{
    private Vector3 startLocation;
    private Vector3 endLocation;
    private float pathSegmentLength;
    private float beta;

    public PathSegment(Vector3 startLocation, float pathSegmentLength, float beta)
    {
        this.startLocation = startLocation;
        this.pathSegmentLength = pathSegmentLength;
        this.beta = beta;
        this.GenerateEndLocation();
    }

    public Vector3 GetStartLocation()
    {
        return this.startLocation;
    }

    public Vector3 GetEndLocation()
    {
        return this.endLocation;
    }

    private void GenerateEndLocation()
    {
        this.endLocation = Vector3.zero;
        this.endLocation.x = this.pathSegmentLength * Mathf.Sin(this.beta) + this.startLocation.x;
        this.endLocation.z = this.pathSegmentLength * Mathf.Cos(this.beta) + this.startLocation.z;
    }
}