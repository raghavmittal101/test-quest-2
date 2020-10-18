using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
public class _Path : MonoBehaviour
{
    Vector3 startPoint { get; }
    Vector3 endPoint { get; }
    List<PathSegment> presentPathSegmentsList { get; }
    float maxPathSegments;

    public _Path(int maxPathSegments, Vector3 startPoint)
    {
        this.presentPathSegmentsList = new List<PathSegment>();
        this.startPoint = startPoint;
        this.maxPathSegments = maxPathSegments;
        this.endPoint = startPoint;
    }

    void GrowForwardPath() {
        int presentCount = presentPathSegmentsList.Count;
        if(presentCount == maxPathSegments)
        {
            presentPathSegmentsList.RemoveAt(0);
            presentPathSegmentsList.Add(new PathSegment(beta, length));
        }
        else if(presentCount == 0)
        {
            // generate first path segment
            presentPathSegmentsList.Add(new PathSegment());

            // generate other path segments
            for (int i=0; i<maxPathSegments-1; i++)
            {
                // generate remaining path segments
                presentPathSegmentsList.Add(new PathSegment(beta, length));
            }
        }
    }
}
