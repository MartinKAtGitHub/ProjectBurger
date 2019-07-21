using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiUpdater : MonoBehaviour {

    [SerializeField] private Text gold = null;
    [SerializeField] private Text life = null;
    [SerializeField] private Text time = null;


    private void Start() {
        ScoreManager.OnGoldChange += GoldChanged;
        ScoreManager.OnLifeChange += LifeChanged;
        ScoreManager.OnTimeChange += TimeChanged;

        GoldChanged();
        LifeChanged();
        TimeChanged();

    }
    
    private void GoldChanged() {
        gold.text = "" + (LevelManager.Instance.ScoreManager.Gold - LevelManager.Instance.ScoreManager.GoldLost) + " / " + LevelManager.Instance.WinLooseManager.GoldWinAmount;
    }

    private void LifeChanged() {
        life.text = "";

        for (int i = 0; i < LevelManager.Instance.WinLooseManager.TotalLifes - LevelManager.Instance.ScoreManager.LifeLost; i++) {
            life.text += "<3 ";
        }

    }

    private void TimeChanged() {
        time.text = "" + (LevelManager.Instance.WinLooseManager.TimeLimit - LevelManager.Instance.ScoreManager.TimeUsed);
    }


}
