using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
   All the manager classes provide static variables to keep the code clean and easy to read. 
*/
public class SceneManager : MonoBehaviour
{
    public GameObject playarea;
    public GameObject player;

    // this are taken from Inspector panel only for demo purposes. Ideally, should be read from metadata file
    public float pathSegmentLength;
    public float pathSegmentWidth;
    public int numberOfPathSegments;

    public static Vector3 playerStartingPosition_static;
    public static Vector3 playAreaDimensions_static;

    private Points _points;
    private Vector3 playAreaDimensions;
    private List<Vector3> presentPathPoints = new List<Vector3>();

    private PathMesh _pathMesh;
    private MetadataManager _metadataManager;
    void Start()
    {
        _metadataManager = new MetadataManager(this.pathSegmentWidth, this.pathSegmentLength, this.numberOfPathSegments); // this contructor only used for demo purpose; ideally it should read it from metadata file.
        playerStartingPosition_static = this.player.transform.position;

        this.playAreaDimensions = Vector3.Scale(this.playarea.transform.localScale, this.playarea.GetComponent<Mesh>().bounds.size);
        playAreaDimensions_static = this.playAreaDimensions;
        this._points = new Points();
        this._pathMesh = new PathMesh(_points); // instance to PathPoints class is passed because we wanted to set it's values in SceneManager.
        this._pathMesh.Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
