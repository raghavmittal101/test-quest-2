using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoundary
{
    // cast a ray in directions relative to previousBeta, the ray length should be pathLength+(pathWidth/2)
    // if ray hits a boundary, decide the angle range based on the angles at which it hits.
    private float[] betaRange;
    float validDistance;
    Vector3 point;
    float beta;
    float pathLength;

    public float[] GetBetaRange(Vector3 point, float beta, float pathLength, float pathWidth)
    {
        // beta is rotation of point along y-axis or angle of point with z-axis
        // beta should be in radians
        betaRange = new float[2];
        Vector3 fwd = GetFwd(beta, point);
        this.validDistance = pathLength + (pathWidth / 2);
        this.point = point;
        this.beta = beta;
        this.pathLength = pathLength;
        /*
         Algorithm
         - cast a very long ray at beta angle from point
         - when it hits a collider, find the distance of ray.
         - If distance is less than equal to validDistance, then it means the point is close to a boundary
         - look at 45 degree, again do same process till +- 90 degrees ... and go on
         */

        betaRange[0] = GetAngleRecusively(0, -1);
        betaRange[1] = GetAngleRecusively(0, 1);

        // only lower range is invalid, means path is very close to one of the boundaries
        // set lower range = upper range
        if (betaRange[0] == -1*Mathf.PI/2 && betaRange[1] > 0)
        {
            Debug.Log("DetectBoundary: very close to left boundary");
            betaRange[0] = betaRange[1];
        }

        // opposite of above case
        else if (betaRange[0] < 0 && betaRange[1] < 0)
        {
            Debug.Log("DetectBoundary: very close to left boundary");
            betaRange[1] = betaRange[0];
        }

        // both the range are invalid, means path is stuck in a corner
        // redirect the path by giving a sharp 135-degree turn
        else if (betaRange[0] > 0 && betaRange[1] < 0)
        {
            Debug.Log("DetectBoundary: in a corner");
            RaycastHit hit;
            if (Physics.Raycast(point, GetFwd(beta + (3 * Mathf.PI / 4), point), out hit, validDistance))
            {
                Debug.DrawRay(point, GetFwd(beta + (3 * Mathf.PI / 4), point), Color.cyan, 5f);
                Debug.Log("DetectBoundary: GetAngleRecursively: hit at" + beta + (3 * Mathf.PI / 4) * Mathf.Rad2Deg);
                Debug.Log("DetectBoundary: GetAngleRecursively: colliderName: " + hit.collider.name);
                betaRange[0] = -3 * Mathf.PI / 4;
                betaRange[1] = -3 * Mathf.PI / 4;
            }

            else
            {
                betaRange[0] = 3 * Mathf.PI / 4;
                betaRange[1] = 3 * Mathf.PI / 4;
                
            }
        }

        return betaRange;

    }

    RaycastHit hit;
    private float GetAngleRecusively(float angle, int i)
    {
        
        // Debug.DrawLine(transform.position, hit.transform.position, Color.blue, 10f);
        ///Debug.DrawLine(this.point, hit.transform.position, Color.blue, 10f);
        
        if(Physics.Raycast(point, GetFwd(beta + angle, point), out hit, validDistance))
        {
            Debug.DrawRay(point, GetFwd(beta + angle, point), Color.red, 5f);
            Debug.Log("DetectBoundary: GetAngleRecursively: hit at" + angle * Mathf.Rad2Deg);
            Debug.Log("DetectBoundary: GetAngleRecursively: colliderName: " + hit.collider.name);
            if(angle == 0 || angle == Mathf.PI * 2 || angle==-1*Mathf.PI*2) { return -1000; }
            return angle - i * (Mathf.PI / 4);
        }
        else
        {
            Debug.DrawRay(point, GetFwd(beta + angle, point), Color.green, 2f);
            Debug.Log("DetectBoundary: GetAngleRecursively: no hit at" + angle * Mathf.Rad2Deg);
            if(angle == i*Mathf.PI / 2) { return angle; }
            return GetAngleRecusively(angle + i * (Mathf.PI / 4), i);
        }
    }

    private Vector3 GetFwd(float beta, Vector3 originPoint)
    {
        // beta should be in radians
        // returns a vector pointing in forward direction from the current point at an given angle
        Vector3 targetPoint = Vector3.zero; // direction of the ray
        targetPoint.x = validDistance*Mathf.Sin(beta) + point.x;
        targetPoint.y = 0f;
        originPoint.y = 0f;
        targetPoint.z = validDistance* Mathf.Cos(beta) + point.z;
        return targetPoint-originPoint;
    }
}
