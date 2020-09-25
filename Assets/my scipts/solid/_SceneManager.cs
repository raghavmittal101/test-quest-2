using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Manages the main scene generation
/// </summary>
public class _SceneManager : MonoBehaviour
{
    /// <summary>
    /// It contains the points which represent a path visible in the scene.
    /// Order of the points matters to represent a valid path.
    /// </summary>
    private List<Vector3> pointLocationsList = new List<Vector3>();

    /// <summary>
    /// It contains the angles along Y-axis associated with each point.
    /// The order of storage of angles matters for the associations to be valid.
    /// </summary>
    private List<float> betaList = new List<float>();

    private PathMesh pathMesh;
    private DetectBoundaryFixedDirections db;

    /// <summary>
    /// This is the starting position of the player in play area. 
    /// Only x and z components are used by the system.
    /// </summary>
    private Vector3 startLocation;

    /// <see cref="MetadataInputContext"/>
    private MetadataInputContext metadataInput;

    /// <see cref="InputDeviceContext"/>
    private InputDeviceContext inputDevice;

    private int numberOfPathSegmentsCovered = 0;
    
    // for demo purposes
    public GameObject playerGameObject;
    public GameObject plane;

    // should be preset through metadataInput when the program is run on VR
    /// <summary>
    /// Number of rays to be casted in range of -90 to +90.
    /// More rays means more optimized boundary avoidance streragy. 
    /// minimum 5 recommended. 
    /// </summary>
    public int rayArrayLength=10;

    /// <summary>
    /// Can be used to increase the boundary detection ray length.
    /// This can help in introducing extra space between generated path and boundaries of the play area for user's safety.
    /// Set to 0 by default.
    /// </summary>
    public float boundaryBufferWidth = 0.0f;

    /// <summary>
    /// Set to 2 by default.
    /// Can be used to increase the number of triagles in the mesh generated for the floor of path by dividing the path points into
    /// equidistant points by given value of <c>slice</c>.
    /// </summary>
    public int slices=2;

    /// <summary>
    /// Array of GameObjects which contains colliders to represent the boundary of the play-area.
    /// </summary>
    private GameObject[] boundaryCollider = new GameObject[4];

    public GameObject boundaryColliderPrefab;

    private void Awake()
    {
        // adapters
        this.inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();
        this.metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();

        this.pathMesh = new PathMesh();

        ///<summary>
        /// On game start, add the current position of the player to <c>pointLocationList</c>.
        ///</summary>
        this.pointLocationsList.Add(inputDevice.PlayerPosition());

        ///<summary>
        /// On game start, add the player's angle of rotation along Y-axis to <c>betaList</c>.
        ///</summary>
        this.betaList.Add(inputDevice.PlayerRotationAlongYAxis());

        db = new DetectBoundaryFixedDirections(rayArrayLength, boundaryBufferWidth, 
            metadataInput.PathSegmentLength(), metadataInput.PathWidth());
    }

    /// <summary>
    /// This method is responsible for:
    /// <list type="bullet">
    /// <item>Generate the first set of path segments. These segments are generated from the player's present position in the playarea using <seealso cref="GenerateNewPathSegment"/></item>
    /// <item>Place colliders to mark the boundary of play area. Boundary detection system depends on these colliders using <seealso cref="SpawnBoundaryColliders"/></item>
    /// <item>Resize the plane according to input dimensions using <seealso cref="resizePlane"/></item>
    /// </list>
    /// </summary>
    void Start()
    {
        resizePlane();
        SpawnBoundaryColliders();

        // for demo purposes
        playerGameObject.transform.position = inputDevice.PlayerPosition();

        for (int i = 0; i < metadataInput.VisiblePathSegmentCount(); i++)
        {
            this.GenerateNewPathSegment();
            Debug.DrawLine(pointLocationsList[i], pointLocationsList[i+1], Color.cyan);
        }
        this.RenderMesh(this.pathMesh.GetMesh(this.pointLocationsList, metadataInput.PathWidth(), slices));
        
    }

    void Update()
    {
        Debug.Log("player area dimensions: x: "+ inputDevice.PlayAreaDimensions() +", y: " +inputDevice.PlayAreaDimensions().y + ", z: "+inputDevice.PlayAreaDimensions().z);
        // will be done through collider triggers instead of mouse click in future
        if (this.inputDevice.ButtonPressed())
        {
            // Spawning the paths again to check if this is the reason behind colliders not spawning properly with playarea dimensions
            // it may also mean that the playarea dimensions are not set properly.
            foreach(GameObject obj in boundaryCollider)
            {
                Destroy(obj);
            }
            SpawnBoundaryColliders();
            resizePlane();

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
        db.GenerateRays(pointLocationsList[pointLocationsList.Count-1], betaList[betaList.Count-1]);
    }

    private void resizePlane()
    {
        Vector3 planeScale = plane.transform.localScale;
        Vector3 size = plane.GetComponent<Renderer>().bounds.size;
        planeScale.z = inputDevice.PlayAreaDimensions().z * planeScale.z / size.z;
        planeScale.x = inputDevice.PlayAreaDimensions().x * planeScale.x / size.x;
        plane.transform.localScale = planeScale;
    }

    private void SpawnBoundaryColliders()
    {
        // place colliders on boundaries of play area
        Vector3[] boundaryPositions = new Vector3[4];

        Vector3 playAreaDimensions = inputDevice.PlayAreaDimensions();
        boundaryPositions[0] = new Vector3(0f, 0f, -playAreaDimensions.z / 2);
        boundaryPositions[1] = new Vector3(-playAreaDimensions.x / 2, 0f, 0f);
        boundaryPositions[2] = new Vector3(0f, 0f, playAreaDimensions.z / 2);
        boundaryPositions[3] = new Vector3(playAreaDimensions.x / 2, 0f, 0f);

        for (int i = 0; i < 4; i++)
        {
            var go = Instantiate(boundaryColliderPrefab, boundaryPositions[i], Quaternion.Euler(0f, i * 90f, 0f));
            var scale = go.transform.localScale;
            if (boundaryPositions[i].z == 0f) scale.x = playAreaDimensions.z;
            else scale.x = playAreaDimensions.x;
            go.transform.localScale = scale;
            boundaryCollider[i] = go;
        }
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
