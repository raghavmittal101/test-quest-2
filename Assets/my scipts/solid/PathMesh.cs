using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMesh
{
    private List<int> triangles;
    private List<Vector3> vertices;
    private List<Vector2> uvs;

    public Mesh GetMesh(List<Vector3> pathPointsLocationsList, float pathWidth, int slices) {
        Mesh mesh = new Mesh();
        //GenerateTrianglesAndUvs(SlicePath(pathPointsLocationsList, slices), pathWidth);
        GenerateTrianglesAndUvs(pathPointsLocationsList, pathWidth);
        //mesh.SetVertices(this.vertices);
        mesh.vertices = this.vertices.ToArray();
        //mesh.SetTriangles(this.triangles, 0);
        mesh.triangles = this.triangles.ToArray();
        mesh.uv = this.uvs.ToArray();
        return mesh;
    }

    //<summary>
    // slice path into smaller fragments of equally divided points.
    // this will enable a more smoother path by increasing the number of triangles
    //</summery>
    private List<Vector3> SlicePath(List<Vector3> pointsList, int slices)
    {
        List<Vector3> slicedPoints = new List<Vector3>();
        slicedPoints.Add(pointsList[0]);
        for (int i = 1; i < pointsList.Count; i++)
        {
            Vector3 diff = Vector3.zero;
            for (int j = 0; j < slices; j++)
            {
                diff += (pointsList[i] - pointsList[i - 1]) / 10;
                slicedPoints.Add(pointsList[i - 1] + diff);
            }
            slicedPoints.Add(pointsList[i]);
        }
        return slicedPoints;
    }

    private void GenerateTrianglesAndUvs(List<Vector3> points, float pathWidth) {     
        int trisIndex = 0;
        int vertIndex = 0;
        this.vertices = new List<Vector3>();
        this.triangles = new List<int>();
        this.uvs = new List<Vector2>();
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
            this.vertices.Add(points[i] + left * pathWidth * 0.5f); // left point
            this.vertices.Add(points[i] - left * pathWidth * 0.5f); // right point

            float completionPercent = i / (float)(points.Count - 1);
            this.uvs.Add(new Vector2(0, i));
            this.uvs.Add(new Vector2(1, i));

            if (i < points.Count - 1)
            {
                // first triangle
                this.triangles.Add(vertIndex);
                this.triangles.Add(vertIndex + 2);
                this.triangles.Add(vertIndex + 1);

                // second triangle
                this.triangles.Add(vertIndex + 1);
                this.triangles.Add(vertIndex + 2);
                this.triangles.Add(vertIndex + 3);

            }
            trisIndex += 6;
            vertIndex += 2;
        }
    }
    private void GenerateUv()
    { 
    }
}
