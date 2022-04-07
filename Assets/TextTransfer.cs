using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransfer : MonoBehaviour
{
    public string theName;
    public GameObject confIDInputField;
    public GameObject textDisplay;

    public void OnButClick(){
        theName = confIDInputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = theName;
    }
}
