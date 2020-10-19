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
   
    /// <summary>
    /// Player x-z position in playArea. y is 0
    /// PlayArea origin is at middle of the playarea.
    /// </summary>
    /// <returns>player position in playarea. y is 0</returns>
    public Vector3 PlayerPosition()
    {
        return this.inputDevice.PlayerPosition();
    }
    /// <summary>
    /// Rectangular Playarea dimensions x and z. y is 0.
    /// </summary>
    /// <returns>Playarea dimensions x and z. y is 0</returns>
    public Vector3 PlayAreaDimensions()
    {
        return this.inputDevice.PlayAreaDimensions();
    }
    /// <summary>
    /// Player head yaw in radians
    /// </summary>
    /// <returns>Player's head yaw in radians</returns>
    public float PlayerRotationAlongYAxis()
    {
        return this.inputDevice.PlayerRotationAlongYAxis();
    }
    public bool ButtonPressed()
    {
        return this.inputDevice.ButtonPressed();
    }
}
