using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handel's the selection of customers in queue.
/// the selected customer will be the one the food is checked against
/// and any other operation needed to be done to a singular customer 
/// </summary>
public class CustomerSelect : MonoBehaviour // TODO CustomerSelect.cs | Update the script to take Customer object instead of directly ref OrderGenerator
{
    [SerializeField] private int _customerSelectIndex = 0;
    public QueueManager QueueManager;

    private FoodTrayDropArea _foodTrayDropArea;

    [SerializeField] private TextMeshProUGUI _customerFocusName;

    public Customerss CustomerInFocus { get; private set; }
    public int CustomerSelectIndex { get => _customerSelectIndex;}

    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();
       
    }
   
    public void Initialize()
    {
        //LevelManager.Instance.CustomerSelect = this;
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;
        LevelManager.Instance.SalesManager.OnSale += ResetCustomerSelect;
    }

    public void NextCustomer()
    {
        if (_customerSelectIndex < QueueManager.ActiveQueueLimit.Count - 1)
        {
            _customerSelectIndex++;

            //FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
            //    .GetComponent<OrderGenerator>().Order;

            //_customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
            //    .GetComponent<OrderGenerator>().Order.CustomerName;
        }
        else
        {
            Debug.Log("NO NEXT CUSTOMER");
        }
    }

    public void PrevCustomer()
    {
        if (_customerSelectIndex > 0)
        {
            _customerSelectIndex--;
            Debug.Log("CUSTOMER = " + _customerSelectIndex);

           // FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
           //     .GetComponent<OrderGenerator>().Order;

           // _customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
           //.GetComponent<OrderGenerator>().Order.CustomerName;
        }
        else
        {
            Debug.Log("NO BACK CUSTOMER");
        }
    }

    /// <summary>
    /// When the game starts, the first person to come to the queue will be auto selected for the player.
    /// This method attaches the first customer to the selector
    /// </summary>
    public void SetInitialCustomer()
    {
        _customerSelectIndex = 0;
        SetCustomerFocus(_customerSelectIndex);
    }

    private void ResetCustomerSelect()
    {
        if (QueueManager.ActiveQueueLimit.Count == 0)
        {
            _customerFocusName.text = "No customer";
        }
        else
        {
            _customerSelectIndex = QueueManager.ActiveQueueLimit.Count - 1;
            //_customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
            //   .GetComponent<OrderGenerator>().Order.CustomerName;
        }



        Debug.Log("ResetCustomerSelect() need to set a new Selected customer on Sale");
    }


    public void CircularNextItem()
    {
        _customerSelectIndex++;
        _customerSelectIndex %= QueueManager.ActiveQueueLimit.Count; // clip index (turns to 0 if index == items.Count)

        SetCustomerFocus(_customerSelectIndex);
    }

    public void CircularPreviousItem()
    {
        _customerSelectIndex--; // decrement index

        if (_customerSelectIndex < 0)
        {
            _customerSelectIndex = QueueManager.ActiveQueueLimit.Count - 1;
        }

        SetCustomerFocus(_customerSelectIndex);
    }


    private void SetCustomerFocus(int index)
    {
        if(QueueManager.ActiveQueueLimit.Count > 0)
        {
            CustomerInFocus = QueueManager.ActiveQueueLimit[index];
            ChangeFoodTrayOrder();
        }
        else
        {
            Debug.LogError("CustomerSelect.cs |  SetCustomerFocus () =  ActiveCustomerQueue is Empty");
        }
    }


    private void ChangeFoodTrayOrder() // This runs before Start because of Levelmanager ScriptOrder priority
    {
        _foodTrayDropArea.Order = CustomerInFocus.Order;
    }
}
