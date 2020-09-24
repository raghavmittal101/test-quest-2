using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{

    public static void SpawnWall(Vector3 startPos, Vector3 endPos, float rotationAlongYAxis, float pathWidth)
    {
        Vector3 LeftWallPos;
        Vector3 RightWallPos;

        GameObject wallPrefab = GameObject.Find("wallPrefab");
        Vector3 wallPos = (endPos + startPos)/2;
        wallPos.y = wallPrefab.transform.position.y;

        Vector3 left = new Vector3(-wallPos.z, 0, wallPos.x).normalized;

        LeftWallPos = wallPos + left * pathWidth * 0.5f; // left point
        RightWallPos = wallPos - left * pathWidth * 0.5f; // right point

        Debug.DrawLine(startPos, wallPos, Color.blue, 10f);
        Vector3 wallRot = new Vector3(0f, (rotationAlongYAxis+ Mathf.PI / 2 )* Mathf.Rad2Deg, 0f) ;
        GameObject leftWall = Instantiate(wallPrefab, RightWallPos, Quaternion.Euler(wallRot));
        GameObject rightWall = Instantiate(wallPrefab, LeftWallPos, Quaternion.Euler(wallRot));
        
        //var scale = go.transform.localScale.normalized;
        //scale.x = scale.x * wallVector.magnitude;
        //go.transform.localScale = scale;
    }
}
