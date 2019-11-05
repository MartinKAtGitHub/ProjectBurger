using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{

    [SerializeField]private LimitedCustomerSelect _customerSelect;
    private QueueManager _queueManager;
    private Action _onSale;
    public Action OnSale { get => _onSale; set => _onSale = value; }

    private void Awake()
    {
        _customerSelect = GetComponent<LimitedCustomerSelect>();
        _queueManager = GetComponent<QueueManager>();
    }
    /// <summary>
    /// Triggers the onSale event;
    /// </summary>
    public void OnSell()
    {

        var customer = _customerSelect.QueueSlotInFocus.CurrentCustomer;
        if (customer != null)
        {
            if (!_customerSelect.InSmoothTransition)
            {
                LevelManager.Instance.FoodTray.CheckFoodStacksAgainstOrder();

               // _customerSelect.OnSell();
                _queueManager.RemoveCustomerFromQueue(customer);
            }
        }
        else
        {
            Debug.Log("No customer too sell to");
        }
    }
}
