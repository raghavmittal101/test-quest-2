using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoundaryTestScript : MonoBehaviour
{
    private DetectBoundary db;
    public Vector3 point;
    public GameObject player;
    public float pathLength;
    public float pathWidth;
    public List<float> betaList = new List<float>();
    public List<Vector3> pointsList = new List<Vector3>();
    public float[] betaRange = new float[2];
    

    // Start is called before the first frame update
    void Awake()
    {
        this.db = new DetectBoundary();
        float beta = player.transform.rotation.eulerAngles.y;
        Debug.Log("Awake: beta: " + beta*Mathf.Rad2Deg);
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
            
            if (Input.GetMouseButtonDown(0))
            {
            if (pointsList.Count >= 5) { pointsList.RemoveAt(0); }
                betaList.RemoveAt(0);
                GenerateNextPoint();
            }
        

    }

    private void GenerateNextPoint()
    {
        float lastBeta = betaList[betaList.Count - 1];
        
        Vector3 lastPoint = pointsList[pointsList.Count - 1];
        betaRange = db.GetBetaRange(lastPoint, lastBeta, pathLength, pathWidth);
        float newBeta;
        if (betaRange[0] != betaRange[1]) newBeta = Random.Range(lastBeta + betaRange[0], lastBeta + betaRange[1]);
        else newBeta = lastBeta + betaRange[0];
        Debug.Log("GenerateNextPoint: betaRange: " + betaRange[0] * Mathf.Rad2Deg + ", " + betaRange[1] * Mathf.Rad2Deg);
        Debug.Log("GenerateNextPoint: newBeta: " + newBeta*Mathf.Rad2Deg);
        Vector3 newPoint = new Vector3(lastPoint.x + pathLength * Mathf.Sin(lastBeta), 0f, lastPoint.z + pathLength * Mathf.Cos(lastBeta));
        this.betaList.Add(newBeta);
        this.pointsList.Add(newPoint);
        GenerateLines();
    }

    private void GenerateLines()
    {
        for (int i = 0; i < pointsList.Count-1; i++)
            Debug.DrawLine(pointsList[i], pointsList[i + 1], Color.gray, 5f);
    }

}
