using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides functionality for intantiating GameObjects in scene.
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary><see cref="InputDeviceContext"/></summary>
    private InputDeviceContext inputDevice { get { return _ResourceLoader.inputDevice; } }
    private MetadataInputContext metadataInput { get { return _ResourceLoader.metadataInput; } }
    private GameObject[] boundaryColliderArr;
    /// <summary>
    /// Must be stored at "Assets/Resources/prefabs/"
    /// </summary>
    private GameObject boundaryColliderPrefab { get { return _ResourceLoader.spawner_boundaryColliderPrefab; } }
    private GameObject wallPrefab { get { return _ResourceLoader.spawner_wallPrefab; } }
    private GameObject photoFramePrefab { get { return _ResourceLoader.spawner_photoFramePrefab; } }
    private GameObject triggerColliderPrefab { get { return _ResourceLoader.spawner_triggerColliderPrefab; } }
    public List<GameObject>[] leftRightWallObjectsList;

    List<GameObject[]> pathSegmentWallsList;

    private void Start()
    {
    }
    public Spawner() {
        boundaryColliderArr = new GameObject[4];
    }

    /// <summary>
    /// Instantiate walls on both sides of the given path segment left right points. And add them to spawnned walls list <see cref="leftRightWallObjectsList"/>.
    /// </summary>
    /// <param name="leftRightPoints"></param>
    /// <returns></returns>
    public void SpawnWalls(ref List<Vector3>[] leftRightPoints)
    {
        var points = leftRightPoints;
        for (int i = 0; i < 2; i++)
        {
            var pointsCount = points[i].Count;
            Vector3 wallPos = (points[i][pointsCount - 2] + points[i][pointsCount - 3]) / 2;
            wallPos.y = 0.5f;

            GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity);
            Vector3 wallScale = wall.transform.localScale;
            wallScale.x = (points[i][pointsCount - 2] - points[i][pointsCount - 3]).magnitude;
            wallScale.y = 1f;
            wallScale.z = 0.01f;
            wall.transform.localScale = wallScale;

            float wallRotAngleAlongY = Vector3.SignedAngle(new Vector3(1f, 0f, 0f), (points[i][pointsCount - 2] - points[i][pointsCount - 3]).normalized, Vector3.up);
            Vector3 rotation = wall.transform.localEulerAngles;
            rotation = new Vector3(0f, wallRotAngleAlongY, 0f);
            wall.transform.localEulerAngles = rotation;

            leftRightWallObjectsList[i].Add(wall);
        }
    }

    public void RemoveFirstLeftRightWalls()
    {
        for(int i=0; i<2; i++)
        {
            Destroy(leftRightWallObjectsList[i][0]);
            leftRightWallObjectsList[i].RemoveAt(0);
        }
    }

    // TBD
    /// <summary>
    /// Generates points on left and right side of the path segment for positioning of walls. Gets
    /// </summary>
    /// <returns>List of points </returns>
    private List<Vector3> GenerateLeftRightPointsForPathSegment()
    {
        return new List<Vector3>();
    }

    /// <summary>
    /// Removes the left and right wall GameObject of oldest  from scene
    /// </summary>
    private void RemoveOldestLeftRightWalls()
    {

    }

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
            go.transform.position = new Vector3(boundaryPositions[i].x, scale.y / 2, boundaryPositions[i].z);
            boundaryColliderArr[i] = go;
        }
    }
}
