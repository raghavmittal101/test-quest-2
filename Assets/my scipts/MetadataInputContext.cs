using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetadataInputContext : MonoBehaviour, IMetadataInput
{
    
    private enum metadataInputType { ManualInput, FileInput};
    [SerializeField] private metadataInputType _metadataInputType;
    [SerializeField] private float pathSegmentLength;
    [SerializeField] private int visiblePathSegmentCount;
    [SerializeField] private float pathWidth;
//    [SerializeField] private Material material;
    [SerializeField] private int rayArrayLength;
    [SerializeField] private float playAreaPadding;
    [SerializeField] private string docId;
    [SerializeField] private string metadataAPIURL;
    [SerializeField] private List<Texture> imageTexturesList;

    public IMetadataInput metadataInput;

    public void Awake()
    {
        if (_metadataInputType == metadataInputType.ManualInput)
        {
            this.metadataInput = new MetadataManualInput(pathSegmentLength, visiblePathSegmentCount, pathWidth, 
                rayArrayLength, playAreaPadding, imageTexturesList);
        }
        else if (_metadataInputType == metadataInputType.FileInput)
        {
            this.metadataInput = new MetadataFileInput(docId, metadataAPIURL);
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
    public List<Texture> ImageTexturesList()
    {
        return this.imageTexturesList;
    }
  //  public Material PathMaterial()
  //  {
  //      return this.metadataInput.PathMaterial();
  //  }
}
