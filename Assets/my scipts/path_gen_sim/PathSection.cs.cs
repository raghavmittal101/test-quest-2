using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSection
{
    public Material material;
    public float width;
    List<Vector3> verts = new List<Vector3>();
    List<int> tris = new List<int>();
    List<Vector3> uvs = new List<Vector3>();
    int trisIndex=0;
    int vertIndex=0;
}
