using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for detecting the boundaries of the playarea and set range of beta.
/// </summary>
public class DetectBoundaryFixedDirections
{

    RaycastHit[] rayArray;
    float[] rayDirectionArray;
    int rayArrayLength = 5;
    public float rayLength;

    float playerRotation;
    Vector3 playerPosition;
    float boundaryBufferWidth = 0.5f;

    public DetectBoundaryFixedDirections(int rayArrayLength, float boundaryBufferWidth, float pathLength, float pathWidth)
    {
        this.boundaryBufferWidth = boundaryBufferWidth;
        this.rayLength = pathLength + pathWidth / 2 + boundaryBufferWidth;
        this.rayArray = new RaycastHit[rayArrayLength];
        this.rayDirectionArray = new float[rayArrayLength];
        this.rayArrayLength = rayArrayLength;
        for (int i = 0; i < rayArrayLength; i++) { rayArray[i] = new RaycastHit(); }
        for (int i = 0; i < rayArrayLength; i++) { rayDirectionArray[i] = (-1) * (Mathf.PI / 2) + ((Mathf.PI / (rayArrayLength-1)) * i); }
    }

    
    public float GetBeta(Vector3 pointLocation, float pointRotationWithY)
    {
        float[] betaRange = this.GetBetaRange(pointLocation, pointRotationWithY);
        float newBeta;
        if (betaRange[0] != betaRange[1]) newBeta = Random.Range(pointRotationWithY + betaRange[0], pointRotationWithY + betaRange[1]);
        else newBeta = pointRotationWithY + betaRange[0];
        return newBeta;
    }

    public float[] GetBetaRange(Vector3 pointPosition, float pointRotationWithY)
    {
        this.playerPosition = pointPosition;
        this.playerRotation = pointRotationWithY;
        List<int> noHitRayIndexList = new List<int>();
        float[] betaRange = new float[2];
        Debug.Log("DetectBoundaryFixedDirections.cs: playerPosition" + playerPosition);
        GenerateRays(this.playerPosition, this.playerRotation);

        for (int i = 0; i < rayArrayLength; i++)
        {
            //Debug.DrawRay(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition) * rayLength, Color.green);
            if (Physics.Raycast(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition), out rayArray[i], rayLength))
            {
                //Debug.DrawRay(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition) * rayLength, Color.red);
            }
            else
            {
                noHitRayIndexList.Add(i);
            }
        }

        var message = "";
        foreach (int i in noHitRayIndexList) { message += ", " + i; }

        Debug.Log("noHitRayIndexList" + message);

        if (noHitRayIndexList.Count == 0)
        {
            RaycastHit hitObj;
            if (Physics.Raycast(playerPosition, GetFwd(playerRotation + (3 * Mathf.PI / 4), playerPosition), out hitObj, rayLength))
                return new float[] { -3 * Mathf.PI / 4, -3 * Mathf.PI / 4 };
            else return new float[] { -3 * Mathf.PI / 4, -3 * Mathf.PI / 4 };
        }

        System.Random r = new System.Random();
        var rayDirectionIndex = noHitRayIndexList[r.Next(noHitRayIndexList.Count)];

        Debug.Log("Next beta is:" + rayDirectionArray[rayDirectionIndex] * Mathf.Rad2Deg);
        Debug.DrawRay(playerPosition, GetFwd(playerRotation + rayDirectionArray[rayDirectionIndex], playerPosition) * rayLength, Color.white);

        
        if (noHitRayIndexList.Count < rayArrayLength)
        {
            return new float[] { rayDirectionArray[rayDirectionIndex], rayDirectionArray[rayDirectionIndex] };
        }
        else
        {
            return new float[] { rayDirectionArray[0], rayDirectionArray[rayArrayLength - 1] };
        }
    }

    public void GenerateRays(Vector3 playerPosition, float playerRotation)
    {
        for (int i = 0; i < rayArrayLength; i++)
        {
            Debug.DrawRay(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition) * rayLength, Color.green);
            if (Physics.Raycast(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition), out rayArray[i], rayLength))
            {
                Debug.DrawRay(playerPosition, GetFwd(playerRotation + rayDirectionArray[i], playerPosition) * rayLength, Color.red);
            }
        }
    }

    private Vector3 GetFwd(float beta, Vector3 origin)
    {
        // beta should be in radians
        // returns a unit vector pointing in forward direction from the current vector at an given angle
        Vector3 targetPoint;
        targetPoint.x = Mathf.Sin(beta) + origin.x;
        targetPoint.y = origin.y;
        targetPoint.z = Mathf.Cos(beta) + origin.z;
        return (targetPoint - origin).normalized;
    }
}
    