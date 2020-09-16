using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectBoundariesTrigno: MonoBehaviour
{
    Vector3 previousPosition;
    Vector3 playerPosition;
    Vector3[] RayDirections = { new Vector3(0f, 0f, 1f),
                                    new Vector3(1f, 0f, 0f),
                                    new Vector3(0f, 0f, -1f),
                                    new Vector3(-1f, 0f, 0f) };
    RaycastHit north = new RaycastHit();
    RaycastHit east = new RaycastHit();
    RaycastHit south = new RaycastHit();
    RaycastHit west = new RaycastHit();
    RaycastHit[] rayArray = new RaycastHit[4];
    public GameObject player;
    public GameObject previousPlayerPositionObject;
    
    [Range(1f, 5f)]
    public float rayLength;

    private void Awake()
    {
        playerPosition = player.transform.position;
        previousPosition = previousPlayerPositionObject.transform.position;
        rayArray[0] = north;
        rayArray[1] = east; 
        rayArray[2] = south;
        rayArray[3] = west;

        
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        previousPosition = previousPlayerPositionObject.transform.position;

        // for visualising the orientation and position of the player

        Debug.DrawLine(previousPosition, playerPosition);
        // ray towards right of player
        Debug.DrawRay(playerPosition, GetFwd(player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad + Mathf.PI/2, playerPosition) * rayLength, Color.grey);
        // ray towards left of player
        Debug.DrawRay(playerPosition, GetFwd(player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad - Mathf.PI / 2, playerPosition) * rayLength, Color.grey);

        // list for storing all possible beta ranges
        // this will be useful if the player is in corner, we will have 4 gamma lines, hence 2 sets of beta
        List<float[]> betaRanges = new List<float[]>();

        // rays for north, east, south and west directions
        for (int i=0; i<4; i++)
        {
            // cast ray in a direction
            // make it red if hit else green
            if (Physics.Raycast(playerPosition, RayDirections[i], out rayArray[i], rayLength))
            {
                Debug.DrawRay(playerPosition, RayDirections[i]*rayLength, Color.red);
            }
            else Debug.DrawRay(playerPosition, RayDirections[i]*rayLength, Color.green);

            // cast ray in a direction and if it hits a collider
            if (Physics.Raycast(playerPosition, RayDirections[i], out rayArray[i], rayLength))
            {
                // find signed angle between the player's orientation and direction 
                // -- angle has negative sign when player's rotation along y-axis is positive and vice versa
                float angle = Vector3.SignedAngle(playerPosition - previousPosition, RayDirections[i], Vector3.up);
                angle = angle * Mathf.Deg2Rad;
                Debug.Log("Angle between player orientation and raydirection[" + i + "]: " + angle*Mathf.Rad2Deg);
                Debug.Log("ray.distance for ["+i+"]: " + rayArray[i].distance);
                
                // angle between the direction and a line of length rayLength
                float gamma = Mathf.Acos(rayArray[i].distance / rayLength);
                Debug.Log("gamma for ["+i+"]: " + gamma*Mathf.Rad2Deg);

                // two lines area formed to visualize angle gamma but draw only when gamma lines are not hitting the colliders
                Debug.DrawRay(playerPosition, GetFwd(gamma+(i)*Mathf.PI/2, playerPosition) * rayLength, Color.cyan);
                Debug.DrawRay(playerPosition, GetFwd(-gamma + (i) * Mathf.PI / 2, playerPosition) * rayLength, Color.cyan);

                // angles between gamma line and player's local right and left directions
                float angleBetaLeft;
                float angleBetaRight;
                angleBetaRight = Mathf.PI / 2 - (gamma + angle);
                angleBetaLeft = Mathf.PI / 2 - (gamma - angle);

                Debug.Log("angleBetaLeft [" + i + "]: " + angleBetaLeft*Mathf.Rad2Deg);
                Debug.Log("angleBetaRight [" + i + "]: " + angleBetaRight*Mathf.Rad2Deg);

                // if angleBetaLeft or Right are exclusively > = 90, only then their ranges should be added to list
                if(angleBetaLeft < 0 & angleBetaRight < 0) { Debug.Log("Wrong betas"); }
                if (angleBetaLeft >= 0)
                {
                    float[] betaRange = { - (Mathf.PI/2 - angleBetaLeft), - Mathf.PI/2 };
                    betaRanges.Add(betaRange);

                }
                if (angleBetaRight >= 0)
                {
                    float[] betaRange = { Mathf.PI / 2 - angleBetaRight, Mathf.PI / 2 };
                    betaRanges.Add(betaRange);
                }
            }
        }

        // printing all the ranges
        for(int i=0; i<betaRanges.Count; i++)
        {
            Debug.Log("BetaRange[" + i + "] : {" + betaRanges[i][0]*Mathf.Rad2Deg +"," + betaRanges[i][1] * Mathf.Rad2Deg + "}");
        }


        // Printing only the valid ranges, given the orientation and location of the player in play area

        // if the list of ranges is empty, this means, player is not close to boundaries
        if(betaRanges.Count == 0)
        {
            Debug.Log("BetaRange: { -90, 90 }");
        }
        // if there is one range in the list, then it means player has hit only one boundary and the orientation is such that there is only one range
        // if there is only one range, then it must be a valid range
        else if(betaRanges.Count == 1)
        {
            Debug.Log("BetaRange: {" + betaRanges[0][0] * Mathf.Rad2Deg + "," + betaRanges[0][1] * Mathf.Rad2Deg + "}");
        }

        // if there are two ranges in the list, this means the player has hit only one boundary and the orientation is such that there are two available ranges
        // this also means that the player is standing in a more perpendicular manner toward the boundary
        // both the ranges must be valid range
        else if(betaRanges.Count == 2)
        {
            Debug.Log("BetaRange: {" +betaRanges[0][0] * Mathf.Rad2Deg + "," + betaRanges[0][1] * Mathf.Rad2Deg + "}, " +
                "{ " +betaRanges[1][0] * Mathf.Rad2Deg + "," + betaRanges[1][1] * Mathf.Rad2Deg + "}");
        }

        // if there are 3 ranges in list, it means the player is standing near two adjecent boundaries. Due to orientation, 3 ranges are captured
        // in this case, to avoid any difficulties added due to corner of the walls, we will take only the range with minimum scope.
        // this will ensure that the resultant point will lay on the extreme left or extreme right of player and not closer to the corner.
        else if(betaRanges.Count == 3)
        {
            if(Mathf.Abs(betaRanges[2][1] - betaRanges[2][0]) > Mathf.Abs(betaRanges[0][1] - betaRanges[0][0]))
            {
                Debug.Log("BetaRange: {" + betaRanges[0][0] * Mathf.Rad2Deg + "," + betaRanges[0][1] * Mathf.Rad2Deg + "}");
            }
            else Debug.Log("BetaRange: {" + betaRanges[2][0] * Mathf.Rad2Deg + "," + betaRanges[2][1] * Mathf.Rad2Deg + "}");
        }

        // if there are 4 elements in list then it means the player is for sure stuck in the corner, because the probability of getting a point close to 
        // corner is very high here. So, to avoid corners, we can take a turn to +-135 degree. Another reason for not choosing any beta range is that in
        // current algorithm, there is an issue. Let's say the player is standing near x and z-axis corner, the gamma with x-axis and z-axis is calculated
        // but if the player is moved paraller x-axis only, the gamma is changed only for the other z-axis. This can lead to rays going out of boundaries of
        // the play area. Similarly, it can happen with z-axis also. If player is moved parallaly towards the  corner of boundary, the gamma will remain same.
        else if(betaRanges.Count == 4)
        {
            Debug.Log("Stuck in corner, cast rays to -135 or +135");
            Debug.DrawRay(playerPosition, (1f+rayLength) * GetFwd(player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad - (3 * Mathf.PI / 4), playerPosition), Color.cyan);
            Debug.DrawRay(playerPosition, (1f+rayLength) * GetFwd(player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad + (3 * Mathf.PI / 4), playerPosition), Color.cyan);
        }
    }

    private Vector3 GetFwd(float beta, Vector3 origin)
    {
        // beta should be in radians
        // returns a untit vector pointing in forward direction from the current point at an given angle
        Vector3 targetPoint = Vector3.zero;
        targetPoint.x = Mathf.Sin(beta) + origin.x;
        targetPoint.y = origin.y;
        targetPoint.z = Mathf.Cos(beta) + origin.z;
        return (targetPoint - origin).normalized;
    }
}
