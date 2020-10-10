using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoundaryTestScript : MonoBehaviour
{
    private DetectBoundaryFixedDirections db;
    public Vector3 point;
    public GameObject player;
    public float pathLength;
    public float pathWidth;
    public List<float> betaList = new List<float>();
    public List<Vector3> pointsList = new List<Vector3>();
    public float[] betaRange = new float[2];
    public int rayArrayLength = 5;
    public float boundaryBufferWidth = 0.5f;
    public int numberOfPathSegments;
    public GameObject wallPrefab;
    public GameObject tasveer;
    public bool spawnWallsFlag = false; // for simulation purpose to on/off wall spawning

    private WallsSpawner _WallSpawner;
    private int steps = 0;

    List<GameObject> leftWalls, rightWalls;
    public List<Texture> imageList = new List<Texture>();
    int imageIndex;

    void Awake()
    {
        //this.db = new DetectBoundary();
        this.db = new DetectBoundaryFixedDirections(rayArrayLength, boundaryBufferWidth, pathLength, pathWidth);
        float beta = player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad;
        this.point = player.transform.position;
        this.betaList.Add(beta);
        this.pointsList.Add(point);
        this._WallSpawner = new WallsSpawner();
        leftWalls = new List<GameObject>();
        rightWalls = new List<GameObject>();
        imageIndex = imageList.Count - 1; // number of images. Images will be placed in reverse order
    }

    private void Start()
    {
        List<Vector3>[] leftRightPoints;
        GenerateNextPoint();
        for (int i=0; i<numberOfPathSegments; i++)
        {
            GenerateNextPoint();
            leftRightPoints = GenerateLeftRightPoints(pointsList, pathWidth);
            if (spawnWallsFlag)
            {
                leftWalls.Add(_WallSpawner.GenerateWall(leftRightPoints[0], wallPrefab));
                rightWalls.Add(_WallSpawner.GenerateWall(leftRightPoints[1], wallPrefab));
                if(imageIndex >= 0) { PlaceOnWall(leftWalls[leftWalls.Count - 1], true, imageList[imageIndex--]); };
                if(imageIndex >= 0) { PlaceOnWall(rightWalls[rightWalls.Count - 1], false, imageList[imageIndex--]); }
            }
        }
        // after above step:
        // length of pointsList is numberOfPathSegments+1
        // 1 extra point is generated to keep the walls aligned properly
        
    }

    void Update()
    {
        List<Vector3>[] leftRightPoints;
        if (Input.GetMouseButtonDown(0))
        {
            if (steps <= numberOfPathSegments/2)
            {
                
                var p = player.transform.position;
                p = pointsList[steps];
                player.transform.position = p;
                steps+=1;
            }

            else
            {
                pointsList.RemoveAt(0);
                betaList.RemoveAt(0);
                GenerateNextPoint();
                var p = player.transform.position;
                p = pointsList[numberOfPathSegments / 2];
                player.transform.position = p;

                Destroy(leftWalls[0]);
                leftWalls.RemoveAt(0);
                Destroy(rightWalls[0]);
                rightWalls.RemoveAt(0);
                leftRightPoints = GenerateLeftRightPoints(pointsList, pathWidth);
                if (spawnWallsFlag)
                {
                    leftWalls.Add(_WallSpawner.GenerateWall(leftRightPoints[0], wallPrefab));
                    rightWalls.Add(_WallSpawner.GenerateWall(leftRightPoints[1], wallPrefab));
                    if (imageIndex >= 0) { PlaceOnWall(leftWalls[leftWalls.Count - 1], true, imageList[imageIndex--]); };
                    if (imageIndex >= 0) { PlaceOnWall(rightWalls[rightWalls.Count - 1], false, imageList[imageIndex--]); }
                }
                
            }
        }
        

        for (int i = 0; i < pointsList.Count - 1; i++)
        {
            Debug.DrawLine(pointsList[i], pointsList[i + 1], Color.gray);
        }
        db.GenerateRays(pointsList[pointsList.Count-1], betaList[betaList.Count-1]);
        //_WallSpawner.GenerateWalls(pointsList, betaList, pathWidth);
        List<Vector3>[] _leftRightPoints = GenerateLeftRightPoints(pointsList, pathWidth);
    }

    /// <summary>
    /// Generates a new point based on last element in <see cref="pointsList"/> and <see cref="betaList"/>.
    /// Appends the new point to <see cref="pointsList"/> and new beta to <see cref="betaList"/>.
    /// </summary>
    private void GenerateNextPoint()
    {
        float lastBeta = betaList[betaList.Count - 1];
        
        Vector3 lastPoint = pointsList[pointsList.Count - 1];
        betaRange = db.GetBetaRange(lastPoint, lastBeta);
        float newBeta;
        if (betaRange[0] != betaRange[1]) newBeta = Random.Range(lastBeta + betaRange[0], lastBeta + betaRange[1]);
        else newBeta = lastBeta + betaRange[0];
        Debug.Log("GenerateNextPoint: betaRange: " + betaRange[0] * Mathf.Rad2Deg + ", " + betaRange[1] * Mathf.Rad2Deg);
        Debug.Log("GenerateNextPoint: newBeta: " + newBeta*Mathf.Rad2Deg);
        Vector3 newPoint = new Vector3(lastPoint.x + pathLength * Mathf.Sin(newBeta), 0f, lastPoint.z + pathLength * Mathf.Cos(newBeta));
        this.betaList.Add(newBeta);
        this.pointsList.Add(newPoint);
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

    /// <summary>
    /// Generates points to repsent the width of path and shape of path. 
    /// Can be used to spawn walls or generate mesh by using returned points as vertices.
    /// </summary>
    /// <param name="points">List of points representing the direction of path segments.</param>
    /// <param name="pathWidth"></param>
    /// <returns>[Left point, Right point] to the last element of the points list input</returns>
    private List<Vector3>[] GenerateLeftRightPoints(List<Vector3> points, float pathWidth)
    {
        List<Vector3> leftPoints = new List<Vector3>();
        List<Vector3> rightPoints = new List<Vector3>();

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 forward = Vector3.zero;
            if (i < points.Count - 1)
            {
                forward += points[i + 1] - points[i];
            }
            if (i > 0)
            {
                forward += points[i] - points[i - 1];
            }

            forward.Normalize();
            Vector3 left = new Vector3(-forward.z, 0, forward.x);

            leftPoints.Add(points[i] + left * pathWidth * 0.5f); // left point
            rightPoints.Add(points[i] - left * pathWidth * 0.5f); // right point

        }

        List<Vector3>[] array = new List<Vector3>[2];
        array[0] = leftPoints;
        array[1] = rightPoints;

        for (int i = 1; i < leftPoints.Count; i++)
        {
            Debug.DrawLine(leftPoints[i - 1], leftPoints[i], Color.yellow);
            Debug.DrawLine(rightPoints[i - 1], rightPoints[i], Color.yellow);
        }

        return array;
    }

    /// <summary>
    /// Place a tasveer prefab on wall.
    /// </summary>
    /// <param name="wallObj">Reference to wall Gameobject on which tasveer has to be placed.</param>
    /// <param name="isLeft">If it's a left side wall, then isLeft should be true.</param>
    /// <param name="image">Image/painting/poster in Texture form to place on wall.</param>
    private void PlaceOnWall(GameObject wallObj, bool isLeft, Texture image)
    {
        GameObject tasveerObj = Instantiate(tasveer, wallObj.transform);
        Vector3 localPosition, localRotation=Vector3.zero;
        if (isLeft) { localPosition = new Vector3(0f, 0f, -1f); localRotation = new Vector3(180f, 0f, 180f); }
        else localPosition = new Vector3(0f, 0f, 1f);
       // localPosition = new Vector3(0f, 0f, 1f);
        tasveerObj.transform.localPosition = localPosition;
        tasveerObj.transform.localRotation = Quaternion.Euler(localRotation);
        Vector3 wallScale = wallObj.transform.localScale; // scale of tasveer will be divided by wallscale to mantain the original scale of tasveer which otherwise will be lost because it's child of wall.
        tasveerObj.transform.localScale = new Vector3(0.7f/wallScale.x, 0.7f/wallScale.y, 0.1f); // not done along z-axis, the depth of wall is less than tasveer
        Renderer tasveerRenderer = tasveerObj.GetComponents<Renderer>()[0];
        tasveerRenderer.material.mainTexture = image;
    }
}
