/*
 * Script to demo collider detection using raycasting.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayColliderExample : MonoBehaviour
{
    public GameObject player;
    private float distance;
    private string colliderName;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit objHit;
        Vector3 fwd = player.transform.forward;

        distance = 0;
        colliderName = "none";

        Debug.DrawRay(player.transform.position, fwd * 50, Color.red);
        if(Physics.Raycast(player.transform.position, fwd, out objHit, 50))
        {
            Debug.DrawRay(player.transform.position, fwd * 50, Color.green);
            distance = objHit.distance;
            colliderName = objHit.collider.name;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        GUI.Label(new Rect(10, 0, 0, 0), "Distance" + distance, style);
        GUI.Label(new Rect(10, 20, 0, 0), "Collider name" + colliderName, style);

    }
}
