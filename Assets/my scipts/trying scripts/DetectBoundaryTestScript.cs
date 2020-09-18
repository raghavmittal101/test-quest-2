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

    // Start is called before the first frame update
    void Awake()
    {
        //this.db = new DetectBoundary();
        this.db = new DetectBoundaryFixedDirections(rayArrayLength, boundaryBufferWidth, pathLength, pathWidth);
        float beta = player.transform.rotation.eulerAngles.y*Mathf.Deg2Rad;
        this.point = player.transform.position;
        this.betaList.Add(beta);
        this.pointsList.Add(point);
    }

    private void Start()
    {
        GenerateNextPoint();
    }
    // Update is called once per frame
    void Update()
    {
        db.GenerateRays();
        if (Input.GetMouseButtonDown(0))
        {
            if (pointsList.Count >= 5) {
                pointsList.RemoveAt(0);
                betaList.RemoveAt(0);
                var p = player.transform.position;
                p = pointsList[2];
                player.transform.position = p;
            }
            GenerateNextPoint();
            
        }
        
        for (int i = 0; i < pointsList.Count - 1; i++)
            Debug.DrawLine(pointsList[i], pointsList[i+1], Color.gray);
        

    }

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

}
