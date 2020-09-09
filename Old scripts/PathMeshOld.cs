using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMeshOld
{
    private List<Vector3> trianglePoints = new List<Vector3>();
    private List<Vector3> visiblePoints = new List<Vector3>();

    public PathMesh() {
        for(int i = SceneManager.pointsVisibleInScene_static[0]; i <= SceneManager.pointsVisibleInScene_static[1]; i++)
        {
            visiblePoints.Add()
        }
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
