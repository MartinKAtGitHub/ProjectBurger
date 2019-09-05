using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerLevelSelectInfo {

    public int PlayerPreviousNode = 0;
    public int PlayerCurrentNode = 0;

    public float CameraOffsecLeftX = 0;
    public float CameraOffsecRightX = 0;
    public float CameraOffsecDownY = 0;
    public float CameraOffsecUpY = 0;

    /// <summary>
    /// Should Only Be Called When You Want To Reset The Game. Setting The Info Back To The Very Beginning, Section 1, Node 0 (if this is start positions).
    /// </summary>
    /// <param name="startPoints"></param>
    public PlayerLevelSelectInfo(AreaMapPositions startPoints) {
        PlayerPreviousNode = startPoints.StartNode.NodeIndexInArray;
        PlayerCurrentNode = startPoints.StartNode.NodeIndexInArray;

        CameraOffsecLeftX = startPoints.MapPoints.AreaOffsetX.x;
        CameraOffsecRightX = startPoints.MapPoints.AreaOffsetX.y;
        CameraOffsecDownY = startPoints.MapPoints.AreaOffsetY.x;
        CameraOffsecUpY = startPoints.MapPoints.AreaOffsetY.y;

    }

    public void SetCameraOffset(float a, float b, float c, float d) {
        CameraOffsecLeftX = a;
        CameraOffsecRightX = b;
        CameraOffsecDownY = c;
        CameraOffsecUpY = d;
    }

    public void SetPlayerNodeIndexes(int previous, int current) {
        PlayerPreviousNode = previous;
        PlayerCurrentNode = current;
    }


}

[System.Serializable]
public class LevelInfos {

    [SerializeField]
    public TheLevels[] Levels;

    /// <summary>
    /// Copy Data From Old/New LevelInfo Over To This
    /// </summary>
    /// <param name="test"></param>
    public LevelInfos(LevelInfos test) {
        Levels = test.Levels;
    }


    /// <summary>
    /// This Is If Its A New Game, Where The File Needs To Be Generated. Parameter Is The Amount Of Levels That There Will Be In The Game
    /// </summary>
    /// <param name="arrayLength"></param>
    public LevelInfos(int arrayLength) {
        Levels = new TheLevels[arrayLength];

        for (int i = 0; i < arrayLength; i++) {
            Levels[i] = new TheLevels();
            Levels[i].TimesPlayer = 0;
            Levels[i].GoldEarned = 0;
            Levels[i].StarsGained = 0;
        }

    }

}

[System.Serializable]
public class TheLevels {
    public int TimesPlayer = 0;
    public int GoldEarned = 0;
    public int StarsGained = 0;
}