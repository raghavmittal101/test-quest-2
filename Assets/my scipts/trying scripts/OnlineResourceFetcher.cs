using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This class is used to fetch metadata information and images submitted by the user online. 
/// This class is used when metadata input type is `online`.
/// </summary>
public class OnlineResourceFetcher : MonoBehaviour
{
    string doc_id;
    string uri;
    public MetadataJson metadataJson { get; private set; }
    public static bool assetsDownloadComplete { get; private set; }
    public static bool jsonFetchComplete {get; private set;}
    public List<Texture> texturesList;
    private bool errorDownloadingImages = false;
    // List<byte[]> downloadedDataBytesList;
    string payloadStr;
    public bool isPayloadRecieved;
   // public MetadataFileInput metadataFileInput;
   
    /// <summary>
    /// any changes to this class should be matched with the expected payload JSON format
    /// </summary>
    class Doc_id
    {
        public string id;
        public Doc_id(string id)
        {
            this.id = id;
        }
    }

    /// <summary>
    /// This is a pseudo-constructor method for this class because constructors are not allowed in `monobehaviour`.
    /// </summary>
    /// <param name="doc_id"></param>
    /// <param name="uri"></param>
    public void _Constructor(string doc_id, string uri)
    {
        this.doc_id = doc_id;
        this.uri = uri;
        assetsDownloadComplete= false;
        jsonFetchComplete = false;
        isPayloadRecieved = false;
}
    /*
    private void Start()
    {
        this.uri = "http://127.0.0.1:5000/metadata";
        this.doc_id = "5";
        StartCoroutine(FetchMetadata_Coroutine());
        StartCoroutine(DownloadAssets());
    }
    */
    /// <summary>
    /// Fetches metadata, downloads the images given in metadata and convert them into textures.
    /// These textures are saved into <see cref="texturesList"/>.
    /// </summary>
    public void FetchAndDownload()
    {
        Debug.Log("1. Fetching and downlaoding resources ...");
        StartCoroutine(FetchAndDownloadResources());
    }

    public IEnumerator FetchAndDownloadResources()
    {
        StartCoroutine(FetchMetadata_Coroutine());
        yield return new WaitUntil(() => jsonFetchComplete);
        StartCoroutine(DownloadAssets_Coroutine());
        yield return new WaitUntil(() => assetsDownloadComplete);
        Debug.Log("13. Fetching and downloading resources complete...");
    }
/*
    public IEnumerator WaitForMetadataComplete()
    {
        yield return new WaitUntil(() => jsonFetchComplete);
    }*/
    public IEnumerator WaitForAssetsDownloadComplete()
    {
        yield return new WaitUntil(() => assetsDownloadComplete);
        // metadataFileInput.metadataJson = this.metadataJson;
        /* metadataFileInput.pathSegmentLength = float.Parse(metadataJson.pathSegmentLength);
        metadataFileInput.visiblePathSegmentCount = int.Parse(metadataJson.visiblePathSegmentCount);
        metadataFileInput.pathWidth = float.Parse(metadataJson.pathWidth);
        metadataFileInput.rayArrayLength = int.Parse(metadataJson.rayArrayLength);
        metadataFileInput.playAreaPadding = float.Parse(metadataJson.playAreaPadding);
        metadataFileInput.imageTexturesList = this.texturesList;
        */
            yield return null;
    }

    public IEnumerator FetchMetadata_Coroutine()
    {
        Debug.Log("2. fetching metadata...");
        yield return StartCoroutine(GetPayloadRequest());
        if (payloadStr != null)
        {
            metadataJson = JsonUtility.FromJson<MetadataJson>(payloadStr);
            jsonFetchComplete = true;
        }
        Debug.Log("8. fetching metadata complete...");
    }

    
    IEnumerator GetPayloadRequest()
    {
        Debug.Log("3. Building request payload...");
        Doc_id id = new Doc_id(this.doc_id);
        string request_json = JsonUtility.ToJson(id);
        UnityWebRequest webRequest = UnityWebRequest.Put(uri, request_json);
        webRequest.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("4. Sending request to server...");
        webRequest.SendWebRequest();
        yield return new WaitUntil(() => webRequest.isDone);
        Debug.Log("5. Response recieved...");
        if (webRequest.isNetworkError)
        {
            Debug.Log("Error: " + webRequest.error);
            payloadStr = null;
        }
        else
        {
            // return null if response is empty
            if (webRequest.GetResponseHeaders() is null) { Debug.Log("Response headers null"); yield return null; }
            Debug.Log("6. fetched data size : " + webRequest.downloadHandler.data.Length);
            Debug.Log("7. fetched data:" + webRequest.downloadHandler.text);
            payloadStr = webRequest.downloadHandler.text;
            isPayloadRecieved = true;
        }
        yield return null;
    }

    /// <summary>
    /// Intended for downloading images only. 
    /// </summary>
    /// <returns></returns>
    IEnumerator DownloadAssets_Coroutine()
    {
        yield return new WaitUntil(() => jsonFetchComplete);
        Debug.Log("9. Downloading assets...");
        foreach (string uri in metadataJson.imageURI){
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            { Debug.Log(request.error); errorDownloadingImages = true; }
            else
            {
                Debug.Log("10. downloading: " + request.url);
                Debug.Log("11. Image download status: " + request.responseCode);
                texturesList.Add(((DownloadHandlerTexture)request.downloadHandler).texture);
            }
        }
        if (!errorDownloadingImages)
            assetsDownloadComplete = true;
        Debug.Log("12. Assets download complete...");
    }    
}
