using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaChange : NodeBehaviour {

    public AreaMapPositions Area0;//0 == right or up
    public AreaMapPositions Area1;//1 == left or down

    public override void SteppingOffEndNodeBehaviour() {}


    public override void TransitionNodeBehaviour() {
        if (LevelSelectManager.Instance.Player.InvisibleNodeContinueWalk() == true) {
            GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.SetCameraOffset(Area0.MapPoints);
        } else {
            GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.SetCameraOffset(Area1.MapPoints);
        }

        LevelSelectManager.Instance.CameraFollow.UpdateAreaOffset();

    }


    public override void SteppingOnEndNodeBehaviour() {}


}
