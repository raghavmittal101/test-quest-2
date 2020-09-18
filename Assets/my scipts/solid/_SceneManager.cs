using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SceneManager : MonoBehaviour
{
    private List<Vector3> pointLocationsList = new List<Vector3>();
    private List<float> betaList = new List<float>();
    private PathMesh pathMesh;
    private DetectBoundaryFixedDirections db;

    private Vector3 startLocation;
    private int numberOfPathSegmentsCovered = 0;

    // input settings
    MetadataInputContext metadataInput;
    InputDeviceContext inputDevice;

    // for demo purposes
    public GameObject playerGameObject;
    public GameObject plane;

    // should be preset through metadataInput when the program is run on VR
    public int rayArrayLength=10;
    public float boundaryBufferWidth = 0.5f;
    public int slices=2;

    // For setting boundary colliders
    private GameObject[] boundaryCollider = new GameObject[4];
    public GameObject boundaryColliderPrefab;

    private void Awake()
    {
        this.inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();
        this.metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();

        this.pathMesh = new PathMesh();

        this.pointLocationsList.Add(inputDevice.PlayerPosition());
        this.betaList.Add(inputDevice.PlayerRotationAlongYAxis());

        db = new DetectBoundaryFixedDirections(rayArrayLength, boundaryBufferWidth, 
            metadataInput.PathSegmentLength(), metadataInput.PathWidth());
    }
    void Start()
    {
        SpawnBoundaryColliders();

        // for demo purposes
        playerGameObject.transform.position = inputDevice.PlayerPosition();

        for (int i = 0; i < metadataInput.VisiblePathSegmentCount(); i++)
        {
            this.GenerateNewPathSegment();
            Debug.DrawLine(pointLocationsList[i], pointLocationsList[i+1], Color.cyan);
        }
        this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList, metadataInput.PathWidth(), slices));

        Vector3 planeScale = plane.transform.localScale;
        planeScale.z = inputDevice.PlayAreaDimensions().z;
        planeScale.x = inputDevice.PlayAreaDimensions().x;
        plane.transform.localScale = planeScale;

    }

    private void SpawnBoundaryColliders()
    {
        // place colliders on boundaries of play area
        Vector3[] boundaryPositions = new Vector3[4];

        Vector3 playAreaDimensions = inputDevice.PlayAreaDimensions();
        boundaryPositions[0] = new Vector3(0f, 0f, -playAreaDimensions.z*10 / 2);
        boundaryPositions[1] = new Vector3(-playAreaDimensions.x*10 / 2, 0f, 0f);
        boundaryPositions[2] = new Vector3(0f, 0f, playAreaDimensions.z*10 / 2);
        boundaryPositions[3] = new Vector3(playAreaDimensions.x*10 / 2, 0f, 0f);

        for (int i = 0; i < 4; i++)
        {
            var go = Instantiate(boundaryColliderPrefab, boundaryPositions[i], Quaternion.Euler(0f, i*90f, 0f));
            var scale = go.transform.localScale;
            if (boundaryPositions[i].z == 0f) scale.x = playAreaDimensions.z*10;
            else scale.x = playAreaDimensions.x*10;
            go.transform.localScale = scale;
            boundaryCollider[i] = go;
        }
    }
    
    void Update()
    {
        // will be done through collider triggers instead of mouse click in future
        if (Input.GetMouseButtonDown(0))
        {
            numberOfPathSegmentsCovered += 1;
            if (pointLocationsList.Count >= this.metadataInput.VisiblePathSegmentCount() + 1)
            {
                pointLocationsList.RemoveAt(0);
                betaList.RemoveAt(0);
                
                // demo purpose
                playerGameObject.transform.position = pointLocationsList[metadataInput.VisiblePathSegmentCount() / 2 + 1];
            }
            else
            {
                playerGameObject.transform.position = pointLocationsList[numberOfPathSegmentsCovered];
            }
            this.GenerateNewPathSegment();
            this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList, metadataInput.PathWidth(), slices));

        }
        for (int i = 0; i < pointLocationsList.Count - 1; i++)
            Debug.DrawLine(pointLocationsList[i], pointLocationsList[i + 1], Color.gray);
        db.GenerateRays();
    }

    private void RenderMesh(Mesh mesh)
    {
        GameObject pathMeshGameObj = GameObject.FindGameObjectsWithTag("PathMesh")[0];
        var meshFilterMesh = pathMeshGameObj.GetComponent<MeshFilter>().mesh;
        meshFilterMesh = mesh;
        pathMeshGameObj.GetComponent<MeshFilter>().mesh = meshFilterMesh;
        var meshRendererMaterial = pathMeshGameObj.GetComponent<MeshRenderer>().material;
        meshRendererMaterial = metadataInput.PathMaterial();
        pathMeshGameObj.GetComponent<MeshRenderer>().material = meshRendererMaterial;
    }
    

    private void GenerateNewPathSegment()
    {
        float lastBeta = this.betaList[this.betaList.Count-1];
        Vector3 lastPoint = this.pointLocationsList[this.pointLocationsList.Count - 1];
        float newBeta = db.GetBeta(lastPoint, lastBeta);
        Vector3 newPoint = new Vector3(lastPoint.x + metadataInput.PathSegmentLength() * Mathf.Sin(newBeta), 
            0f, 
            lastPoint.z + metadataInput.PathSegmentLength() * Mathf.Cos(newBeta));

        this.pointLocationsList.Add(newPoint);
        this.betaList.Add(newBeta);

        // Debug.Log("ZoneId: "+ this.zoneId);
        // Debug.Log("playAreaDimension: " + this.playAreaDimension);
        // Debug.Log("pathSegmentLength: "+ this.pathSegmentLength);
    }
}
