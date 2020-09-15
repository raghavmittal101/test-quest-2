using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDeviceContext : MonoBehaviour, IDeviceInput
{
    private enum inputDeviceType { ManualInput, OculusVRNotWorking };
    [SerializeField] private inputDeviceType _inputDeviceType;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 playAreaDimensions;

    public IDeviceInput inputDevice;

    public void Awake()
    {
            if (_inputDeviceType == inputDeviceType.ManualInput)
            {
                this.inputDevice = new ManualDeviceInput(startingPosition, playAreaDimensions);
            }

            else
        {
            Debug.Log("Please select Manual Input in Input device type");
        }
            // add new input options here
    }
   

    public Vector3 StartingPosition()
    {
        return this.inputDevice.StartingPosition();
    }

    public Vector3 PlayAreaDimensions()
    {
        return this.inputDevice.PlayAreaDimensions();
    }
}
