using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputContext
{
    private IDeviceInput deviceInput;
    public InputContext(IDeviceInput deviceInput)
    {
        this.deviceInput = deviceInput;
    }

    public Vector3 StartingPosition()
    {
        return this.deviceInput.StartingPosition();
    }

    public Vector3 PlayAreaDimension()
    {
        return this.deviceInput.PlayAreaDimensions();
    }
}
