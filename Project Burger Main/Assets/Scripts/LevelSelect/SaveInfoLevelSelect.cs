using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveInfoLevelSelect {

    private const int LevelsOnTheMaps = 20;

    /// <summary>
    /// Saving The Game Data.
    /// </summary>
    /// <param name="info"></param>
    public static void Saver(SaveFileLevelSelect info) {
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
    public static SaveFileLevelSelect LoadSaveFile() {

        string path = Application.persistentDataPath + "/TheSaveFile.owo";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveFileLevelSelect data = formatter.Deserialize(stream) as SaveFileLevelSelect;

            stream.Close();

            Debug.Log("Successfully Loaded From File " + path);
            return data;
        } else {
            Debug.LogError("No File To Be Found At " + path);
            return null;
        }

    }

    public static void NewSaveFile(ref SaveFileLevelSelect theData) {
        theData = new SaveFileLevelSelect();
        theData.PlayerInfo = new PlayerLevelSelectInfo(LevelSelectManager.Instance.AreaMapStartSection);
        theData.LevelInfo = new LevelInfos(LevelsOnTheMaps);
  
        Saver(theData);
    }


}