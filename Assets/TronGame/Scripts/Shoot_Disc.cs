using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Disc : MonoBehaviour
{
    public Camera cam;
    public Vector3 target;
    public GameObject discOrange, discBlue;
    public Transform spawnPoint;
    public Material TronOrange,TronBlue;
    public float range = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject discGO;

        // for capsule Player
        // if(gameObject.GetComponent<Renderer>().sharedMaterial == orangeLight) // always use sharedMaterial to compare a instance of material

        // for Tron Player
        if(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial == TronOrange) 
        {
            discGO = Instantiate(discOrange,spawnPoint.position,spawnPoint.rotation);
        } 
        else
        {
            discGO = Instantiate(discBlue,spawnPoint.position,spawnPoint.rotation);
        }


        Ray mouseray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit mousehit;
        if(Physics.Raycast(mouseray,out mousehit))
        {
            // if(hit.transform.tag == "Turret")
            // {
            //     target = hit.point;
            // }
            Vector3 dir = mousehit.point - transform.position;
            RaycastHit hit;
            Ray ray = new Ray(transform.position,dir);
            if(Physics.Raycast(ray,out hit))
            {
                // if(hit.transform.tag == "Turret")
                {
                    target = hit.point;
                }
            }
        }   
        // Physics.Raycast(gameObject.transform.position,gameObject.transform.forward, out hit,range);
        
        
        Disc disc = discGO.GetComponent<Disc>();
        if(disc != null)
        {
            disc.Seek(target);
        }
    }
}
