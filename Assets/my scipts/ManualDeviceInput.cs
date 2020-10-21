using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDeviceInput : IDeviceInput
{
    private Vector3 playerPosition;
    private Vector3 playAreaDimensions;
    private float playerRotationAlongYAxis;
    
    public ManualDeviceInput(Vector3 playerPosition, float playerRotationAlongYAxis, Vector3 playAreaDimensions)
    {
        this.playerPosition = playerPosition;
        this.playerRotationAlongYAxis = playerRotationAlongYAxis;
        this.playAreaDimensions = playAreaDimensions;
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
        if (Input.GetMouseButtonDown(0))
            return true;
        return false;
    }
}
