using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
public class _Path
{
    private MetadataInputContext metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();
    Vector3 startPoint;
    Vector3 endPoint;
    public Vector3 StartPoint { get { return startPoint; } }
    public Vector3 EndPoint { get { return endPoint; } }
    private List<PathSegment> presentPathSegmentsList;
    public List<PathSegment>PresentPathSegmentsList{ get{return presentPathSegmentsList;} }
    readonly float maxPathSegments;

    /// <summary>
    /// Initialize a path.
    /// </summary>
    public _Path()
    {
        this.presentPathSegmentsList = new List<PathSegment>();
        this.maxPathSegments = metadataInput.VisiblePathSegmentCount();
    }

    /// <summary>
    /// Generate path in forward direction while maintaining a constant path length
    /// </summary>
    public void GrowForward() {
        int presentCount = presentPathSegmentsList.Count;
        
        if(presentCount == maxPathSegments)
        {
            presentPathSegmentsList.RemoveAt(0);
            presentPathSegmentsList.Add(new PathSegment());
        }
        else if(presentCount == 0)
        {
            // generate first and following path segments
            for (int i=0; i<maxPathSegments; i++)
            {
                presentPathSegmentsList.Add(new PathSegment());
            }
        }

        this.startPoint = presentPathSegmentsList[0].StartPoint;
        this.endPoint = presentPathSegmentsList[presentPathSegmentsList.Count - 1].EndPoint;
    }
}
