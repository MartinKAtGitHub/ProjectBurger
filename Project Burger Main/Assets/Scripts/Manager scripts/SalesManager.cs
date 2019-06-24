using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{

    private Action _onSale;

    public Action OnSale { get => _onSale; set => _onSale = value; }

    public void Initialize()
    {
        LevelManager.Instance.SalesManager = this;
    }

    /// <summary>
    /// Triggers the onSale event;
    /// </summary>
    public void OnSell()
    {
        // Call this on a button
        // event.Fire();

        //  OnSale?.Invoke();

        LevelManager.Instance.FoodTrayDropArea.CheckFoodStacksAgainstOrder();
        LevelManager.Instance.QueueManager.RemoveCustomerFromQueue();
        LevelManager.Instance.CustomerSelect.ReFocusCustomerOnSell();
    }
}
