using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{

    private CustomerSelect _customerSelect;
    private QueueManager _queueManager;
    private Action _onSale;
    public Action OnSale { get => _onSale; set => _onSale = value; }

    private void Awake()
    {
        _customerSelect = GetComponent<CustomerSelect>();
        _queueManager = GetComponent<QueueManager>();
    }
    /// <summary>
    /// Triggers the onSale event;
    /// </summary>
    public void OnSell()
    {

        var customer = _customerSelect.CustomerInFocus;
        if (!_customerSelect.InSmoothTransition && customer != null)
        {
            LevelManager.Instance.FoodTrayDropArea.CheckFoodStacksAgainstOrder();

            _customerSelect.OnSell();
            _queueManager.RemoveCustomerFromQueue(customer);
        }
    }
}
