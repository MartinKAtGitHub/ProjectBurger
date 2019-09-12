using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchBackgroundNode : NodeBehaviour {

    public BackGroundFollower Background;

    public override void SteppingOffEndNodeBehaviour() {
        //Empty

    }


    public override void TransitionNodeBehaviour() {
        LevelSelectManager.Instance.Player.ContinueToNextNode();
   //     Background.SetMovingObject(LevelSelectManager.Instance.Player.transform);
   if(LevelSelectManager.Instance.Player.Speed > 50)
        LevelSelectManager.Instance.Player.Speed = 50;
   else
        LevelSelectManager.Instance.Player.Speed = 200;
        //  Background.transform.parent = LevelSelectManager.Instance.Player.transform;



        //if (Unlocked == true) {
        //    LevelSelectManager.Instance.Player.ContinueToNextNode();
        //} else {
        //    LevelSelectManager.Instance.Player.StopAndIgnoreClick();
        //    LevelSelectManager.Instance.BarrierInfo.Activate(this);
        //}
    }


    public override void SteppingOnEndNodeBehaviour() {

        //if (Unlocked == true) {
        //    LevelSelectManager.Instance.Player.StopOnNode();
        //} else {
        //    LevelSelectManager.Instance.Player.StopAndIgnoreClick();
        //    LevelSelectManager.Instance.BarrierInfo.Activate(this);
        //}
    }

}
