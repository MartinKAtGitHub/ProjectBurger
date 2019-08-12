using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLooseManager : MonoBehaviour
{

    public float GoldWinAmount = 1000;
    public int TimeLimit = 1000;
    public int TotalLifes = 3;
    public GameObject ScorePanel;

    int _secondCount = 0;
    public int TimeUsed { get => (TimeLimit - _secondCount); }

    private void Awake()
    {
        LevelManager.Instance.WinLooseManager = this;
    }
    private void Start()
    {
        ScoreManager.OnGoldChange += GoldWinCheck;
        ScoreManager.OnLifeChange += LifeCheck;
        ScoreManager.OnTimeChange += TimeCheck;
        LevelManager.Instance.ScoreManager.CombosApplied = 1;
    }



    private void Update()
    {

        if (Time.time >= _secondCount + 1)
        {
            _secondCount++;
            LevelManager.Instance.ScoreManager.TimeUsed = 1;
        }

    }

    void TimeCheck()
    {
        if (Time.time >= TimeLimit)
        {
            Time.timeScale = 0f;
            GameEnd();
        }
    }

    void LifeCheck()
    {
        if (TotalLifes - LevelManager.Instance.ScoreManager.LifeLost <= 0)
        {
            GameEnd();
        }
    }


    void GoldWinCheck()
    {//Gold Check Each Time Gold Changes
        if (LevelManager.Instance.ScoreManager.Gold - LevelManager.Instance.ScoreManager.GoldLost >= GoldWinAmount)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        //Stop Everything In Background And Fade In ScoreBoard
        ScorePanel.SetActive(true);
        enabled = false;
    }

}
