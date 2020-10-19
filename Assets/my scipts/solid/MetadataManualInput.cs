using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataManualInput : IMetadataInput
{
    private readonly float pathSegmentLength;
    private readonly int visiblePathSegmentCount;
    private readonly float pathWidth;
    private readonly Material material;
    private readonly int rayArrayLength;
    private readonly float playAreaPadding;

    public MetadataManualInput(float pathSegmentLength, int visiblePathSegmentCount, float pathWidth, Material material, int rayArrayLength, float playAreaPadding)
    {
        this.pathSegmentLength = pathSegmentLength;
        this.visiblePathSegmentCount = visiblePathSegmentCount;
        this.pathWidth = pathWidth;
        this.material = material;
        this.rayArrayLength = rayArrayLength;
        this.playAreaPadding = playAreaPadding;
    }
    public float PlayAreaPadding()
    {
        return this.playAreaPadding;
    }
    public int RayArrayLength()
    {
        return this.rayArrayLength;
    }
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
    public Material PathMaterial()
    {
        return this.material;
    }
}
