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
    public List<Vector3> presentPathPointsList;
    public List<Vector3>[] presentPathLeftRightPointsArray;
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
    /// Also the left and right points for present paths are caluclated. <seealso cref="SetPathSegmentLeftRightPoints"/>
    /// </summary>
    public void GrowForward() {
        presentPathSegmentsList.Add(new _PathSegment());
        if (presentPathSegmentsList.Count == maxPathSegments)
        {
            presentPathSegmentsList.RemoveAt(0);
        }

        this.startPoint = presentPathSegmentsList[0].StartPoint;
        this.endPoint = presentPathSegmentsList[presentPathSegmentsList.Count - 1].EndPoint;
        this.presentPathLeftRightPointsArray = this.GetLeftRightPoints(presentPathSegmentsList);
    }

    /// <summary>
    /// Populates <see cref="presentPathPointsList"/> list with all the points in current path segment including starting and ending points.
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetPresentPathPoints(List<_PathSegment> presentPathSegmentsList)
    {
        List<Vector3> presentPoints = new List<Vector3>();
        var PresentPathSegmentsListCount = presentPathSegmentsList.Count;
        for (int i=0; i< PresentPathSegmentsListCount; i++)
        {
            presentPoints.Add(presentPathSegmentsList[i].StartPoint);
        }
        presentPoints.Add(presentPathSegmentsList[PresentPathSegmentsListCount - 1].EndPoint);
        return presentPoints;
    }

    /// <summary>
    /// Generates points to repsent the width of path and shape of path. 
    /// Can be used to spawn walls or generate mesh by using returned points as vertices.
    ///  Populates <see cref="presentPathLeftRightPointsArray"/>.
    /// </summary>
    /// <returns>List[List of left points, list of right points] corresponding to input list of points.</returns>
    public List<Vector3>[] GetLeftRightPoints(List<_PathSegment> presentPathSegmentsList)
    {
        List<Vector3> presentPathPointsList = GetPresentPathPoints(presentPathSegmentsList);
        List<Vector3> leftPoints = new List<Vector3>();
        List<Vector3> rightPoints = new List<Vector3>();
        List<Vector3> points = presentPathPointsList;
        var pathWidth = MetadataInput.PathWidth();

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 forward = Vector3.zero;
            if (i < points.Count - 1)
            {
                forward += points[i + 1] - points[i];
            }
            if (i > 0)
            {
                forward += points[i] - points[i - 1];
            }

            forward.Normalize();
            Vector3 left = new Vector3(-forward.z, 0, forward.x);

            leftPoints.Add(points[i] + left * pathWidth * 0.5f); // left point
            rightPoints.Add(points[i] - left * pathWidth * 0.5f); // right point

        }

        List<Vector3>[] array = new List<Vector3>[2];
        array[0] = leftPoints;
        array[1] = rightPoints;

        for (int i = 1; i < leftPoints.Count; i++)
        {
            Debug.DrawLine(leftPoints[i - 1], leftPoints[i], Color.yellow);
            Debug.DrawLine(rightPoints[i - 1], rightPoints[i], Color.yellow);
        }

        presentPathLeftRightPointsArray = array;
        return array;
    }

    /// <summary>
    /// Sets left-right start and end points for each pathsegment in present path.
    /// </summary>
    /*private void SetPathSegmentLeftRightPoints()
    {
        List<Vector3> points = presentPathPointsList;

        var presentPathSegmentsListCount = presentPathSegmentsList.Count;
        List<Vector3>[] LeftRightPoints = GenerateLeftRightPoints();

        // setting the points of pathsegments
        // number of points will always be pathsegmentcount+1
        for(int i=0; i<presentPathSegmentsListCount; i++)
        {
            presentPathSegmentsList[i].LeftRightStartPoint = new Vector3[] { LeftRightPoints[0][i], LeftRightPoints[1][i] };
            presentPathSegmentsList[i].LeftRightEndPoint = new Vector3[] { LeftRightPoints[0][i + 1], LeftRightPoints[1][i + 1] };
        }
                
    }*/
}
