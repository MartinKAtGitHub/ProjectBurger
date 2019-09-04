using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaChange : NodeBehaviour {

    public AreaMapPositions Area0;//0 == right or up
    public AreaMapPositions Area1;//1 == left or down

    public override void SteppingOffEndNodeBehaviour() {}


    public override void TransitionNodeBehaviour() {
        if (LevelSelectManager.Instance.Player.InvisibleNodeContinueWalk() == true) {
            LevelSelectManager.Instance.CameraFollow.UpdateAreaOffset(Area0.points);
        } else {
            LevelSelectManager.Instance.CameraFollow.UpdateAreaOffset(Area1.points);
        }
    
    }


    public override void SteppingOnEndNodeBehaviour() {}


}
