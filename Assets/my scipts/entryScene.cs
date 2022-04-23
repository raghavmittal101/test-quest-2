using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class entryScene : MonoBehaviour
{
    private enum autoContinueToPathgenScene { yes, no };
    [SerializeField] private autoContinueToPathgenScene _autoContinueToPathgenScene;
   // [SerializeField] private bool skipSubjectIdUiInput;
    private bool sceneLoadFlag = true;
    public static string subjectID = "-1";
    public TMP_InputField subjectIDInputField;
    public static bool isSubjectIDRecieved = false;

    public void OnSubjectIdSubmit()
    {
        Debug.Log("At subject submission");
        // get data from input field and assign it to subject ID.
        //       Debug.Log(subjectIDInputField.GetComponent<Text>().text);
        entryScene.subjectID = subjectIDInputField.text;
        if(entryScene.subjectID != "-1")
        {
            isSubjectIDRecieved = true;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

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
        //if (OVRInput.GetDown(OVRInput.Button.One) && sceneLoadFlag)
        //{
        //    Debug.Log("inside entryScene.cs Update()->if()");
            
        //    // skipSubjectIdUiInput = false;
        //    entryScene.isSubjectIDRecieved = true;
        //    sceneLoadFlag = false;
        //}
    }

}
