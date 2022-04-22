using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetadataInputContext : MonoBehaviour, IMetadataInput
{
    
    private enum metadataInputType { ManualInput, OnlineInput};
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
    public static bool isMetadataFetchComplete;
    private OnlineResourceFetcher onlineResourceFetcher;
    public GameObject ConfIDInputField;
    private enum configurationIDEnteringMethod
    {
        EnterInEditor, 
        EnterInUI
    };

    [SerializeField] private configurationIDEnteringMethod _configurationIDEnteringMethod;

    public void Awake()
    {

        isMetadataFetchComplete = false;

        if (_metadataInputType == metadataInputType.ManualInput)
        {
            this.metadataInput = new MetadataManualInput(pathSegmentLength, visiblePathSegmentCount, pathWidth, 
                rayArrayLength, playAreaPadding, imageTexturesList);
            isMetadataFetchComplete = true;
        }
        else if (_metadataInputType == metadataInputType.OnlineInput)
        {

            switch (_configurationIDEnteringMethod)
            {
                case configurationIDEnteringMethod.EnterInEditor:
                    GameObject.Find("Canvas").SetActive(false);
                    StartCoroutine(StartResourceDownload());
                    break;
                case configurationIDEnteringMethod.EnterInUI:
                    GameObject.Find("Canvas").SetActive(true);
                    // GameObject.Find("TextDisplay").GetComponent<Text>().text = "Loading for " + docId;
                    break;
                default:
                    StartCoroutine(StartResourceDownload());
                    break;
            }
        }

        else Debug.Log("Please choose manual input in metadata input type");
    }
    public void OnDocIDSubmit(){
        this.docId = ConfIDInputField.GetComponent<Text>().text;
        GameObject.Find("TextDisplay").GetComponent<Text>().text = "Loading...";
        StartCoroutine(StartResourceDownload());
    }
    IEnumerator StartResourceDownload()
    {
        //this.metadataInput = new MetadataFileInput(docId, metadataAPIURL);

        onlineResourceFetcher = GameObject.Find("ScriptObject").GetComponent<OnlineResourceFetcher>();
        onlineResourceFetcher._Constructor(docId, metadataAPIURL);
        onlineResourceFetcher.FetchAndDownload();
        yield return new WaitUntil(()=> OnlineResourceFetcher.assetsDownloadComplete);
        this.metadataInput = new MetadataFileInput(onlineResourceFetcher);
        isMetadataFetchComplete = true;
        Debug.Log("Metadata settings done!!!");
    }
    /*
    IEnumerator WaitForMetadataFetchComplete(MetadataFileInput metadataFileInput)
    {
        yield return metadataFileInput.WaitForCompleteFetch();
    }
    */
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
        //return this.imageTexturesList;
        return this.metadataInput.ImageTexturesList();
    }
  //  public Material PathMaterial()
  //  {
  //      return this.metadataInput.PathMaterial();
  //  }
}
