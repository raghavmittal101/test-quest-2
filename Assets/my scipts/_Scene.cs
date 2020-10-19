using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    /// <summary><see cref="MetadataInputContext"/></summary>
    private MetadataInputContext metadataInput;

    /// <summary><see cref="InputDeviceContext"/></summary>
    private InputDeviceContext inputDevice;

    _Path path;
    Spawner spawner;

    /// <summary>
    /// Array of GameObjects which contains colliders to represent the boundary of the play-area.
    /// </summary>
    private GameObject[] boundaryCollider = new GameObject[4];

    public GameObject planeObject;

    private void Awake()
    {
        path = new _Path();
        inputDevice = GameObject.Find("ScriptObject").GetComponent<InputDeviceContext>();
        metadataInput = GameObject.Find("ScriptObject").GetComponent<MetadataInputContext>();
        spawner = new Spawner();
    }
    // Start is called before the first frame update
    void Start()
    {
        resizePlane();
        spawner.SpawnPlayAreaBoundaryColliders();
    }

    /// <summary>
    /// Generate and render path.
    /// </summary>
    void Update()
    {
        for(int i=0; i<metadataInput.VisiblePathSegmentCount(); i++)
        {
            path.GrowForward();
        }
        if (this.inputDevice.ButtonPressed())
        {
            /*
             * done to ensure that the environment adapts to changes in play area
             * dimensions during game play in Oculus Quest if player chooses to do so.
             */
            resizePlane();

            /*
             *  done because in Oculus Quest the gaurdian system don't
             *  provide correct playarea dimensions on Awake and Start.
             */
            spawner.UpdatePlayAreaBoundaries();
            path.GrowForward();
            
        }
    }

    /// <summary>
    /// To make sure that floor is present throught the play area
    /// </summary>
    private void resizePlane()
    {
        Vector3 planeScale = planeObject.transform.localScale;
        Vector3 size = planeObject.GetComponent<Renderer>().bounds.size;
        planeScale.z = inputDevice.PlayAreaDimensions().z * planeScale.z / size.z;
        planeScale.x = inputDevice.PlayAreaDimensions().x * planeScale.x / size.x;
        planeObject.transform.localScale = planeScale;
    }
}
