using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All the angles are assumed to be in radians.
 * Units are Unity units 1 Unity unit ~ 1 meter.
 * 
 * Awake function excecution order 
 *      MetadataInputContext, InputDeviceContext
 *      Spawner
 *      _Scene
 *  Can be changed from edit -> project settings -> script excecution order
 */
public class _Scene : MonoBehaviour
{
    /// <summary><see cref="MetadataInputContext"/></summary>
    private MetadataInputContext metadataInput { get { return _ResourceLoader.metadataInput; } }

    /// <summary><see cref="InputDeviceContext"/></summary>
    private InputDeviceContext inputDevice { get { return _ResourceLoader.inputDevice; } }

    _Path path;
    Spawner spawner;

    public GameObject planeObject;

    private void Awake()
    {
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner = new Spawner();
        path = new _Path();
        resizePlane();
        spawner.SpawnPlayAreaBoundaryColliders();

        for (int i=0; i<metadataInput.VisiblePathSegmentCount(); i++)
            path.GrowForward();
    }

    /// <summary>
    /// Generate and render path.
    /// </summary>
    void Update()
    {
        if (inputDevice.ButtonPressed()) // TBD: to be replaced with path collider hit
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
        for(int i=0; i<path.PresentPathSegmentsList.Count; i++)
        {
            Debug.DrawLine(path.PresentPathSegmentsList[i].StartPoint, path.PresentPathSegmentsList[i].EndPoint, Color.blue);
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
