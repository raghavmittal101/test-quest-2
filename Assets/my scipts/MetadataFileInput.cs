using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
public class Doc_ID
{
    public int id;
    public Doc_ID(int id)
    {
        this.id = id;
    }
}
*/

public class MetadataFileInput : MonoBehaviour, IMetadataInput
{
    public float pathSegmentLength;
    public int visiblePathSegmentCount;
    public float pathWidth;
    // private readonly Material material;
    public int rayArrayLength;
    public float playAreaPadding;
    // private string returnedPayload;
    //    private Doc_ID doc_id;
    public List<Texture> imageTexturesList;
    OnlineResourceFetcher onlineResourceFetcher;
    public MetadataJson metadataJson;

    public MetadataFileInput(string doc_id, string metadataAPIURI)
    {
        onlineResourceFetcher = GameObject.Find("ScriptObject").GetComponent<OnlineResourceFetcher>();
        onlineResourceFetcher._Constructor(doc_id, metadataAPIURI);
        onlineResourceFetcher.FetchAndDownload();
        Debug.Log("I am here first");
        onlineResourceFetcher.StartCoroutine(onlineResourceFetcher.WaitForAssetsDownloadComplete(this));
        //this.metadataJson = onlineResourceFetcher.metadataJson;
        //this.pathSegmentLength = float.Parse(metadataJson.pathSegmentLength);
        //this.visiblePathSegmentCount = int.Parse(metadataJson.visiblePathSegmentCount);
        //this.pathWidth = float.Parse(metadataJson.pathWidth);
        //this.rayArrayLength = int.Parse(metadataJson.rayArrayLength);
        //this.playAreaPadding = float.Parse(metadataJson.playAreaPadding);
        //this.imageTexturesList = onlineResourceFetcher.texturesList;
        //onlineResourceFetcher.StartCoroutine(WaitForCompleteFetch());
        //StartCoroutine(onlineResourceFetcher.FetchMetadata_Coroutine());        
        // StartCoroutine(onlineResourceFetcher.WaitForAssetsDownloadComplete());
        //StartCoroutine(onlineResourceFetcher.FetchAndDownloadResources());


        Debug.Log("I am here second");

    }

    public IEnumerator WaitForCompleteFetch()
    {
        yield return new WaitUntil(() => OnlineResourceFetcher.assetsDownloadComplete);

    }
/*
    public MetadataFileInput(int doc_id)
    {
        this.doc_id = new Doc_ID(doc_id);
        string json = JsonUtility.ToJson(this.doc_id);
        Debug.Log(json);
        string uri = "http://127.0.0.1:5000/metadata?id=" + doc_id;
        Debug.Log("URI" + uri);
        var rl = new _ResourceLoader();
        returnedPayload = rl.GetDataFromURI(uri);
        Debug.Log(returnedPayload);
        metadataJson = JsonUtility.FromJson<MetadataJson>(returnedPayload);
        this.pathSegmentLength = float.Parse(metadataJson.pathSegmentLength);
        this.visiblePathSegmentCount = int.Parse(metadataJson.visiblePathSegmentCount);
        this.pathWidth = float.Parse(metadataJson.pathWidth);
        this.rayArrayLength = int.Parse(metadataJson.rayArrayLength);
        this.playAreaPadding = float.Parse(metadataJson.playAreaPadding);
      //  this.material = material;
    }
    */
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
//    public Material PathMaterial()
//    {
//        return this.material;
//    }
}
