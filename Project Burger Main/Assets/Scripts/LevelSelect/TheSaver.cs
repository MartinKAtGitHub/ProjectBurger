using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class TheSaver {


    /// <summary>
    /// Saving The Game Data.
    /// </summary>
    /// <param name="info"></param>
    public static void Saver(SaveFile info) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/TheSaveFile.owo", FileMode.Create);

        formatter.Serialize(stream, info);
        stream.Close();

        Debug.Log("Successfully Saved To File " + (Application.persistentDataPath + "/TheSaveFile.owo"));
    }

    /// <summary>
    /// Loading The Game Data, That Was Saved Before.
    /// </summary>
    /// <returns></returns>
    public static SaveFile LoadSaveFile() {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/TheSaveFile.owo", FileMode.Open);
        SaveFile data = formatter.Deserialize(stream) as SaveFile;

        stream.Close();

        Debug.Log("Successfully Loaded From File " + (Application.persistentDataPath + "/TheSaveFile.owo"));
        return data;

    }

    public static bool DoesSaveFileExist() {

        if (File.Exists(Application.persistentDataPath + "/TheSaveFile.owo")) {
            return true;
        } else {
            return false;
        }

    }


    public static void NewSaveFile(ref SaveFile theData) {
        theData = new SaveFile();
        Saver(theData);
    }


}