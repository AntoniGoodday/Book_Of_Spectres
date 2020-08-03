using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/SaveData/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "saveFile.txt", saveString);
    }

    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "saveFile.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "saveFile.txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
