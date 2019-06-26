using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{

    private CustomerSelect _customerSelect;
    private Action _onSale;
    public Action OnSale { get => _onSale; set => _onSale = value; }

    private void Awake()
    {
        _customerSelect = GetComponent<CustomerSelect>();
    }
    /// <summary>
    /// Triggers the onSale event;
    /// </summary>
    public void OnSell()
    {
        //  OnSale?.Invoke();
        if (!_customerSelect.InSmoothTransition)
        {
            LevelManager.Instance.FoodTrayDropArea.CheckFoodStacksAgainstOrder();
            LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(LevelManager.Instance.CustomerSelect.CustomerInFocus);
           // LevelManager.Instance.CustomerSelect.ReFocusCustomerOnSell();
        }

    }
}
