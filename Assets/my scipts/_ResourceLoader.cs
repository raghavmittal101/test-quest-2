using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        foreach (FileInfo file in directoryInfo.GetFiles("*.jpg")) { yield return StartCoroutine(LoadFile(file)); }
        allImagesLoaded = true;
    }

    /// <summary>
    /// Loads the file, waits until it is fully loaded and then adds it to <see cref="imagesList"/>.
    /// </summary>
    IEnumerator LoadFile(FileInfo file)
    {
        string filePath = file.FullName.ToString();
        string url = string.Format("file://{0}", filePath);
        WWW www = new WWW(filePath);
        yield return new WaitUntil(() => www.isDone);
        Debug.Log(file);
        Debug.Log(www.bytesDownloaded);
        imagesList.Add(www.texture);
        
    }
}
