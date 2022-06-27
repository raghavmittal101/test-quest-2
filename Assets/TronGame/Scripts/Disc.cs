using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    private Vector3 target;
    public float speed = 70f;
    public void Seek(Vector3 _target)
    {
        target = _target;
    }
     void Update()
    {
        if(target == Vector3.zero)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
