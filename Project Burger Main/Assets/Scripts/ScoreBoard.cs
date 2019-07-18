using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {


    [SerializeField] private Text _completeOrders = null;
    [SerializeField] private Text _failedOrders = null;
    [SerializeField] private Text _ignoredCustomers = null;
    [SerializeField] private Text _biggestCombo = null;
    [SerializeField] private Text _timeRemaining = null;

    //   private Text _combosAquired;
    //   private Text _objectsSold;
    //0-30 == F, 30-50 == E, 50-70 == D, 70-85 == C, 85-95 == B, 95-100 == A. ??




    public void OnEnable() {

        _completeOrders.text = "" + LevelManager.Instance.ScoreManager.OrdersCompleted;
        _failedOrders.text = "" + LevelManager.Instance.ScoreManager.OrdersFailed;
        _ignoredCustomers.text = "" + LevelManager.Instance.ScoreManager.CustomersMade;
        _biggestCombo.text = "" + LevelManager.Instance.ScoreManager.ComboHighest;
        _timeRemaining.text = "" + (LevelManager.Instance.WinLooseManager.TimeLimit - (int)LevelManager.Instance.ScoreManager.TimeUsed);
    
        //TODO Rewards?


    }


}
