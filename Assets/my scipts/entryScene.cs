using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class entryScene : MonoBehaviour
{
    public GameObject panel;
    void Awake()
    {
        
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("player").transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("PlayerController").transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("UIHelpers").transform.gameObject); // it is needed to show controllers in the art gallery scene
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

    public void togglePannel(){
        if(panel.activeSelf) panel.SetActive(false);
        else panel.SetActive(true);
    }

}
