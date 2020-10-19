using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathSegment
{
    /// <summary><see cref="MetadataInputContext"/></summary>
    private MetadataInputContext metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();
    /// <summary><see cref="InputDeviceContext"/></summary>
    private InputDeviceContext inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();

    private BoundaryDetector boundaryDetector = new BoundaryDetector();

    private Vector3 startPoint;
    public Vector3 StartPoint { get { return startPoint; } }
    private Vector3 endPoint;
    public Vector3 EndPoint { get { return endPoint; } }
    /// <summary>
    /// list of path segments generated since start of system
    /// </summary>
    public static readonly List<PathSegment> PathSegmentsList;
    /// <summary>
    /// angle in radians between path segment and z-axis
    /// </summary>
    private float rotationAlongYAxis;
    public float RotationAlongYAxis { get { return rotationAlongYAxis; } }

    /// <summary>
    /// Generate a new path segment between two points and add it to <see cref="PathSegmentsList"/>
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    public PathSegment(Vector3 startPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up)*Mathf.Deg2Rad;
        PathSegment.PathSegmentsList.Add(this);
    }

    /// <summary>
    /// Generate a new path segment from given startPoint and add it to <see cref="PathSegmentsList"/>
    /// </summary>
    /// <param name="playerYaw">head rotation along y-axis in radians</param>
    /// <param name="startPoint">position of player</param>
    public PathSegment(float playerYaw, Vector3 startPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = GenerateEndPoint(playerYaw, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        PathSegment.PathSegmentsList.Add(this);
    }

    /// <summary>
    /// Generate a new path segment connected to previous path segment.
    /// and add it to static <see cref="PathSegmentsList"/>.
    /// <para>
    /// When <seealso cref="PathSegmentsList"/> is empty, pathsegment from player's current position is generated.
    /// </para>
    /// </summary>
    public PathSegment()
    {
        var PathSegmentsListCount = PathSegment.PathSegmentsList.Count;
        float initialRotation;
        if (PathSegmentsListCount > 0)
        {
            var lastSegment = PathSegment.PathSegmentsList[PathSegmentsListCount - 1];
            this.startPoint = lastSegment.endPoint;
            initialRotation = lastSegment.rotationAlongYAxis;
        }
        else
        {
            this.startPoint = inputDevice.PlayerPosition(); //player starting position
            initialRotation = inputDevice.PlayerRotationAlongYAxis(); //player head yaw
            
        }
        this.endPoint = GenerateEndPoint(initialRotation, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        PathSegment.PathSegmentsList.Add(this);
    }
    /// <summary>
    /// Calculate the endpoint of the pathsegment by generating a new beta <see cref="DetectBoundaryFixedDirections.GetBeta(Vector3, float)"/>.
    /// </summary>
    /// <param name="rotationAlongYAxis">heading direction of the startPoint. Direction of generated pathsegment will be relative to this.</param>
    /// <param name="startPoint"></param>
    /// <returns></returns>
    private Vector3 GenerateEndPoint(float rotationAlongYAxis, Vector3 startPoint)
    {
        Vector3 endPoint;
        var pathSegmentLength = metadataInput.PathSegmentLength();
        var beta = boundaryDetector.GetBeta(startPoint, rotationAlongYAxis);
        endPoint.x = pathSegmentLength * Mathf.Sin(rotationAlongYAxis + beta) + startPoint.x;
        endPoint.z = pathSegmentLength * Mathf.Cos(rotationAlongYAxis + beta) + startPoint.z;
        endPoint.y = 0f;
        return endPoint;
    }

}
