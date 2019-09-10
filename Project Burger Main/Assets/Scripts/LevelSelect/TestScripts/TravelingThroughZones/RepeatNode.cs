using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : NodeBehaviour {

    public BackGroundFollower Backgorund;


    public int Repeats = 5;
    public int CurrentRepeats = 0;
    Vector3 offset = Vector3.zero;

    public override void SteppingOffEndNodeBehaviour() {
        //Empty

    }


    public override void TransitionNodeBehaviour() {
        
      /*  if(CurrentRepeats < Repeats) {
            offset = LevelSelectManager.Instance.CameraFollow.transform.position - LevelSelectManager.Instance.Player.transform.position;
            LevelSelectManager.Instance.Player.TeleportPlayerToPreviousPosition();
            LevelSelectManager.Instance.CameraFollow.TeleportToPlayerwithOffset(offset);
            CurrentRepeats++;
       
        } else {*/
            LevelSelectManager.Instance.Player.ContinueToNextNode();
        LevelSelectManager.Instance.Player.Speed = 200;

          //  Backgorund.RemoveMovingObject();
     //   }


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
