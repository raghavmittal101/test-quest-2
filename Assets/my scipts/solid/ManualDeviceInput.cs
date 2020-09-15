using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDeviceInput : IDeviceInput
{
    private Vector3 startingPosition;
    private Vector3 playAreaDimensions;
    
    public ManualDeviceInput(Vector3 startingPosition, Vector3 playAreaDimensions)
    {
        this.startingPosition = startingPosition;
        this.playAreaDimensions = playAreaDimensions;
    }
    public Vector3 StartingPosition()
    {
        return startingPosition;
    }
    public Vector3 PlayAreaDimensions()
    {
        return playAreaDimensions;
    }
}
