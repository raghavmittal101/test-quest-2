using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRayShoot : MonoBehaviour
{
    public Transform fpsObject;
    public float rayLength = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(fpsObject.position, fpsObject.forward, out hit, rayLength);
        Debug.DrawRay(fpsObject.position, fpsObject.forward * rayLength, Color.red);
    }
}
