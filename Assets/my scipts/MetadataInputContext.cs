using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataInputContext : MonoBehaviour, IMetadataInput
{
    
    private enum metadataInputType { ManualInput, MetadataFileNotWorking};
    [SerializeField] private metadataInputType _metadataInputType;
    [SerializeField] private float pathSegmentLength;
    [SerializeField] private int visiblePathSegmentCount;
    [SerializeField] private float pathWidth;
    [SerializeField] private Material material;
    [SerializeField] private int rayArrayLength;
    [SerializeField] private float playAreaPadding;

    public IMetadataInput metadataInput;

    public void Awake()
    {
        if (_metadataInputType == metadataInputType.ManualInput)
        {
            this.metadataInput = new MetadataManualInput(pathSegmentLength, visiblePathSegmentCount, pathSegmentLength, 
                material, rayArrayLength, playAreaPadding);
        }

        else Debug.Log("Please choose manual input in metadata input type");
    }

    public float PlayAreaPadding()
    {
        return this.metadataInput.PlayAreaPadding();
    }
    public int RayArrayLength()
    {
        return this.metadataInput.RayArrayLength();
    }
    public float PathSegmentLength()
    {
        return this.metadataInput.PathSegmentLength();
    }
    public int VisiblePathSegmentCount()
    {
        return this.metadataInput.VisiblePathSegmentCount();
    }
    public float PathWidth() {
        return this.metadataInput.PathWidth();
    }
    public Material PathMaterial()
    {
        return this.metadataInput.PathMaterial();
    }
}
