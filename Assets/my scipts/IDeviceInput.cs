using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeviceInput {
    Vector3 PlayerPosition();
    Vector3 PlayAreaDimensions();
    float PlayerRotationAlongYAxis();
    bool ButtonPressed();
    bool PlayerMovingForward();
    GameObject PlayerObj();
}
