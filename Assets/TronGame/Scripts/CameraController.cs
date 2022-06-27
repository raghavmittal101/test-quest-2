using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
 
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame

    private void LateUpdate() 
    {
        // Vector3 newPosition = new Vector3(transform.position.x,transform.position.y,offset.z+target.position.z);

        // In below play around this 1.5,-1.5 and 8 values to get smooth transition and better camera capture for leeft and right side
        Vector3 newPosition = new Vector3(0,0,0);
        if(target.position.x == 2.5){
            newPosition = new Vector3(1.5f,transform.position.y,offset.z+target.position.z);
        }
        else if(target.position.x == -2.5){
            newPosition = new Vector3(-1.5f,transform.position.y,offset.z+target.position.z);
        }
        else{
            newPosition = new Vector3(0,transform.position.y,offset.z+target.position.z);
        }
        transform.position = Vector3.Lerp(transform.position,newPosition,8*Time.deltaTime);
    }
}
