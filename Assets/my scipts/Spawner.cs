using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary><see cref="InputDeviceContext"/></summary>
    private InputDeviceContext inputDevice;
    private GameObject[] boundaryColliderArr;
    /// <summary>
    /// Must be stored at "Assets/resources/prefabs/" as "boundaryColliderPrefab"
    /// </summary>
    private GameObject boundaryColliderPrefab;
    private GameObject wallPrefab;
    private GameObject photoFramePrefab;
    private GameObject triggerColliderPrefab;

    private void Awake()
    {
        boundaryColliderArr = new GameObject[4];
        boundaryColliderPrefab = (GameObject)Resources.Load("prefabs/boundaryColliderPrefab", typeof(GameObject));
        wallPrefab = (GameObject)Resources.Load("prefabs/wallPrefab", typeof(GameObject));
        photoFramePrefab = (GameObject)Resources.Load("prefabs/photoFramePrefab", typeof(GameObject));
        triggerColliderPrefab = (GameObject)Resources.Load("prefabs/triggerColliderPrefab", typeof(GameObject));


    }

    public void SpawnLeftRightWalls() { }

    public void SpawnImages() { }

    public void SpawnTriggerColliders() { }

    /*
     * Play area boundary collider management related methods
     */
    /// <summary>
    /// Remove exisiting boundaries and spawn them again.
    /// </summary>
    public void UpdatePlayAreaBoundaries()
    {
        RemovePlayAreaBoundaries();
        SpawnPlayAreaBoundaryColliders();
    }

    public void RemovePlayAreaBoundaries()
    {
        if (boundaryColliderArr.Length != 0)
        {
            foreach (GameObject obj in boundaryColliderArr)
            {
                Destroy(obj);
            }
        }
    }

    /// <summary>
    /// It is required for <see cref="BoundaryDetector"/> to function properly.
    /// </summary>
    public void SpawnPlayAreaBoundaryColliders()
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
            boundaryColliderArr[i] = go;
        }
    }
}
