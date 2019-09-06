using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelSelectData {

    private const int LevelsOnTheMaps = 20;

    public LevelSelectPlayerData PlayerData = new LevelSelectPlayerData();
    public LevelSelectLevelData LevelData = new LevelSelectLevelData();

    


    /// <summary>
    /// Should Only Be Called When You Want To Reset The Game. Setting The Info Back To The Very Beginning, Section 1, Node 0 (if this is start positions).
    /// </summary>
    /// <param name="startPoints"></param>
    public LevelSelectData(AreaMapPositions startPoints) {
        PlayerData.SetCameraOffset(startPoints.MapPoints);
        PlayerData.SetPlayerNodeIndexes(startPoints);

        LevelData.InitializeArray(LevelsOnTheMaps);
    }

}

[System.Serializable]
public class LevelSelectPlayerData {
    public int PlayerPreviousNode = 0;
    public int PlayerCurrentNode = 0;

    public float CameraOffsecLeftX = 0;
    public float CameraOffsecRightX = 0;
    public float CameraOffsecDownY = 0;
    public float CameraOffsecUpY = 0;

    public void SetCameraOffset(AreaPoints startPoints) {
        CameraOffsecLeftX = startPoints.AreaOffsetX.x;
        CameraOffsecRightX = startPoints.AreaOffsetX.y;
        CameraOffsecDownY = startPoints.AreaOffsetY.x;
        CameraOffsecUpY = startPoints.AreaOffsetY.y;
    }

    public void SetPlayerNodeIndexes(AreaMapPositions startPoints) {
        PlayerPreviousNode = startPoints.StartNode.NodeIndexInArray;
        PlayerCurrentNode = startPoints.StartNode.NodeIndexInArray;
    }

    public void SetPlayerNodeIndexes(Node previous, Node current) {
        PlayerPreviousNode = previous.NodeIndexInArray;
        PlayerCurrentNode = current.NodeIndexInArray;
    }

}




[System.Serializable]
public class LevelSelectLevelData {

    [SerializeField]
    public LevelData[] TheLevels;

    public void InitializeArray(int arrayLength) {
        TheLevels = new LevelData[arrayLength];

        for (int i = 0; i < arrayLength; i++) {
            TheLevels[i] = new LevelData();
            TheLevels[i].TimesPlayer = 0;
            TheLevels[i].GoldEarned = 0;
            TheLevels[i].StarsGained = 0;
        }
    }

}

[System.Serializable]
public class LevelData {
    public int TimesPlayer = 0;
    public int GoldEarned = 0;
    public int StarsGained = 0;
}