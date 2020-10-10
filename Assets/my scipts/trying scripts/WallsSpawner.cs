﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    public GameObject GenerateWall(List<Vector3> points, GameObject wallPrefab)
    {
        //GameObject wallPrefab = GameObject.Find("wallPrefab");
        Vector3 wallPos = (points[points.Count-2] + points[points.Count - 3]) / 2;
        wallPos.y = 0.5f;
   
        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity);
        Vector3 wallScale = wall.transform.localScale;
        wallScale.x = (points[points.Count - 2] - points[points.Count - 3]).magnitude;
        wallScale.y = 1f;
        wallScale.z = 0.01f;
        wall.transform.localScale = wallScale;

        float wallRotAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), (points[points.Count - 2] - points[points.Count - 3]).normalized, Vector3.up);
        Vector3 rotation = wall.transform.localEulerAngles;
        rotation = new Vector3(0f, wallRotAngleAlongY, 0f);
        wall.transform.localEulerAngles = rotation;
        return wall;

    }

}
