using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public Material material;

    [Range(0.05f, 5f)]
    public float roadWidth = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateMesh(List<Vector3> points)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int>tris = new List<int>();
        List<Vector3> uvs = new List<Vector3>();
        int trisIndex = 0;
        int vertIndex = 0;

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
            verts.Add(points[i] + left * roadWidth * 0.5f); // left point
            verts.Add(points[i] - left * roadWidth * 0.5f); // right point
                                                            // float completionPercent = i / (float)(points.Count - 1);
                                                            // uvs.Add(new Vector3(0, 0, completionPercent));
                                                            // uvs.Add(new Vector3(1, 0, completionPercent));

            if (i < points.Count - 1)
            {
                // first triangle
                tris.Add(vertIndex);
                tris.Add(vertIndex + 2);
                tris.Add(vertIndex + 1);

                // second triangle
                tris.Add(vertIndex + 1);
                tris.Add(vertIndex + 2);
                tris.Add(vertIndex + 3);

            }

            trisIndex += 6;
            vertIndex += 2;
        }
            
        Debug.Log(verts.Count +":"+ vertIndex +" "+ trisIndex);

        for (int k = 0; k < tris.Count; k++)
        {
            Debug.Log(tris[k]);
        }
        Mesh mesh = new Mesh();
        mesh.SetVertices(verts);
        //mesh.vertices = verts;
        mesh.SetTriangles(tris, 0);
        //        mesh.triangles = tris;
        //mesh.SetUVs(0, uvs, 0, uvs.Count);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;

    }
}
