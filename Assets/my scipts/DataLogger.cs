using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DataLogger : MonoBehaviour
{
    // string SubjectId { get; }
    // private enum subjectIDInputMethod { EnterInEditor, EnterInScene};
    // [SerializeField] private subjectIDInputMethod _subjectIDInputMethod;
    private string subjectId;
    
    private GameObject player;
    //public Dictionary<double, Vector3> playerPositionLog;
    public Dictionary<double, float> playerVelocityLog;
    public Dictionary<double, string> playerPositionLog;
    private System.DateTime startTime;
    private bool loggerFlag = true;

    /// <summary>
    /// Save current <see cref="DetectBoundaryTestScript.pointsList"/> to CSV file.
    /// Resultant filename will be subjectId_pointsList.csv .
    /// </summary>
    /// <param name="pointsList"></param>
    /// <returns>full path of the resultant file.</returns>
    public string LogPointsList(List<Vector3> pointsList)
    {
        var str = "x, y, z";
        foreach (Vector3 point in pointsList)
        {
            str = str + "\n[" + point.x + "," + point.y + "," + point.z + "],";
        }
        Debug.Log("Saving PointsList");
        return SaveToCSV("pointsList", str);
        
    }

    public string LogPlayerPosition()
    {
        Debug.Log("Saving PlayerPosition");
        return SaveToCSV("playerPositions", playerPositionLog);
    }

    public string LogLeftWallPositions(List<Vector3> leftWallsList)
    {
        Debug.Log("Saving Left Wall positions");
        var str = "x, y, z";
        foreach (Vector3 point in leftWallsList)
        {
            str = str + "\n[" + point.x + "," + point.y + "," + point.z + "],";
        }
        return SaveToCSV("leftWallPositions", str);
    }

    public string LogRightWallPositions(List<Vector3> rightWallsList)
    {
        Debug.Log("Saving Right Wall positions");
        var str = "x, y, z";
        foreach (Vector3 point in rightWallsList)
        {
            str = str + "\n[" + point.x + "," + point.y + "," + point.z + "],";
        }
        return SaveToCSV("rightWallPositions", str);
    }

    public string LogPlayerVelocity()
    {
        Debug.Log("Saving Velocity");
        return SaveToCSV("velocity", playerVelocityLog);
    }

    /// <summary>
    /// Save data to CSV file named "SubjectID_dataType.csv".
    /// </summary>
    private string SaveToCSV(string dataType, string content)
    {
        var folder = Application.persistentDataPath;
        var filePath = Path.Combine(folder, subjectId + "_" + dataType + ".csv");
        File.WriteAllText(filePath, content);
        return filePath;
    }

    /// <summary>
    /// Save data to CSV file named "SubjectID_dataType.csv".
    /// </summary>
    private string SaveToCSV(string dataType, Dictionary<double, string> content)
    {
        var folder = Application.persistentDataPath;
        var filePath = Path.Combine(folder, subjectId + "_" + dataType + ".csv");


        using (StreamWriter file = new StreamWriter(filePath))
            foreach (var entry in content)
                file.WriteLine("{0}, {1}", entry.Key, entry.Value);

        return filePath;
    }

    /// <summary>
    /// Save data to CSV file named "SubjectID_dataType.csv".
    /// </summary>
    private string SaveToCSV(string dataType, Dictionary<double, float> content)
    {
        var folder = Application.persistentDataPath;
        var filePath = Path.Combine(folder, subjectId + "_" + dataType + ".csv");


        using (StreamWriter file = new StreamWriter(filePath))
            foreach (var entry in content)
                file.WriteLine("[{0}, {1}]\n", entry.Key, entry.Value);

        return filePath;
    }

    public void Start()
    {
        startTime = System.DateTime.Now;
        playerPositionLog = new Dictionary<double, string>();
        playerVelocityLog = new Dictionary<double, float>();
        player = GameObject.FindGameObjectWithTag("playerObj");
        subjectId = entryScene.subjectID;

    }
    public void Update()
    {
        if (loggerFlag)
        {
            StartCoroutine(logData());
        }
    }

    private IEnumerator logData() {
        loggerFlag = false;
        yield return new WaitForSeconds(1);
        double timeSpent = System.DateTime.Now.Subtract(startTime).TotalSeconds;
        Vector3 playerPos = GameObject.FindObjectOfType<OVRCameraRig>().leftEyeAnchor.position;
        string playerPosString = "[" + playerPos.x +","+ playerPos.z + "]";  // for getting more decimal places for precision you need to write x, y, z separately.
        playerPositionLog.Add(timeSpent, playerPosString);
     //   Vector3 velocity = ;
     //   float speed = Mathf.Pow(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2), 0.5f);
     //   float speed = velocity.magnitude;
        // playerVelocityLog.Add(timeSpent, speed);
        loggerFlag = true;
        Debug.Log("playerPos: " + playerPos.ToString());
        Debug.Log(entryScene.subjectID);
    }

}
