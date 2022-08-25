using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    public static string directory = "/data/";
    public static string fileName = "data.txt";

    public static void Save(Data data)
    {
        string dir = Application.persistentDataPath + directory;
        Debug.Log(dir);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + fileName, json);
    }

    public static Data Load()
    {
        string filePath = Application.persistentDataPath + directory + fileName;
        Data data = new Data();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Safe file does not exist");
            return null;
        }
    }
}
