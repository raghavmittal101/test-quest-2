using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineResourceFetcher : MonoBehaviour
{
    string doc_id;
    string uri;
    public MetadataJson metadataJson { get; private set; }
    public bool allImagesFetched {get; private set;}
    public bool jsonFetchComplete = false;
    public List<Texture> texturesList { get; private set; }
    // List<byte[]> downloadedDataBytesList;
    string payloadStr;


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
        allImagesFetched = false;
        jsonFetchComplete = false;
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
    /// </summary>
    /// <returns>List containing textures</returns>
    public List<Texture> GetTexturesList()
    {
        Debug.Log("Downloading textures ...");
        StartCoroutine(FetchMetadata_Coroutine());
        StartCoroutine(DownloadAssets());
        Debug.Log("Texture download complete...");
        return texturesList;
    }

    public MetadataJson FetchMetadata()
    {
        StartCoroutine(FetchMetadata_Coroutine());
        return metadataJson;
    }

    public IEnumerator FetchMetadata_Coroutine()
    {
        Debug.Log("fetching metadata...");
        yield return StartCoroutine(GetPayloadRequest());
        if (payloadStr != null)
        {
            metadataJson = JsonUtility.FromJson<MetadataJson>(payloadStr);
            jsonFetchComplete = true;
        }
        Debug.Log("fetching metadata complete...");
    }
    
    /// <summary>
    /// Intended for downloading images only. 
    /// </summary>
    /// <returns></returns>
    IEnumerator DownloadAssets()
    {
        yield return new WaitUntil(() => jsonFetchComplete);
        for (int i = 0; i < metadataJson.imageURI.Length; i++)
        {
            Debug.Log(metadataJson.imageURI[i]);
        }
        Debug.Log("Downloading assets...");
        foreach (string uri in metadataJson.imageURI){
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else {
                Debug.Log("downloading: " + request.url);
                Debug.Log("Image download status: " + request.responseCode);
                texturesList.Add(((DownloadHandlerTexture)request.downloadHandler).texture);
                allImagesFetched = true;
            }
        }
        Debug.Log("Assets download complete...");
    }
    /*
    Texture ConvertByteToTexture(byte[] data)
    {
        
        var texture = new Texture2D(1, 1);
        texture.LoadRawTextureData(data);
        return texture;
    }
    */
    IEnumerator GetPayloadRequest()
    {
        // string json = "{ 'id':'"+doc_id+"' }";
        //       Dictionary<string, string> headers = new Dictionary<string, string>();
        //       headers.Add("Content-Type", "application/json");
        //        json = json.Replace("'", "\"");
        //        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        Debug.Log("Building request payload...");
        Doc_id id = new Doc_id(this.doc_id);
        string request_json = JsonUtility.ToJson(id);
        UnityWebRequest webRequest = UnityWebRequest.Put(uri, request_json);
        webRequest.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("Sending request to server...");
        webRequest.SendWebRequest();
        yield return new WaitUntil(() => webRequest.isDone);
        Debug.Log("Response recieved...");
        if (webRequest.isNetworkError)
        {
            Debug.Log("Error: " + webRequest.error);
            payloadStr = null;
        }
        else
        {
            // return null if response is empty
            if (webRequest.GetResponseHeaders() is null) { Debug.Log("Response headers null"); yield return null; }
            Debug.Log("fetched data size : " + webRequest.downloadHandler.data.Length);
            Debug.Log("fetched data:" + webRequest.downloadHandler.text);
            payloadStr = webRequest.downloadHandler.text;
        }
        yield return null;
    }

 
    
}
