using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDimensions : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (OVRManager.boundary.GetConfigured())
        {
            Debug.Log("boundary configured!!!");
        }

//        Vector3 dimensions = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.OuterBoundary);
  //      Debug.Log("x:" + dimensions.x + " z:" + dimensions.z);

    }
}
