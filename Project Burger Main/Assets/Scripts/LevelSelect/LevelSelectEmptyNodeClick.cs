using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectEmptyNodeClick : NodeBehaviour, IPointerClickHandler {

    private Node _myNode;

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
        LevelSelectManager.Instance.Player.StopOnNode();

    }


    public override void SteppingOnEndNodeBehaviour() {
        LevelSelectManager.Instance.Player.StopOnNode();

    }

}
