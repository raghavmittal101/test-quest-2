using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenUpdated : MonoBehaviour
{
    public GameObject player; // this gives us orientation 'alpha' and initial position `P` of the player
    public float l; //segment length
    public int m; //number of visible path segments
    public GameObject line_prefab;
    public float alpha; // should taken as input from HMD

    private List<LineRenderer> L = new List<LineRenderer>();
    private List<float> beta = new List<float>();
    private List<Vector3> p = new List<Vector3>();
    private int i = 0;
    private int j = 0;
    private List<Vector3> visiblePoints = new List<Vector3>(); // 'p's which should be passed to mesh generator 

    public MeshGenerator mg;

    private Vector3 GetNextPoint(Vector3 p, float beta, float l)
    {
        p.x = l * Mathf.Sin(beta) + p.x;
        p.z = l * Mathf.Cos(beta) + p.z;
        return p;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos;
        pos = player.transform.position;
        pos.y = 1f;
        player.transform.position = pos;

        p.Add(pos); //p0
        beta.Add(Random.Range(alpha - (Mathf.PI / 2), alpha + (Mathf.PI / 2))); //beta[0]

        p.Add(GetNextPoint(p[0], beta[0], l)); //p[1]
        //L.Add(RenderLine(p[0], p[1])); //L[0]

        visiblePoints.Add(pos);

        for (int i = 1; i < m; i++)
        {
            GenerateLine(i); // beta[i] //p[i+1] //L[i]
        }

        mg.CreateMesh(visiblePoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            i += 1;
            Debug.Log("step: " + i);
            if (i >= m / 2 + 1)
            {
                //Destroy(L[j]);
                visiblePoints.RemoveAt(0);
                GenerateLine(m + j);
                j += 1; //beta[m+j] //p[m+j+1] //L[m+j]
                mg.CreateMesh(visiblePoints);
            }
        }

    }

    private void GenerateLine(int n)
    {
        Vector3 point;
        beta.Add(Random.Range(beta[n - 1] - (Mathf.PI / 2), beta[n - 1] + (Mathf.PI / 2)));
        point = GetNextPoint(p[n], beta[n], l);
        p.Add(point);
        visiblePoints.Add(point);
        // L.Add(RenderLine(p[n], p[n + 1]));
    }

    private LineRenderer RenderLine(Vector3 start_pos, Vector3 end_pos)
    {
        GameObject newLine = Instantiate(line_prefab);
        newLine.AddComponent(typeof(MeshFilter));
        newLine.AddComponent(typeof(MeshRenderer));

        LineRenderer lineRenderer = newLine.GetComponent<LineRenderer>();

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        lineRenderer.SetPosition(0, start_pos);
        lineRenderer.SetPosition(1, end_pos);

        return lineRenderer;
    }
}
