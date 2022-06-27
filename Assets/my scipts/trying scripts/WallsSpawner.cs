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


    // Kandarp
    public GameObject GenerateObstacleGateTurretCoin(ref List<Vector3>[] points,ref List<float> betaList,List<GameObject> obstacleBlueGateOrangegate,List<GameObject> turretBlueOrange, GameObject coin)
    {
        float smallerPath = Mathf.Max((points[1][points[1].Count - 2] - points[1][points[1].Count - 3]).magnitude, (points[0][points[0].Count - 2] - points[0][points[0].Count - 3]).magnitude);
        bool isRightWall = false;

        if(smallerPath == (points[1][points[1].Count - 2] - points[1][points[1].Count - 3]).magnitude){
            isRightWall = true;
        }
        else{
            isRightWall = false;
        }


        float pathLimit = 0.25f;
        int obstacleORgateORturretORcoin = Random.Range(0,4);  // 0 for obstacle, 1 for gate, 2 for turret
        int colorType = Random.Range(0,2); // 0 for blue, 1 for orange

        GameObject obstacleORgateORturretORcoininstantiate;
        if(obstacleORgateORturretORcoin == 0){
            obstacleORgateORturretORcoininstantiate = obstacleBlueGateOrangegate[0];
        }
        else if(obstacleORgateORturretORcoin == 1){
            if(colorType == 0){
                obstacleORgateORturretORcoininstantiate = obstacleBlueGateOrangegate[1];
            }
            else{
                obstacleORgateORturretORcoininstantiate = obstacleBlueGateOrangegate[2];
            }
        }
        else if(obstacleORgateORturretORcoin == 2){
            if(colorType == 0){
                obstacleORgateORturretORcoininstantiate = turretBlueOrange[0];
            }
            else{
                obstacleORgateORturretORcoininstantiate = turretBlueOrange[1];
            }
        }
        else{
            obstacleORgateORturretORcoininstantiate = coin;
        }



        if(smallerPath > pathLimit && (Mathf.Abs(betaList[betaList.Count - 2]) % Mathf.PI) <= Mathf.PI/3){
            GameObject tmp = Instantiate(obstacleORgateORturretORcoininstantiate, Vector3.zero, Quaternion.identity);
            Vector3 tmpScale = tmp.transform.localScale;

            if(obstacleORgateORturretORcoin == 2){
                float turretScale = (points[1][points[1].Count - 2] - points[0][points[0].Count - 2]).magnitude/5;
                tmpScale.x = turretScale;
                tmpScale.y = turretScale;
                tmpScale.z = turretScale;
            }
            else if(obstacleORgateORturretORcoin == 3){
                float coinScale = (points[1][points[1].Count - 2] - points[0][points[0].Count - 2]).magnitude/3;
                tmpScale.x = tmp.transform.localScale.x * coinScale;
                tmpScale.y = tmp.transform.localScale.y * coinScale;
                tmpScale.z = tmp.transform.localScale.z * coinScale; // do for turret and walls also like this
            }
            else{
                tmpScale.x = (points[1][points[1].Count - 2] - points[0][points[0].Count - 2]).magnitude/3; // here 1/3 factor can be changed
                tmpScale.y = 2f;
                tmpScale.z = 0.01f;
            }
            
            tmp.transform.localScale = tmpScale;



            Vector3 tmpPos = tmp.transform.position;
            
            // if(Random.Range(0,2) == 0){
            //     tmpPos = (points[1][points[1].Count - 2] + points[1][points[1].Count - 3])/2; // obstacle spawned near right wall
            //     tmpPos.x = tmpPos.x - tmpScale.x/2;
            // }
            // else{
            //     tmpPos = (points[0][points[0].Count - 2] + points[0][points[0].Count - 3])/2; // obstacle spawned near left wall
            //     tmpPos.x = tmpPos.x + tmpScale.x/2;
            // }

            // tmpPos = (points[1][points[1].Count - 2] + points[1][points[1].Count - 3] + points[0][points[0].Count - 2] + points[0][points[0].Count - 3])/4;

            Vector3 mid = (points[1][points[1].Count - 2] + points[1][points[1].Count - 3] + points[0][points[0].Count - 2] + points[0][points[0].Count - 3])/4;
            Vector3 spawnPoint; 
            if(isRightWall){
                spawnPoint = (mid + (points[1][points[1].Count - 2] + points[1][points[1].Count - 3])/2)/2;
            }
            else{
                spawnPoint = (mid + (points[0][points[0].Count - 2] + points[0][points[0].Count - 3])/2)/2;
            }

            tmpPos = spawnPoint;

            if(obstacleORgateORturretORcoin == 2){
                tmpPos.y = tmpScale.y;
            }   
            else if(obstacleORgateORturretORcoin == 3){
                tmpPos.y = 1.32f;
            }
            else{
                tmpPos.y = tmpScale.y - 0.8f;
            } 
            
            tmp.transform.position = tmpPos;

            // float obstacleAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), (points[1][points[1].Count - 2] - points[0][points[0].Count - 2]).normalized, Vector3.up); // bad method, doesnt produce obstacles perpendicular to the walls
            
            // good method to rotate, produces obstacles perpendicular to the wall
            float obstacleAngleAlongY;
            if(isRightWall){
                obstacleAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), Quaternion.AngleAxis(90,Vector3.up)*(points[1][points[1].Count - 2] - points[1][points[1].Count - 3]).normalized, Vector3.up);
            }
            else{
                obstacleAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), Quaternion.AngleAxis(90,Vector3.up)*(points[0][points[0].Count - 2] - points[0][points[0].Count - 3]).normalized, Vector3.up);
            }


            Vector3 rotation = tmp.transform.localEulerAngles;
            rotation = new Vector3(0f, obstacleAngleAlongY, 0f);
            tmp.transform.localEulerAngles = rotation;

            return tmp;
        }
        return null;
    }

}
