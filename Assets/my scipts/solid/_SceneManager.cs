using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SceneManager : MonoBehaviour
{
    private PathSegment pathSegment;
    private List<Vector3> pointLocationsList = new List<Vector3>();
    private PathMesh pathMesh;

    private Vector3 startLocation;
    private int zoneId;
    private int numberOfPathSegmentsCovered = 0;

    // input settings
    MetadataInputContext metadataInput;
    InputDeviceContext inputDevice;

    // for demo purposes
    public GameObject playerGameObject;

    // For setting boundary colliders
    private GameObject[] boundaryCollider = new GameObject[4];
    public GameObject boundaryColliderPrefab;

    private void Awake()
    {
        this.inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();
        this.metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();

        this.pathMesh = new PathMesh(metadataInput.PathWidth());
    }
    void Start()
    {
        SpawnBoundaryColliders();

        playerGameObject.transform.position = inputDevice.StartingPosition();

        pointLocationsList.Add(inputDevice.StartingPosition());

        for (int i = 0; i < metadataInput.VisiblePathSegmentCount(); i++)
        {
            this.GenerateNewPathSegment();
        }
        this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList));


    }

    private void SpawnBoundaryColliders()
    {
        // place colliders on boundries of play area
        Vector3[] boundaryPositions = new Vector3[4];

        Vector3 playAreaDimensions = inputDevice.PlayAreaDimensions();
        boundaryPositions[0] = new Vector3(0f, 5f, -playAreaDimensions.z / 2);
        boundaryPositions[1] = new Vector3(-playAreaDimensions.x / 2, 5f, 0f);
        boundaryPositions[2] = new Vector3(0f, 5f, playAreaDimensions.z / 2);
        boundaryPositions[3] = new Vector3(playAreaDimensions.x / 2, 5f, 0f);

        for (int i = 0; i < 4; i++)
        {
            var go = Instantiate(boundaryColliderPrefab, boundaryPositions[i], Quaternion.Euler(0f, i*90f, 0f));
            var scale = go.transform.localScale;
            if (boundaryPositions[i].z == 0f) scale.x = playAreaDimensions.z;
            else scale.x = playAreaDimensions.x;
            go.transform.localScale = scale;
            boundaryCollider[i] = go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numberOfPathSegmentsCovered += 1;

            if (numberOfPathSegmentsCovered <= metadataInput.VisiblePathSegmentCount() / 2 + 1)
            {
                playerGameObject.transform.position = pointLocationsList[numberOfPathSegmentsCovered];
            }
            if(numberOfPathSegmentsCovered >= this.metadataInput.VisiblePathSegmentCount() / 2 + 1)
            {
                pointLocationsList.RemoveAt(0);
                this.GenerateNewPathSegment();
                this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList));

                // demo purpose
                playerGameObject.transform.position = pointLocationsList[metadataInput.VisiblePathSegmentCount() / 2 + 1];
            }
        }
    }

    private void RenderMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = metadataInput.PathMaterial();
    }

    private void GenerateNewPathSegment()
    {
        this.startLocation = this.pointLocationsList[this.pointLocationsList.Count - 1];
        this.zoneId = startLocation.GetZoneId(inputDevice.PlayAreaDimensions(), metadataInput.PathSegmentLength());
        float beta = Beta.GenerateBeta(this.startLocation, this.zoneId);
        //Debug.Log("_SceneManager.cs: beta: " + beta);
        this.pathSegment = new PathSegment(startLocation, metadataInput.PathSegmentLength(), beta);
        this.pointLocationsList.Add(pathSegment.GetEndLocation());

        // Debug.Log("ZoneId: "+ this.zoneId);
        // Debug.Log("playAreaDimension: " + this.playAreaDimension);
        // Debug.Log("pathSegmentLength: "+ this.pathSegmentLength);
    }
}
