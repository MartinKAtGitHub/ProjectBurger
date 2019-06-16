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
    public int CustomerSelectIndex = 0;
    public QueueManager QueueManager;
    public FoodTrayDropArea FoodTrayDropArea;

    [SerializeField]private TextMeshProUGUI _customerFocusName;


    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();
    }
    private void Start()
    {
        LevelManager.Instance.CustomerSelect = this;
        LevelManager.Instance.SalesManager.OnSale += ResetCustomerSelect;
    }

    public void NextCustomer()
    {
        if(CustomerSelectIndex < QueueManager.ActiveCustomerQueue.Count - 1)
        {
            CustomerSelectIndex++;

            FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
                .GetComponent<OrderGenerator>().Order;

            _customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
                .GetComponent<OrderGenerator>().Order.CustomerName;
        }
        else
        {
            Debug.Log("NO NEXT CUSTOMER");
        }
    }

    public void PrevCustomer()
    {
        if(CustomerSelectIndex > 0)
        {
            CustomerSelectIndex--;
            Debug.Log("CUSTOMER = " + CustomerSelectIndex);

            FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
                .GetComponent<OrderGenerator>().Order;

            _customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
           .GetComponent<OrderGenerator>().Order.CustomerName;
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
    public void SelectInitialCustomer()
    {
        CustomerSelectIndex = 0;
        FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
                .GetComponent<OrderGenerator>().Order;
        _customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
           .GetComponent<OrderGenerator>().Order.CustomerName;
    }

    private void ResetCustomerSelect()
    {
        if (QueueManager.ActiveCustomerQueue.Count == 0)
        {
            _customerFocusName.text = "No customer";
        }
        else
        {
            CustomerSelectIndex = QueueManager.ActiveCustomerQueue.Count - 1;
            _customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
               .GetComponent<OrderGenerator>().Order.CustomerName;
        }

    

        Debug.Log("ResetCustomerSelect() need to set a new Selected customer on Sale");
    }
}
