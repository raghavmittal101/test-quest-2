using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entryScene : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("player").transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForResourceDownload());
        
    }

    IEnumerator WaitForResourceDownload()
    {
        yield return new WaitUntil(() => OnlineResourceFetcher.assetsDownloadComplete);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
