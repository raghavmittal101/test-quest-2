using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDeviceInput : MonoBehaviour, IDeviceInput
{
    [SerializeField]
    private Vector3 startingPosition;
    [SerializeField]
    private Vector3 playAreaDimensions;
    
    public Vector3 StartingPosition()
    {
        return startingPosition;
    }
    public Vector3 PlayAreaDimensions()
    {
        return playAreaDimensions;
    }
}
