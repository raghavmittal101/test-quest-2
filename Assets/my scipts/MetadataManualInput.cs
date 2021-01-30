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
    private readonly string subjectId;

    public MetadataManualInput(float pathSegmentLength, int visiblePathSegmentCount, float pathWidth, int rayArrayLength, float playAreaPadding, List<Texture> imageTexturesList, string subjectId)
    {
        this.pathSegmentLength = pathSegmentLength;
        this.visiblePathSegmentCount = visiblePathSegmentCount;
        this.pathWidth = pathWidth;
        this.imageTexturesList = imageTexturesList;
        this.rayArrayLength = rayArrayLength;
        this.playAreaPadding = playAreaPadding;
        this.subjectId = subjectId;
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

    public string SubjectId()
    {
        return this.subjectId;
    }
   // public Material PathMaterial()
   // {
   //     return this.material;
   // }
}
