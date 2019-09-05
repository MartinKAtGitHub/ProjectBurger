using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaChange : NodeBehaviour {

    public AreaMapPositions Area0;//0 == right or up
    public AreaMapPositions Area1;//1 == left or down

    public override void SteppingOffEndNodeBehaviour() {}


    public override void TransitionNodeBehaviour() {
        if (LevelSelectManager.Instance.Player.InvisibleNodeContinueWalk() == true) {
            GameInfoHolder.Instance.TheSaveFile.PlayerInfo.SetCameraOffset(Area0.MapPoints.AreaOffsetX.x, Area0.MapPoints.AreaOffsetX.y, Area0.MapPoints.AreaOffsetY.x, Area0.MapPoints.AreaOffsetY.y);
        } else {
            GameInfoHolder.Instance.TheSaveFile.PlayerInfo.SetCameraOffset(Area1.MapPoints.AreaOffsetX.x, Area1.MapPoints.AreaOffsetX.y, Area1.MapPoints.AreaOffsetY.x, Area1.MapPoints.AreaOffsetY.y);
        }

        LevelSelectManager.Instance.CameraFollow.UpdateAreaOffset();

    }


    public override void SteppingOnEndNodeBehaviour() {}


}
