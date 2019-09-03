using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierInfoBoard : MonoBehaviour {

    [SerializeField]
    private Text Level = null;
    [SerializeField]
    private Text LevelText = null;

    private LevelSelectBarrierNode _barrier;

    public void Activate(LevelSelectBarrierNode barrier) {
        _barrier = barrier;

        Level.text = _barrier.LevelInfo;
        LevelText.text = _barrier.LevelText;

        gameObject.SetActive(true);
    }

    public void ExitBoard() {
        gameObject.SetActive(false);
        LevelSelectManager.Instance.Player.IgnoreClick = false;
        LevelSelectManager.Instance.Player.SendPlayerToPreviousPosition();

    }

    public void PurchaseWithGold() {
        if (LevelSelectManager.Instance.PlayerGoldAquired >= _barrier.GoldPrice) {
            LevelSelectManager.Instance.PlayerGoldAquired -= _barrier.GoldPrice;
            _barrier.UnlockedBarrier();
            gameObject.SetActive(false);
            LevelSelectManager.Instance.Player.CanMove();
        } else {
            //Play Wrong Sound Or Something
            ExitBoard();
        }
    }

    public void PurchaseWithGems() {
        if (LevelSelectManager.Instance.PlayerGemsAquired >= _barrier.GemPrice) {
            LevelSelectManager.Instance.PlayerGemsAquired -= _barrier.GemPrice;
            _barrier.UnlockedBarrier();
            gameObject.SetActive(false);
            LevelSelectManager.Instance.Player.CanMove();
        } else {
            //Play Wrong Sound Or Something
            ExitBoard();
        }
    }


}
