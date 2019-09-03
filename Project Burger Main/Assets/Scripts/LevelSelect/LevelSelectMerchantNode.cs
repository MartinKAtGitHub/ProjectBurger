﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectMerchantNode : NodeBehaviour, IPointerClickHandler {

    private Node _myNode;
    public GameObject[] ItemsToSell;




    private void Awake() {
        _myNode = GetComponent<Node>();
    }


    public void OnPointerClick(PointerEventData eventData) {
        LevelSelectManager.Instance.Player.Clicked(_myNode);
    }

    public override void SteppingOffEndNodeBehaviour() {
        LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(false);
    }


    public override void TransitionNodeBehaviour() {
        LevelSelectManager.Instance.Player.StopOnNode();
    }


    public override void SteppingOnEndNodeBehaviour() {
      //  LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(true);
        LevelSelectManager.Instance.Player.StopOnNode();
        LevelSelectManager.Instance.Player.IgnoreClick = true;
        LevelSelectManager.Instance.MerchantInfo.Activate(this);

    }

}
