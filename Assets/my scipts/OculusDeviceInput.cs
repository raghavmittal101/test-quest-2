using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusDeviceInput : MonoBehaviour, IDeviceInput
{
    Vector3 dimensions;
    int triggerColliderHitNumber = -1;

    public bool PlayerMovingForward()
    {
        var playerObj = GameObject.Find("CenterEyeAnchor");
        RaycastHit hit;
        int num;
        Debug.DrawRay(playerObj.transform.position, Vector3.forward * 0.5f, Color.white);
        if (Physics.Raycast(playerObj.transform.position, Vector3.forward, out hit, 0.5f)) // cast a ray 0.5 units in player direction
        {
            try
            {
                num = System.Convert.ToInt32(hit.collider.name);
            }
            catch (System.Exception) // if player hits some collider with alphanumeric string then exception will occur
            {
                num = -1;
            }

            if (triggerColliderHitNumber < num) // means that the player has moved forward
            {
                Debug.Log(num);
                triggerColliderHitNumber = num;

                return true;
            }
        }
        return false;
    }
    public Vector3 PlayerPosition()
    {
        var centerEyeAnchor = GameObject.Find("CenterEyeAnchor").transform.position;
        return new Vector3(centerEyeAnchor.x, 0f, centerEyeAnchor.z); // y is set to 0f because we only need 2D position in xz space
    }
    public Vector3 PlayAreaDimensions()
    {
        dimensions = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
        return dimensions;
    }
    /// <summary>
    /// head yaw is converted to radians from degrees
    /// </summary>
    /// <returns>Player's head yaw in radians</returns>
    public float PlayerRotationAlongYAxis()
    {
        OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();
        float rotAlongY = cameraRig.transform.eulerAngles.y * Mathf.Deg2Rad;
        return rotAlongY;
    }

    public bool ButtonPressed()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
            return true;
        return false;
    }

    void Update()
    {
        dimensions = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
    }
}
