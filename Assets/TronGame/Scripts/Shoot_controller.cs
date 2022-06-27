using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_controller : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController =  GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(Physics.Raycast(ray, out RaycastHit hit, 10f, aimColliderLayerMask))
        {
            // float diff;
            // diff = hit.point.z - gameObject.transform.position.z + 5f;
            debugTransform.position = new Vector3(hit.point.x,hit.point.y,hit.point.z);
        }
        
    }
}
