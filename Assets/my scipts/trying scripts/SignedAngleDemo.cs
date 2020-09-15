using UnityEngine;
using System.Collections;

public class SignedAngleDemo : MonoBehaviour
{
    float angle;
    public GameObject player;
    Vector3 pos;
    float angleZ;

    void Update()
    {
        pos = player.transform.position;
        angle = Vector3.SignedAngle(pos, Vector3.forward, Vector3.up);
        angleZ = player.transform.rotation.eulerAngles.y;

        Debug.DrawLine(Vector3.zero, Vector3.forward*5, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.right * 5, Color.red);
        Debug.DrawLine(Vector3.zero, pos, Color.yellow);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        GUI.Label(new Rect(10, 0, 0, 0), "Angle:" + angle, style);
        GUI.Label(new Rect(10, 20, 0, 0), "AngleWithZ:" + angleZ, style);
    }
}
