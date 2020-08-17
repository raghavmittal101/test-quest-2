/*
**Infinite Path Generation in Finite Space Script**

Inputs: 
    theta <Float> : initial orientation of character in degrees (in future it will be recorded via OVRManager API)
    segment_len <Float>: length of path segment in units
    number_of_paths <Int>: number of paths to be visible in the environment at any given time (recommended : 4)
    character <GameObject>: reference to the game character. (in future character can be replaced with position inputs from OVRManager API)
    play_area_dimens <Vector3>: dimensions of play area in units (in future it will be recorded via OVR Gaurdian API)
        x <Float>: width of play area 
        z  <Float>: length of play area
        y <Float> : height of play area will be set to 0
Output:
    Initially instantiate "no_of_segments" line segments starting from character's position. 
    Spawn another line segment when character reaches end of "floor(no_of_segments/2)". (min path segments should be 2 for proper functioning)
    Remove first line segment and keep repeating this process until the program is terminated externally.

    line_segment <GameObject>: line segment represents a path section.

    point_position_log <List [(Float, Float)]> : list of all the points rendered during the gameplay for reproducing 
                                                 the same path if required

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderer : MonoBehaviour
{
    /* user inputs*/
    public float theta;
    public float segment_len;
    public int no_of_segments;
    public GameObject character;
    public Vector3 playArea_dimen;

    /*other inputs*/
    public GameObject pointObject;
    public GameObject lineObject;


    private List<Vector3> projected_points = new List<Vector3>();
    private List<float> projected_thetas = new List<float>();
    private List<GameObject> rendered_lines = new List<GameObject>();
    private List<GameObject> rendered_point_spheres = new List<GameObject>();

    void Start()
    {
        /*
         Start by rendering a single line in front of character

         Render a line between character's current position and projected pointt.
         */
        Vector3 starting_point;
        starting_point.x = character.transform.position.x;
        starting_point.z = character.transform.position.z;
        starting_point.y = 0f;
        projected_points.Add(starting_point);
        projected_thetas.Add(theta); // character's initial orientation
        
        ProjectPoint(); // a new line will be drawn between player's position and this point
        RenderLine();

        for (int i=0; i<no_of_segments-1; i++) // render line segments in advance
        {
            GenerateTheta();
            ProjectPoint();
            RenderLine();
            RenderPointObject();
        }
    }

    int stepCount = 0; // number of steps character has moved
    int stepCountGlobal = 0;
    void Update()
    {
        Debug.Log(projected_points.Count);
        if (Input.GetMouseButtonDown(0))
        {
            // if no_of_segments == 4, then when player crosses first 2 segments, 1 more segment is generated and first 1 is destroyed.
            if(stepCount >= Mathf.Floor(no_of_segments/2)) {
                GenerateTheta();
                ProjectPoint();
                RenderLine();
                RenderPointObject();
                deleteFirstLineObject();
                deleteFirstPointObject();
                stepCount = (int)Mathf.Floor(no_of_segments / 2)-1; // set to no_of_segments/2-1 because from now at every next step, first path segment will be deleted

            }
            MoveCharacterForward();
        }
    }

    private void MoveCharacterForward()
    /*
     * Move character one step forward i.e to the start of next line segment from character's present line segment.
     */
    {
        Vector3 newPos = projected_points[stepCountGlobal+1]; // +1 because we want next location for character to move.
        Vector3 charact_pos = character.transform.position;
        charact_pos.x = newPos.x;
        charact_pos.z = newPos.z;
        character.transform.position = charact_pos;
        stepCountGlobal+=1;
        stepCount += 1;
    }
    
    private Vector3 ProjectPoint()
    /*
     *  updates the projected_points list
        return : Vector3 : position of new point projected
     */
    {
        int len = projected_points.Count;

        Vector3 last_point = projected_points[len - 1];
        float new_theta = projected_thetas[len - 1];
        

        Vector3 new_point;
        new_point.x = Mathf.Sin(NormalizedRad(new_theta)) * segment_len + last_point.x;
        new_point.z = Mathf.Cos(NormalizedRad(new_theta)) * segment_len + last_point.z;
        new_point.y = 0f;
        
        projected_points.Add(new_point);

        return new_point;
    }

    
    private void RenderLine()
    {
        int count = projected_points.Count;
        Vector3 startPosition = projected_points[count - 2];
        Vector3 endPosition = projected_points[count - 1];
        GameObject newLine = Instantiate(lineObject);
        LineRenderer lineRenderer = newLine.GetComponent<LineRenderer>();

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        
        startPosition.y = 1f;
        endPosition.y = 1f;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        rendered_lines.Add(newLine);
    }

    private void deleteFirstLineObject()
    {
        Destroy(rendered_lines[0]);
        rendered_lines.RemoveAt(0);
    }

    private float GenerateTheta()
    {
        float _theta;
        float previous_theta = projected_thetas[projected_thetas.Count - 1];
        _theta = Random.Range(previous_theta - 90, previous_theta + 90);
        projected_thetas.Add(_theta);
        return _theta;
    }

    private float NormalizedRad(float theta)
    {
        float _theta;
        _theta = Mathf.Deg2Rad * (theta % 360);
        return _theta;
    }

    private int IsCollidingWithBoundary(Vector3 point)
    {
        return -1;
    }

    private void RenderPointObject()
     /*
      Instantiate a spherical object to mark the position of second last point
      which also marks the starting of last line segment
     */
    {
        int count = projected_points.Count;
        GameObject newPoint = Instantiate(pointObject);
        newPoint.transform.position = projected_points[count-2];
        rendered_point_spheres.Add(newPoint);
    }

    private void deleteFirstPointObject()
    {
        Destroy(rendered_point_spheres[0]);
        rendered_point_spheres.RemoveAt(0);
    }
}
