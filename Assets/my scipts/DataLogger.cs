using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    string SubjectId { get; }

    public DataLogger(string subjectId)
    {
        this.SubjectId = subjectId;
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

    /// <summary>
    /// Save <see cref="pointsList"/> to CSV file.
    /// </summary>
    private string SaveToCSV(string dataType, string content)
    {
        var folder = Application.persistentDataPath;
        var filePath = Path.Combine(folder, SubjectId + "_" + dataType + ".csv");
        File.WriteAllText(filePath, content);
        return filePath;
    }
}
