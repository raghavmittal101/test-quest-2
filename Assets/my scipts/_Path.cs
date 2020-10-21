using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
public class _Path
{
    private MetadataInputContext MetadataInput { get { return _ResourceLoader.metadataInput; } }
    private InputDeviceContext InputDevice { get { return _ResourceLoader.inputDevice; } }
    Vector3 startPoint;
    Vector3 endPoint;
    public Vector3 StartPoint { get { return startPoint; } }
    public Vector3 EndPoint { get { return endPoint; } }
    private List<_PathSegment> presentPathSegmentsList;
    public List<_PathSegment>PresentPathSegmentsList{ get{return presentPathSegmentsList;} }
    readonly float maxPathSegments;

    /// <summary>
    /// Initialize a path object.
    /// </summary>
    public _Path()
    {
        this.presentPathSegmentsList = new List<_PathSegment>();
        this.maxPathSegments = MetadataInput.VisiblePathSegmentCount();
    }

    /// <summary>
    /// Add one path segment to the current path.
    /// If current path length is equal to max path length, then first pathsegment is removed and a new one is added towards end.
    /// </summary>
    public void GrowForward() {
        int presentCount = presentPathSegmentsList.Count;
        presentPathSegmentsList.Add(new _PathSegment());
        if (presentCount == maxPathSegments)
        {
            presentPathSegmentsList.RemoveAt(0);
        }

        this.startPoint = presentPathSegmentsList[0].StartPoint;
        this.endPoint = presentPathSegmentsList[presentPathSegmentsList.Count - 1].EndPoint;
    }
}
