using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDeviceContext : MonoBehaviour, IDeviceInput
{
    private enum inputDeviceType { ManualInput, OculusVR };
    [SerializeField] private inputDeviceType _inputDeviceType;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 playAreaDimensions;
    [SerializeField] private float playerRotationAlongYAxis;

    public IDeviceInput inputDevice;

    public void Awake()
    {
            if (_inputDeviceType == inputDeviceType.ManualInput)
            {
                this.inputDevice = new ManualDeviceInput(playerPosition, playerRotationAlongYAxis, playAreaDimensions);
            }

            else if(_inputDeviceType == inputDeviceType.OculusVR)
            {
                this.inputDevice = new OculusDeviceInput();
            }
            else
        {
            Debug.Log("Please select Manual Input in Input device type");
        }
        // add new input options here
    }
   

    public Vector3 PlayerPosition()
    {
        return this.inputDevice.PlayerPosition();
    }

    public Vector3 PlayAreaDimensions()
    {
        return this.inputDevice.PlayAreaDimensions();
    }

    public float PlayerRotationAlongYAxis()
    {
        return this.inputDevice.PlayerRotationAlongYAxis();
    }
    public bool ButtonPressed()
    {
        return this.inputDevice.ButtonPressed();
    }
}
