using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    /// <summary>
    /// </summary>
    /// <param name="points">List of points between which you want to place it and stretch it. 
    /// These points are not center point by left or right points.</param>
    /// <param name="wallPrefab">ref to wall GameObject prefab</param>
    /// <returns></returns>
    public GameObject GenerateWall(ref List<Vector3> points, GameObject wallPrefab)
    {   
        GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);

        Vector3 wallScale = wall.transform.localScale;
        wallScale.x = (points[points.Count - 2] - points[points.Count - 3]).magnitude;
        //wallScale.y = 2.5f;  // to be given in metadata
        wallScale.z = 0.01f;
        wall.transform.localScale = wallScale;

        Vector3 wallPos = wall.transform.position;
        wallPos = (points[points.Count - 2] + points[points.Count - 3]) / 2;
        wallPos.y = wallScale.y/2;
        wall.transform.position = wallPos;

        float wallRotAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), (points[points.Count - 2] - points[points.Count - 3]).normalized, Vector3.up);
        Vector3 rotation = wall.transform.localEulerAngles;
        rotation = new Vector3(0f, wallRotAngleAlongY, 0f);
        wall.transform.localEulerAngles = rotation;

        return wall;

    }

}
