using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialChange : MonoBehaviour
{
    //For capsule Player
    // public Material orangeLight, blueLight;

    //For Tron Player 
    public Material TronOrange, TronBlue;
    bool orangeMaterial = true; // orangeMaterial = true(for TronOrange and orangeBGCOLOR), = false(for TronBlue and blueBGCOLOR)

    public GameObject maincamera;

    private void Start() 
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = TronOrange;
        maincamera.GetComponent<Camera>().backgroundColor = new Color32(223,116,12,0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            orangeMaterial = !orangeMaterial;
            if(orangeMaterial)
            {
                 GetComponentInChildren<SkinnedMeshRenderer>().material = TronOrange;
                 maincamera.GetComponent<Camera>().backgroundColor = new Color32(223,116,12,0);

            }
            else
            {
                 GetComponentInChildren<SkinnedMeshRenderer>().material = TronBlue;
                 maincamera.GetComponent<Camera>().backgroundColor = new Color32(125,253,254,0);
            }
        }
    }
}
