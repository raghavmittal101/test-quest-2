using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMesh
{
    private List<Vector3> trianglePoints = new List<Vector3>();
    private Points _points;

    private List<Vector3> presentPoints;


    public PathMesh(Points _points) {
        this._points = _points;
    }

    private void GenerateTrianglePoints()
    /* 
    * Generates triangle points representing a mesh.
    * Input : List of points representing a line; Width of path.
    * Output: List of Mesh triangle points.
    * 
    * Later on we will update this to build mesh whith different path segment sections
    */
    {
        // do something
        this.presentPoints = _points.GetPoints();
        // generate mesh
    }

    public List<Vector3> GetTrianglePoints()
    {
        return this.trianglePoints;
    }

    public void Render()
    {
        this.GenerateTrianglePoints();
        // if there is existing mesh then update the mesh else create mesh
        // and do something to render mesh
    }
}
