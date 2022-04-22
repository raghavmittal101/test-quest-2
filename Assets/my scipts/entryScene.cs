using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class entryScene : MonoBehaviour
{
    private enum autoContinueToPathgenScene { yes, no };
    [SerializeField] private autoContinueToPathgenScene _autoContinueToPathgenScene;
    [SerializeField] private bool skipSubjectIdUiInput;
    void Awake()
    {
        
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("player").transform.gameObject);
        DontDestroyOnLoad(GameObject.Find("PlayerController").transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForResourceDownload());
    }

    IEnumerator WaitForResourceDownload()
    {
        yield return new WaitUntil(() => MetadataInputContext.isMetadataFetchComplete);
        if(_autoContinueToPathgenScene == autoContinueToPathgenScene.yes)
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        if ((!skipSubjectIdUiInput && DataLogger.isSubjectIDRecieved) || OVRInput.GetDown(OVRInput.Button.One))
        {  
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            skipSubjectIdUiInput = false;
            DataLogger.isSubjectIDRecieved = false;
        }
    }

}
