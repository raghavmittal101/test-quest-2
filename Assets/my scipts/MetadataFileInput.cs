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

public class MetadataFileInput : IMetadataInput
{
    private float pathSegmentLength;
    private int visiblePathSegmentCount;
    private float pathWidth;
    private int rayArrayLength;
    private float playAreaPadding;
    public List<Texture> imageTexturesList;
    private string subjectId;
    //OnlineResourceFetcher onlineResourceFetcher;
    //MetadataJson metadataJson;
    // public MetadataJson metadataJson;
    

        //metadataJson = new MetadataJson();
    
    public MetadataFileInput(OnlineResourceFetcher onlineResourceFetcher)
    {
        /*
        onlineResourceFetcher._Constructor(doc_id, metadataAPIURI);
        onlineResourceFetcher.FetchAndDownload();
        Debug.Log("I am here first");
        onlineResourceFetcher.StartCoroutine(onlineResourceFetcher.WaitForAssetsDownloadComplete());
        onlineResourceFetcher = onlineResourceFetcher.GetComponent<OnlineResourceFetcher>();
        */
        //metadataJson = onlineResourceFetcher.metadataJson;
        this.pathSegmentLength = float.Parse(onlineResourceFetcher.metadataJson.pathSegmentLength);
        this.visiblePathSegmentCount = int.Parse(onlineResourceFetcher.metadataJson.visiblePathSegmentCount);
        this.pathWidth = float.Parse(onlineResourceFetcher.metadataJson.pathWidth);
        this.rayArrayLength = int.Parse(onlineResourceFetcher.metadataJson.rayArrayLength);
        this.playAreaPadding = float.Parse(onlineResourceFetcher.metadataJson.playAreaPadding);
        this.imageTexturesList = onlineResourceFetcher.texturesList;
        this.subjectId = onlineResourceFetcher.metadataJson.subjectId;
        //onlineResourceFetcher.StartCoroutine(WaitForCompleteFetch());
        //StartCoroutine(onlineResourceFetcher.FetchMetadata_Coroutine());        
        // StartCoroutine(onlineResourceFetcher.WaitForAssetsDownloadComplete());
        //StartCoroutine(onlineResourceFetcher.FetchAndDownloadResources());

    }
    
   /* public IEnumerator WaitForCompleteFetch()
    {
        yield return new WaitUntil(() => OnlineResourceFetcher.assetsDownloadComplete);

    }
    */
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
    public string SubjectId()
    {
        return this.subjectId;
    }
//    public Material PathMaterial()
//    {
//        return this.material;
//    }
}
