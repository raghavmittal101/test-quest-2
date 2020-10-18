using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoundaryTestScript : MonoBehaviour
{
    private DetectBoundaryFixedDirections db;
    private WallsSpawner _WallSpawner;
    private int steps = 0;
    private List<GameObject> pathTriggerColliderList;
    private List<GameObject> leftWalls, rightWalls;
    private int imageIndex;

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
    public Vector3 tasveerDimensions = new Vector3(0.7f, 0.7f, 0.01f);
    public List<Texture> imageList = new List<Texture>();
    public float tasveerLeftRightPadding;
    public bool repeatPictures = false;
    public GameObject pathTriggerCollider; // holds the reference to prefab

    void Awake()
    {
        //this.db = new DetectBoundary();
        this.db = new DetectBoundaryFixedDirections(rayArrayLength, boundaryBufferWidth, pathLength, pathWidth);
        float beta = player.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        this.point = player.transform.position;
        this.betaList.Add(beta); // adding beta_0 that is player's rotation along Y axis
        this.pointsList.Add(point); // adding P_0 that is player's initial position 
        this._WallSpawner = new WallsSpawner();
        leftWalls = new List<GameObject>();
        rightWalls = new List<GameObject>();
        imageIndex = imageList.Count - 1; // number of images. Images will be placed in reverse order
        pathTriggerColliderList = new List<GameObject>();
    }

    private void Start()
    {
        List<Vector3>[] leftRightPoints;
        GenerateNextPoint();
        for (int i = 0; i < numberOfPathSegments; i++)
        {
            GenerateNextPoint();
            leftRightPoints = GenerateLeftRightPoints(pointsList, pathWidth);
            var lastLeftPoint = leftRightPoints[0][leftRightPoints[0].Count - 2];
            var lastRightPoint = leftRightPoints[1][leftRightPoints[1].Count - 2];
            SpawnPathTrigger(lastLeftPoint, lastRightPoint);
            if (spawnWallsFlag)
            {
                leftWalls.Add(_WallSpawner.GenerateWall(ref leftRightPoints[0], ref wallPrefab));
                rightWalls.Add(_WallSpawner.GenerateWall(ref leftRightPoints[1], ref wallPrefab));
                if (imageIndex >= 0) { PlaceOnWall(leftWalls[leftWalls.Count - 1], true); };
        // after above step:
                if (imageIndex >= 0) { PlaceOnWall(rightWalls[rightWalls.Count - 1], false); }
            }
        }
        // length of pointsList is numberOfPathSegments+1
        // 1 extra point is generated to keep the walls aligned properly

    }

    void Update()
    {
        List<Vector3>[] leftRightPoints;
        if (Input.GetMouseButtonDown(0))
        {
            if (steps <= numberOfPathSegments / 2)
            {

                var p = player.transform.position;
                p = pointsList[steps];
                player.transform.position = p;
                steps += 1;
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
                Destroy(pathTriggerColliderList[0]);
                pathTriggerColliderList.RemoveAt(0);
                leftRightPoints = GenerateLeftRightPoints(pointsList, pathWidth);
                var lastLeftPoint = leftRightPoints[0][leftRightPoints[0].Count - 2];
                var lastRightPoint = leftRightPoints[1][leftRightPoints[1].Count - 2];
                SpawnPathTrigger(lastLeftPoint, lastRightPoint);
                if (spawnWallsFlag)
                {
                    leftWalls.Add(_WallSpawner.GenerateWall(ref leftRightPoints[0], ref wallPrefab));
                    rightWalls.Add(_WallSpawner.GenerateWall(ref leftRightPoints[1], ref wallPrefab));
                    if (imageIndex < 0 && repeatPictures) { imageIndex = imageList.Count - 1; }
                    if (imageIndex >= 0) { PlaceOnWall(leftWalls[leftWalls.Count - 1], true); }
                    if (imageIndex < 0 && repeatPictures) { imageIndex = imageList.Count - 1; }
                    if (imageIndex >= 0) { PlaceOnWall(rightWalls[rightWalls.Count - 1], false); }
                }

            }
        }

        for (int i = 0; i < pointsList.Count - 1; i++)
        {
            Debug.DrawLine(pointsList[i], pointsList[i + 1], Color.gray);
        }
        db.GenerateRays(pointsList[pointsList.Count - 1], betaList[betaList.Count - 1]);
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
        else newBeta = lastBeta + betaRange[0]; // if upper and lower bound of beta are equal
        Debug.Log("GenerateNextPoint: betaRange: " + betaRange[0] * Mathf.Rad2Deg + ", " + betaRange[1] * Mathf.Rad2Deg);
        Debug.Log("GenerateNextPoint: newBeta: " + newBeta * Mathf.Rad2Deg);
        Vector3 newPoint = new Vector3(lastPoint.x + pathLength * Mathf.Sin(newBeta), 0f, lastPoint.z + pathLength * Mathf.Cos(newBeta));
        this.betaList.Add(newBeta);
        this.pointsList.Add(newPoint);
    }

    /// <summary>
    /// Generate a new point at a deviation of beta angles from direction of origin point.
    /// </summary>
    /// <param name="beta">beta should be in radians</param>
    /// <param name="origin"></param>
    /// <returns>unit vector pointing in forward direction from the current vector at an given angle</returns>
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
    /// <returns>List[List of left points, list of right points] corresponding to input list of points.</returns>
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
    /// Spawn trigger colliders at joints in path. Spawned GameObjects are added to <see cref="pathTriggerColliderList"/>.
    /// </summary>
    /// <param name="leftRightPoints">List of two points between which the collider should be placed.</param>
    private void SpawnPathTrigger(Vector3 leftPoint, Vector3 rightPoint)
    {
        var obj = Instantiate(pathTriggerCollider);
        var localScale = new Vector3((rightPoint - leftPoint).magnitude, 1f, 0.1f);
        obj.transform.localScale = localScale;
        float wallRotAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), (rightPoint - leftPoint).normalized, Vector3.up);
        Vector3 rotation = obj.transform.localEulerAngles;
        rotation = new Vector3(0f, wallRotAngleAlongY, 0f);
        obj.transform.localEulerAngles = rotation;
        obj.name = pathTriggerColliderList.Count.ToString();
        var pos = (rightPoint + leftPoint) / 2;
        pos.y = localScale.y / 2;
        obj.transform.position = pos;
        pathTriggerColliderList.Add(obj);
    }

    /// <summary>
    /// Build a photoframe by adding image as texture to it.
    /// Place the photoframe on wall which has enough space to accomodate it.
    /// </summary>
    /// <param name="wallObj">Reference to wall Gameobject on which tasveer has to be placed.</param>
    /// <param name="isLeft">If it's a left side wall, then isLeft should be true.</param>
    private void PlaceOnWall(GameObject wallObj, bool isLeft)
    {
        Vector3 localPosition, localRotation = Vector3.zero;
        Vector3 wallScale = wallObj.transform.localScale; // scale of tasveer will be divided by wallscale to mantain the original scale of tasveer which otherwise will be lost because it's child of wall.
        if (isLeft) { localPosition = new Vector3(0f, 0f, -1f); localRotation = new Vector3(180f, 0f, 180f); }
        else localPosition = new Vector3(0f, 0f, 1f);

        float numberOfTasveer_float = wallScale.x / (tasveerDimensions.x + 2 * tasveerLeftRightPadding);

        for (int i = 0; i < (int)numberOfTasveer_float; i++)
        {
            if (imageIndex >= 0)
            {
                float tasveerWidth = tasveerDimensions.x + 2 * tasveerLeftRightPadding;
                localPosition.x = (tasveerWidth / 2 + (tasveerWidth * i) - (wallScale.x / 2)) / wallScale.x; // the position of tasveer relative to wall
                GameObject tasveerObj = Instantiate(tasveer, wallObj.transform); // added as child of wall to simplify positioning
                tasveerObj.transform.localPosition = localPosition;
                tasveerObj.transform.localRotation = Quaternion.Euler(localRotation);
                tasveerObj.transform.localScale = new Vector3(tasveerDimensions.x / wallScale.x, tasveerDimensions.y / wallScale.y, tasveerDimensions.z); // no need to divide tasveerDimensions.z as the thickness of wall is constant in scene
                AddImageTexture(ref tasveerObj, imageList[imageIndex--]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tasveerObj">reference to GameObject created for adding image texture</param>
    /// <param name="image">Image/painting/poster in Texture form to place on wall.</param>
    private void AddImageTexture(ref GameObject tasveerObj, Texture image)
    {
        Renderer tasveerRenderer = tasveerObj.GetComponents<Renderer>()[0];
        tasveerRenderer.material.mainTexture = image;
    }
}
