using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMesh
{
    private List<int> triangles = new List<int>();
    private List<Vector3> pathPointsLocationsList = new List<Vector3>();
    private float pathWidth;
    private List<Vector3> slicedPoints = new List<Vector3>();
    private List<Vector3> vertices = new List<Vector3>();
    private Mesh mesh = new Mesh();
    private List<Vector3> uvs = new List<Vector3>();

    public PathMesh(float pathWidth)
    {
        this.pathWidth = pathWidth;
    }

    public Mesh GetMesh(List<Vector3> pathPointsLocationsList) {
        this.pathPointsLocationsList = pathPointsLocationsList;
        this.SlicePath();
        this.GenerateTrianglesAndUvs();

        this.mesh.SetVertices(this.vertices);
        this.mesh.SetTriangles(this.triangles, 0);
        this.mesh.SetUVs(0, this.uvs);
        return mesh;
    }

    private void SlicePath() {
        for (int i = 1; i < this.pathPointsLocationsList.Count; i++)
        {
            Vector3 diff = Vector3.zero;
            for (int j = 0; j < 10; j++)
            {
                diff += (this.pathPointsLocationsList[i] - this.pathPointsLocationsList[i - 1]) / 10;
                this.slicedPoints.Add(this.pathPointsLocationsList[i - 1] + diff);
            }
            this.slicedPoints.Add(this.pathPointsLocationsList[i]);
        }
    }
    private void GenerateTrianglesAndUvs() {     
        int trisIndex = 0;
        int vertIndex = 0;

        for (int i = 0; i < this.slicedPoints.Count; i++)
        {
            Vector3 forward = Vector3.zero;
            if (i < this.slicedPoints.Count - 1)
            {
                forward += this.slicedPoints[i + 1] - this.slicedPoints[i];
            }
            if (i > 0)
            {
                forward += this.slicedPoints[i] - this.slicedPoints[i - 1];
            }

            forward.Normalize();
            Vector3 left = new Vector3(-forward.z, 0, forward.x);
            this.vertices.Add(this.slicedPoints[i] + left * this.pathWidth * 0.5f); // left point
            this.vertices.Add(this.slicedPoints[i] - left * this.pathWidth * 0.5f); // right point

            float completionPercent = i / (float)(this.slicedPoints.Count - 1);
            this.uvs.Add(new Vector3(0, 0, completionPercent));
            this.uvs.Add(new Vector3(1, 0, completionPercent));

            if (i < this.slicedPoints.Count - 1)
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
    private void GenerateUv() { }
}
