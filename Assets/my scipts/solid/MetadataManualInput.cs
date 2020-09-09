using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataManualInput : MonoBehaviour, IMetadataInput
{
    [SerializeField]
    private float pathSegmentLength;

    [SerializeField]
    private int visiblePathSegmentCount;

    [SerializeField]
    private float pathWidth;

    [SerializeField]
    private Material material;

    public float PathSegmentLength()
    {
        return this.pathSegmentLength;
    }

    public int VisiblePathSegmentCount()
    {
        return this.visiblePathSegmentCount;
    }
    public float PathWidth()
    {
        return this.pathWidth;
    }
    public Material Material()
    {
        return this.material;
    }
}
