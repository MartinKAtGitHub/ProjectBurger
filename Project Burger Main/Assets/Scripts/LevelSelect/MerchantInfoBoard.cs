using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInfoBoard : MonoBehaviour {

    public GameObject TheSelectedItem;

    public void Activate(LevelSelectMerchantNode merchant) {
        gameObject.SetActive(true);
    }


    public void ExitBoard() {
        gameObject.SetActive(false);
        LevelSelectManager.Instance.Player.IgnoreClick = false;
        //  LevelSelectManager.Instance.Player.SendPlayerToPreviousPosition();

    }

    public void BuyItemWitGold() {


        if (LevelSelectManager.Instance.PlayerGoldAquired >= TheSelectedItem.layer) {//TODO Item Prices And Amount And Stuff
            LevelSelectManager.Instance.PlayerGoldAquired -= TheSelectedItem.layer;
        } else {
            //Play Wrong Sound Or Something
            ExitBoard();
        }
    }

    public void BuyItemWitGems() {
        if (LevelSelectManager.Instance.PlayerGemsAquired >= TheSelectedItem.layer) {//TODO Item Prices And Amount And Stuff
            LevelSelectManager.Instance.PlayerGemsAquired -= TheSelectedItem.layer;
        } else {
            //Play Wrong Sound Or Something
            ExitBoard();
        }
    }

    public void SellItems() {
        if(TheSelectedItem != null) {
            LevelSelectManager.Instance.PlayerGemsAquired += TheSelectedItem.layer;
        } else {
            //Play Wrong Sound Or Something
            ExitBoard();
        }

    }



}
