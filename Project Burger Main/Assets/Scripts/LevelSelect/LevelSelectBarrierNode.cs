using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectBarrierNode : NodeBehaviour, IPointerClickHandler {


    private Node _myNode;
    private bool Unlocked = false;

    [SerializeField]
    private string _levelInfo = null;
    [SerializeField]
    [TextArea(3, 10)]
    private string _levelText = null;
    [SerializeField]
    private int _goldPrice = 0;
    [SerializeField]
    private int _gemPrice = 0;

    public string LevelInfo { get => _levelInfo; }
    public string LevelText { get => _levelText; }
    public int GoldPrice { get => _goldPrice; }
    public int GemPrice { get => _gemPrice; }


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
        if (Unlocked == true) {
            LevelSelectManager.Instance.Player.ContinueToNextNode();
        } else {
            LevelSelectManager.Instance.Player.StopAndIgnoreClick();
            LevelSelectManager.Instance.BarrierInfo.Activate(this);
        }
    }


    public override void SteppingOnEndNodeBehaviour() {

        if(Unlocked == true) {
            LevelSelectManager.Instance.Player.StopOnNode();
        } else {
            LevelSelectManager.Instance.Player.StopAndIgnoreClick();
            LevelSelectManager.Instance.BarrierInfo.Activate(this);
        }
    }

    public void UnlockedBarrier() {
        Unlocked = true;
    }


    public override bool NodeWalkable() {
        if (Unlocked == true) {
            return true;
        } else {
            return false;
        }
    }

}
