using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
/// <summary>
/// Contains methods for finding scripts and loading resources required by other classes.
/// This is done to avoid issues which occur due to asynchronous running of Awake methods of different classes.
/// I tried to fix the issue by changing the excecution order of Awake methods by it didn't work especially in case of loading the prefabs.
/// </summary>
/*
 * Keep all the prefabs at "Assets/resources/prefabs/" location.
 * Keep all the images at "Assets/resources/images/" location.
 *
 */
public class _ResourceLoader : MonoBehaviour
{
    /// <summary><see cref="MetadataInputContext"/></summary>
    public static MetadataInputContext metadataInput;

    /// <summary><see cref="InputDeviceContext"/></summary>
    public static InputDeviceContext inputDevice;

    public static GameObject spawner_boundaryColliderPrefab;
    public static GameObject spawner_wallPrefab;
    public static GameObject spawner_photoFramePrefab;
    public static GameObject spawner_triggerColliderPrefab;
    public static Light spawner_pointLight;
    public static List<Texture> imagesList = new List<Texture>();
    public static string requestData;

    private void Awake()
    {
        SetMetadataInputContext();
        SetInputDeviceContext();
        LoadSpawnerResources();
    }
    void SetMetadataInputContext()
    {
        
        metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();
    }
    void SetInputDeviceContext()
    {
        inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();
    }

    bool allImagesLoaded;
    void LoadSpawnerResources()
    {
        spawner_boundaryColliderPrefab = Resources.Load("Prefabs/WallCollidersPrefab") as GameObject;
        spawner_wallPrefab = Resources.Load("Prefabs/WallPrefab") as GameObject;
        spawner_photoFramePrefab = (GameObject)Resources.Load("Prefabs/PhotoFramePrefab", typeof(GameObject));
        spawner_triggerColliderPrefab = (GameObject)Resources.Load("Prefabs/PathTriggerColliderPrefab", typeof(GameObject));
        spawner_pointLight = (Light)Resources.Load("Prefabs/PointLight", typeof(Light));
        StartCoroutine(LoadImageResources());
        new WaitUntil(() => allImagesLoaded);   
    }
    IEnumerator LoadImageResources()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo("Assets/Resources/Pictures/paintings/");
        FileInfo[] files;
        
        files = directoryInfo.GetFiles("*");
            
       
        //catch { files = directoryInfo.GetFiles("*.jpeg"); }

        foreach (FileInfo file in files) { yield return StartCoroutine(LoadFile(file)); }
        allImagesLoaded = true;
    }

    /// <summary>
    /// Loads the file, waits until it is fully loaded and then adds it to <see cref="imagesList"/>.
    /// </summary>
    IEnumerator LoadFile(FileInfo file)
    {
        if (file.Name.EndsWith("meta"))
        {
            yield break;
        }
        string filePath = file.FullName.ToString();
        string url = string.Format("file://{0}", filePath);
        WWW www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);
        Debug.Log(file);
        Debug.Log(www.bytesDownloaded);
        imagesList.Add(www.texture);
        
    }

    public string GetDataFromURI(string uri)
    {
        StartCoroutine(GetRequest(uri));
        return requestData;
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return new WaitUntil(()=>webRequest.isDone);
        if (webRequest.isNetworkError)
        {
            Debug.Log("Error: " + webRequest.error);
            requestData = null;
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            requestData = webRequest.downloadHandler.text;
        }
    }
    
}
