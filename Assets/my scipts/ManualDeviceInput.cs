using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDeviceInput : IDeviceInput
{
    private Vector3 playerPosition;
    private Vector3 playAreaDimensions;
    private float playerRotationAlongYAxis;
    private GameObject playerObj;

    private int triggerColliderHitNumber = -1;
    
    public ManualDeviceInput(Vector3 playerPosition, float playerRotationAlongYAxis, Vector3 playAreaDimensions, GameObject playerObj)
    {
        this.playerPosition = playerPosition;
        this.playerRotationAlongYAxis = playerRotationAlongYAxis*Mathf.Deg2Rad;
        this.playAreaDimensions = playAreaDimensions;
        this.playerObj = playerObj;
    }
    /// <summary>
    /// to check player movement in forward direction. See also <see cref="DetectBoundaryTestScript"/>.
    /// </summary>
    /// <returns></returns>
    public bool PlayerMovingForward()
    {
        RaycastHit hit;
        int num;
        Debug.DrawRay(playerObj.transform.position, playerObj.transform.localRotation.eulerAngles.normalized * 1f, Color.white);
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
        // a left click on mouse will also mean that player is moving forward
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }
    public Vector3 PlayerPosition()
    {
        return playerPosition;
    }
    public float PlayerRotationAlongYAxis()
    {
        return playerRotationAlongYAxis * Mathf.Deg2Rad; // should be in radians
    }
    public Vector3 PlayAreaDimensions()
    {
        return playAreaDimensions;
    }

    public bool ButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            return true;
        return false;
    }

    public GameObject PlayerObj()
    {
        return playerObj;
    }
}
