using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YT_shooter : MonoBehaviour
{
    // crossHair
    public Sprite crossHair;
   
    // disc
    public GameObject discOrange,discBlue;
    public Material TronOrange,TronBlue;

    public Camera shootCam;

    //disc force
    public float shootForce;

    //Gun stats 
    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magazineSize, discsPerTap;

    int discsLeft, discsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Transform attackPoint;

    public bool allowInvoke = true;

    //made global variable to use in Update()
    GameObject currentDisc;
    
    private void Awake() {
        discsLeft = magazineSize;
        readyToShoot = true;

        Cursor.SetCursor(crossHair.texture,Vector3.zero,CursorMode.Auto);
    }

    private void Update()
    {
        MyInput();

        if(currentDisc != null){
            if(currentDisc.transform.position.magnitude - transform.position.magnitude >= 100)
            {
                Destroy(currentDisc);
            }
        }

        

        // Ray ray = shootCam.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if(Physics.Raycast(ray,out hit)){
        //     crossHair.transform.position = hit.point;
        // }
        // crossHair.transform.position = new Vector3(crossHair.transform.position.x,crossHair.transform.position.y,crossHair.transform.position.z + offset);

    }

    private void MyInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && discsLeft < magazineSize && !reloading){
            Reload();
        }
        if(readyToShoot && shooting && !reloading && discsLeft <=0){
            Reload();
        }


        

        if(readyToShoot && shooting && !reloading && discsLeft > 0)
        {
            discsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = shootCam.ScreenPointToRay(Input.mousePosition);
        // Ray ray = shootCam.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,transform.position.z - Camera.main.transform.position.z)); // change this to make shooting in 3rd person

        // Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        // Vector3 worldMousePosition = shootCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,transform.position.z));
        // Vector3 direction = worldMousePosition - attackPoint.position;
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit)){
        // if(Physics.Raycast(attackPoint.position,direction,out hit)){ 
            targetPoint = hit.point;
        }
        else{
            targetPoint = ray.GetPoint(75);
            // targetPoint = new Vector3(0,0,100);
        }

        Vector3 directionWithoutSpeed = targetPoint - attackPoint.position;
        

        if(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial == TronOrange) 
        {
            currentDisc = Instantiate(discOrange,attackPoint.position,attackPoint.rotation); // Quaternion.identity() ?? check once
        } 
        else
        {
            currentDisc = Instantiate(discBlue,attackPoint.position,attackPoint.rotation);
        }

        currentDisc.transform.forward = directionWithoutSpeed.normalized;

        currentDisc.GetComponent<Rigidbody>().AddForce(directionWithoutSpeed.normalized * shootForce, ForceMode.Impulse);

        discsLeft--;
        discsShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot",timeBetweenShooting);
            allowInvoke = false;
        }

        if(discsShot < discsPerTap && discsLeft > 0){
            Invoke("Shoot",timeBetweenShots);
        }
    }

    private void ResetShot(){
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished",reloadTime);
    }

    private void ReloadFinished()
    {
        discsLeft = magazineSize;
        reloading = false;
    }







   

    
}
