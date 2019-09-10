using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeAreaNode : NodeBehaviour, IPointerClickHandler {


    public Node NextAreaNode;

    public override void SteppingOffEndNodeBehaviour() {
        //Empty

    }


    public override void TransitionNodeBehaviour() {



        //if (Unlocked == true) {
        //    LevelSelectManager.Instance.Player.ContinueToNextNode();
        //} else {
        //    LevelSelectManager.Instance.Player.StopAndIgnoreClick();
        //    LevelSelectManager.Instance.BarrierInfo.Activate(this);
        //}
    }


    public override void SteppingOnEndNodeBehaviour() {
        LevelSelectManager.Instance.Player.StopOnNode();
        LevelSelectManager.Instance.Player.Clicked(NextAreaNode);

        //if (Unlocked == true) {
        //    LevelSelectManager.Instance.Player.StopOnNode();    
        //} else {
        //    LevelSelectManager.Instance.Player.StopAndIgnoreClick();
        //    LevelSelectManager.Instance.BarrierInfo.Activate(this);
        //}
    }

    public void OnPointerClick(PointerEventData eventData) {
        LevelSelectManager.Instance.Player.Clicked(gameObject.GetComponent<Node>());

    }
}
