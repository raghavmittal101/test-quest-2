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
    private string subjectId = "-1";
    public TMP_InputField subjectIDInputField;
    private GameObject player;
    public Dictionary<double, Vector3> playerPositionLog;
    public Dictionary<double, float> playerVelocityLog;
    private System.DateTime startTime;
    public static bool isSubjectIDRecieved;
    private bool loggerFlag = true;

    public void OnSubjectIdSubmit()
    {
        Debug.Log("At subject submission");
        // get data from input field and assign it to subject ID.
 //       Debug.Log(subjectIDInputField.GetComponent<Text>().text);
        this.subjectId = subjectIDInputField.text;
        isSubjectIDRecieved = true;
    }

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
        return SaveToCSV("pointsList", str);
        
    }

    public string LogPlayerPosition()
    {
        return SaveToCSV("playerPositions", playerPositionLog);
    }

    public string LogPlayerVelocity()
    {
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
    private string SaveToCSV(string dataType, Dictionary<double, Vector3> content)
    {
        var folder = Application.persistentDataPath;
        var filePath = Path.Combine(folder, subjectId + "_" + dataType + ".csv");


        using (StreamWriter file = new StreamWriter(filePath))
            foreach (var entry in content)
                file.WriteLine("[{0}, {1}]\n", entry.Key, entry.Value);

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
        playerPositionLog = new Dictionary<double, Vector3>();
        playerVelocityLog = new Dictionary<double, float>();
        player = GameObject.FindGameObjectWithTag("playerObj");

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
        playerPositionLog.Add(timeSpent, playerPos);
        // Vector3 velocity = OVRManager.tracker.GetPose().
        // float speed = Mathf.Pow(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2), 0.5f);
        // float speed = velocity.magnitude;
        // playerVelocityLog.Add(timeSpent, speed);
        loggerFlag = true;
        Debug.Log("playerPos: " + playerPos.ToString());
    }

}
