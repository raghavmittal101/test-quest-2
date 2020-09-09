using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float pathSegmentLength;
    private float pathWidth;
    private int visiblePathSegmentCount;
    private List<Vector3> pointLocationsList = new List<Vector3>();
    private Material material;
    private Vector3 playAreaDimension;
    private float beta;
    private PathMesh pathMesh;
    private _Point point;
    private PathSegment pathSegment;
    MetadataManualInput metadataInput;
    // ManualDeviceInput deviceInput = new ManualDeviceInput();
    ManualDeviceInput deviceInput;
    private Vector3 startLocation;
    private int zoneId;
    private int numberOfSegmentsCovered=0;

    // for demo purposes
    public GameObject playerGameObject;

    private void Awake()
    {
        this.metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataManualInput>();
        this.deviceInput = GameObject.Find("ScriptObject").GetComponent<ManualDeviceInput>();
        this.pathSegmentLength = metadataInput.PathSegmentLength();
        this.visiblePathSegmentCount = metadataInput.VisiblePathSegmentCount();
        this.pathWidth = metadataInput.PathWidth();
        this.material = metadataInput.Material();
        this.pathMesh = new PathMesh(this.pathWidth);
        this.point = new _Point();
        this.startLocation = GameObject.Find("ScriptObject").GetComponent<ManualDeviceInput>().StartingPosition();
    }
    void Start()
    {
        playerGameObject.transform.position = startLocation;

        this.playAreaDimension = deviceInput.PlayAreaDimensions();
       // Debug.Log("startLocation: " + this.startLocation.x);
        pointLocationsList.Add(this.startLocation);

        for (int i=0; i<this.visiblePathSegmentCount; i++)
        {
            this.GenerateNewPathSegment();
        }
        this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numberOfSegmentsCovered += 1;

            if (numberOfSegmentsCovered <= visiblePathSegmentCount / 2 + 1)
            {// demo purpose
                playerGameObject.transform.position = pointLocationsList[numberOfSegmentsCovered];
            }
            if(numberOfSegmentsCovered >= this.visiblePathSegmentCount / 2 + 1)
            {
                pointLocationsList.RemoveAt(0);
                this.GenerateNewPathSegment();
                this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList));

                // demo purpose
                playerGameObject.transform.position = pointLocationsList[visiblePathSegmentCount/2 + 1];
            }
        }
    }

    private void RenderMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
    }

    private void GenerateNewPathSegment()
    {
        this.startLocation = this.pointLocationsList[this.pointLocationsList.Count - 1];
        this.zoneId = this.point.GetZoneId(this.startLocation, this.playAreaDimension, this.pathSegmentLength);
       // Debug.Log("ZoneId: "+ this.zoneId);
       // Debug.Log("playAreaDimension: " + this.playAreaDimension);
       // Debug.Log("pathSegmentLength: "+ this.pathSegmentLength);
        Beta B = new Beta(this.startLocation, this.zoneId);
        this.pathSegment = new PathSegment(startLocation, this.pathSegmentLength, (float)B.GetBeta());
        this.pointLocationsList.Add(pathSegment.GetEndLocation());

    }
}
