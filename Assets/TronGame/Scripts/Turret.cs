using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletOrange, bulletBlue;
    public Transform firePoint;

    public float turretUpOffset = 2f; // seems good for now, can change to see which better later 


    void Start()
    {
        InvokeRepeating("targetLocked",0f,0.5f);
    }

    void targetLocked()
    {
        // For capsule Player
        // target = GameObject.FindWithTag("Player").transform;

        // For Tron Player
        target = GameObject.FindWithTag("PlayerTron").transform; 
        float distFromTarget = Vector3.Distance(target.transform.position,transform.position);
        if(distFromTarget > range)
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        dir.y = dir.y + turretUpOffset;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,0f);

        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGO;
        if(gameObject.name == "Turret_OrangeGrey(Clone)") // exact name has to be given, so added (Clone) also. Check if there is better method to compare later.
        {
            bulletGO = Instantiate(bulletOrange,firePoint.position,firePoint.rotation);
        }
        else
        {
            bulletGO = Instantiate(bulletBlue,firePoint.position,firePoint.rotation);
        }

        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }
}
