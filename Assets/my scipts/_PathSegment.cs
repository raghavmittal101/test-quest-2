using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathSegment
{
    public Vector3 startPoint{ get; }
    public Vector3 endPoint{ get; }
    /// <summary>
    /// list of path segments generated since start of system
    /// </summary>
    public static List<PathSegment> pathSegmentList { get; }
    /// <summary>
    /// angle in radians between path segment and z-axis
    /// </summary>
    public float rotationAlongYAxis { get; }

    /// <summary>
    /// Generate a new path segment between two points and add it to <see cref="pathSegmentList"/>
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    public PathSegment(Vector3 startPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up)*Mathf.Deg2Rad;
        PathSegment.pathSegmentList.Add(this);
    }

    /// <summary>
    /// Generate a new path segment from given startPoint.
    /// It also adds it to <see cref="pathSegmentList"/>
    /// </summary>
    /// <param name="beta"></param>
    /// <param name="pathSegmentLength"></param>
    /// <param name="playerYaw">head rotation along y-axis in radians</param>
    /// <param name="startPoint">position of player</param>
    public PathSegment(float beta, float pathSegmentLength, float playerYaw, Vector3 startPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = GenerateEndPoint(beta, playerYaw, pathSegmentLength, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        PathSegment.pathSegmentList.Add(this);
    }
    
    /// <summary>
    /// Generate a new path segment connected to previous path segment 
    /// and add it to static <see cref="pathSegmentList"/>.
    /// </summary>
    /// <param name="beta">angle of deviation in radians between heading of previous path segment and new path segment.</param>
    /// <param name="pathSegmentLength"></param>
    /// <returns>generated path segment</returns>
    public PathSegment(float beta, float pathSegmentLength)
    {
        var lastSegment = pathSegmentList[pathSegmentList.Count - 1];
        this.startPoint = lastSegment.endPoint;
        this.endPoint = GenerateEndPoint(beta, lastSegment.rotationAlongYAxis, pathSegmentLength, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        PathSegment.pathSegmentList.Add(this);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="beta"></param>
    /// <param name="rotationAlongYAxis"></param>
    /// <param name="pathSegmentLength"></param>
    /// <param name="startPoint"></param>
    /// <returns></returns>
    private static Vector3 GenerateEndPoint(float beta, float rotationAlongYAxis, float pathSegmentLength, Vector3 startPoint)
    {
        Vector3 endPoint;
        endPoint.x = pathSegmentLength * Mathf.Sin(rotationAlongYAxis + beta) + startPoint.x;
        endPoint.z = pathSegmentLength * Mathf.Cos(rotationAlongYAxis + beta) + startPoint.z;
        endPoint.y = 0f;
        return endPoint;
    }
}
