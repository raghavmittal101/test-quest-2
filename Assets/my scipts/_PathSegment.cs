using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class _PathSegment : MonoBehaviour
{
    private BoundaryDetector boundaryDetector = new BoundaryDetector();

    private Vector3 startPoint;
    public Vector3 StartPoint { get { return startPoint; } }
    private Vector3 endPoint;
    public Vector3 EndPoint { get { return endPoint; } }
    /// <summary>
    /// list of path segments generated since start of system
    /// </summary>
    public static List<_PathSegment> PathSegmentsList = new List<_PathSegment>();
    /// <summary>
    /// angle in radians between path segment and z-axis
    /// </summary>
    private float rotationAlongYAxis;
    public float RotationAlongYAxis { get { return rotationAlongYAxis; } }

    private MetadataInputContext MetadataInput { get { return _ResourceLoader.metadataInput; } }
    private InputDeviceContext InputDevice { get { return _ResourceLoader.inputDevice; } }
    private void Awake()
    { 
    }


    /// <summary>
    /// Generate a new path segment between two points and add it to <see cref="PathSegmentsList"/>
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    public _PathSegment(Vector3 startPoint, Vector3 endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up)*Mathf.Deg2Rad;
            _PathSegment.PathSegmentsList.Add(this);
        }

    /// <summary>
    /// Generate a new path segment from given startPoint and add it to <see cref="PathSegmentsList"/>
    /// </summary>
    /// <param name="playerYaw">head rotation along y-axis in radians</param>
    /// <param name="startPoint">position of player</param>
    public _PathSegment(float playerYaw, Vector3 startPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = GenerateEndPoint(playerYaw, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        _PathSegment.PathSegmentsList.Add(this);
    }

    /// <summary>
    /// Generate a new path segment connected to previous path segment.
    /// and add it to static <see cref="PathSegmentsList"/>.
    /// <para>
    /// When <seealso cref="PathSegmentsList"/> is empty, pathsegment from player's current position is generated.
    /// </para>
    /// </summary>
    public _PathSegment()
    {
        var PathSegmentsListCount = PathSegmentsList.Count;
        float initialRotation;
        if (PathSegmentsListCount > 0)
        {
            var lastSegment = PathSegmentsList[PathSegmentsListCount - 1];
            this.startPoint = lastSegment.endPoint;
            initialRotation = lastSegment.rotationAlongYAxis;
        }
        else
        {
            this.startPoint = InputDevice.PlayerPosition(); //player starting position
            initialRotation = InputDevice.PlayerRotationAlongYAxis(); //player head yaw
            
        }
        this.endPoint = GenerateEndPoint(initialRotation, startPoint);
        this.rotationAlongYAxis = Vector3.SignedAngle(Vector3.forward, (endPoint - startPoint), Vector3.up) * Mathf.Deg2Rad;
        _PathSegment.PathSegmentsList.Add(this);
    }
    /// <summary>
    /// Calculate the endpoint of the pathsegment by generating a new beta <see cref="DetectBoundaryFixedDirections.GetBeta(Vector3, float)"/>.
    /// </summary>
    /// <param name="rotationAlongYAxis">heading direction of the startPoint in radians. Direction of generated pathsegment will be relative to this.</param>
    /// <param name="startPoint"></param>
    /// <returns></returns>
    private Vector3 GenerateEndPoint(float rotationAlongYAxis, Vector3 startPoint)
    {
        Vector3 endPoint;
        var pathSegmentLength = MetadataInput.PathSegmentLength();
        Debug.Log("_PathSegment.cs: GenerateEndPoint(): rotationAlongYAxis:" + rotationAlongYAxis * Mathf.Rad2Deg);
        var beta = boundaryDetector.GetBeta(startPoint, rotationAlongYAxis);
        Debug.Log("_PathSegment.cs: GenerateEndPoint(): beta:" + beta * Mathf.Rad2Deg);
        endPoint.x = pathSegmentLength * Mathf.Sin(rotationAlongYAxis + beta) + startPoint.x;
        endPoint.z = pathSegmentLength * Mathf.Cos(rotationAlongYAxis + beta) + startPoint.z;
        endPoint.y = 0f;
        return endPoint;
    }

}
