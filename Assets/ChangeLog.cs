using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChangeLog : MonoBehaviour
{
    [SerializeField] private string[] changeLog;
    private int index = 1;
    private void OnApplicationQuit()
    {
        SaveArrayToFile();
    }
    private void SaveArrayToFile()
    {
        string filePath = Path.Combine(Application.dataPath, "ChangeLog.txt");
        string assetPath = "Assets/ChangeLog.txt";
        UnityEngine.Object existingAsset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(TextAsset));
        StreamWriter writer = File.CreateText(filePath);
        writer.WriteLine("Change Log for Vertical Slice project\nNote: Changes will be saved on playmode exit");
        for (int i = 0; i < changeLog.Length; i += 3)
        {
            writer.WriteLine($"\n\nChange {index}\nDate: {changeLog[i].ToString()}\nAuthor: {changeLog[i + 1]}\nChange Description: {changeLog[i + 2]}");
            index++;
        }
        writer.Close();
        Debug.Log("Array data saved to file: " + filePath);

        if (existingAsset != null)
        {
            // Reimport the existing asset to apply changes AssetDatabase. ImportAsset (assetPath);
            Debug.Log("Array data reimported: ChangeLog.txt");
            AssetDatabase.ImportAsset(assetPath);
        }
        else
        {
            Debug.Log("Array data saved as asset: ChangeLog.txt");
            AssetDatabase.ImportAsset(assetPath);
        }
    }
}
