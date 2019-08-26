using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectBarrierNode : NodeBehaviour, IPointerClickHandler {


    private Node _myNode;

    public bool CanIWalk = false;


    private void Awake() {
        _myNode = GetComponent<Node>();
    }


    public void OnPointerClick(PointerEventData eventData) {
        LevelSelectManager.Instance.Player.Clicked(_myNode);
    }

    public override void SteppingOffEndNodeBehaviour() {
        //Empty

    }


    public override void TransitionNodeBehaviour() {
        if(CanIWalk == false) {
            LevelSelectManager.Instance.Player.ForceWalkBack();
        } else {
            LevelSelectManager.Instance.Player.ContinueToNextNode();
        }
    }


    public override void SteppingOnEndNodeBehaviour() {
        //Do A Check And If False, Move Player Back. Show Display That Informs The Player What He Need To Continue? Text Bubble? Currently Just A Boolean :D
        if (CanIWalk == false) {
            LevelSelectManager.Instance.Player.ForceWalkBack();//When Having A Display, Make The Display Do This Call.
        }
        //   LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(true);

    }

}
