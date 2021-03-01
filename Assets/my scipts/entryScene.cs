using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class entryScene : MonoBehaviour
{
    void Awake()
    {
        
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("player").transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("PlayerController").transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("FPSComponent").transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(WaitForResourceDownload());
    }

    IEnumerator WaitForResourceDownload()
    {
        yield return new WaitUntil(() => MetadataInputContext.isMetadataFetchComplete);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

}
