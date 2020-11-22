using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataManualInput : IMetadataInput
{
    private readonly float pathSegmentLength;
    private readonly int visiblePathSegmentCount;
    private readonly float pathWidth;
   // private readonly Material material;
    private readonly int rayArrayLength;
    private readonly float playAreaPadding;
    private readonly List<Texture> imageTexturesList;

    public MetadataManualInput(float pathSegmentLength, int visiblePathSegmentCount, float pathWidth, int rayArrayLength, float playAreaPadding, List<Texture> imageTexturesList)
    {
        this.pathSegmentLength = pathSegmentLength;
        this.visiblePathSegmentCount = visiblePathSegmentCount;
        this.pathWidth = pathWidth;
        this.imageTexturesList = imageTexturesList;
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
    
    public List<Texture> ImageTexturesList()
    {
        return this.imageTexturesList;
    }
   // public Material PathMaterial()
   // {
   //     return this.material;
   // }
}
