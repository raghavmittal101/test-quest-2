using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
   All the manager classes provide static variables to keep the code clean and easy to read. 
*/
public class SceneManagerOld : MonoBehaviour
{
    public GameObject playarea;
    public GameObject player;

    // this are taken from Inspector panel only for demo purposes. Ideally, should be read from metadata file
    public float pathSegmentLength;
    public float pathSegmentWidth;
    public int numberOfPathSegments;

    public static Vector3 playerStartingPosition_static;
    public static Vector3 playAreaDimensions_static;
    public static int numberOfPathSegmentsCovered_static = 0;
    public static List<Vector3> pointsVisibleInScene_static = new List<Vector3>(); // contains visible path points

    private Points _points;
    private Vector3 playAreaDimensions;

    private PathMesh _pathMesh;
    private MetadataManager _metadataManager;

    private void Awake()
    {
        _metadataManager = new MetadataManager(this.pathSegmentWidth, this.pathSegmentLength, this.numberOfPathSegments); // this contructor only used for demo purpose; ideally it should read it from metadata file.
        playerStartingPosition_static = this.player.transform.position;

        this.playAreaDimensions = Vector3.Scale(this.playarea.transform.localScale, this.playarea.GetComponent<Mesh>().bounds.size);
        playAreaDimensions_static = this.playAreaDimensions;

        this._points = new Points();
    }
    void Start()
    {
        SceneManager.pointsVisibleInScene_static.Add(_points.GetNextPoint(true)); // this way we generate p1

        int m = MetadataManager.numberOfPathSegments_static;
        for (int i=0; i<m-1; i++) // this way we generate p2, p3, p4...p(m)
        {
           SceneManager.pointsVisibleInScene_static.Add(_points.GetNextPoint()); 
        }
        this._pathMesh.Render();

        // this._pathMesh = new PathMesh(_points); // instance to PathPoints class is passed because we wanted to set it's values in SceneManager.
        // this._pathMesh.Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
