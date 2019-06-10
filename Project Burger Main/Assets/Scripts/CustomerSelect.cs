using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelect : MonoBehaviour
{
    public int CustomerSelectIndex = 0;
    public QueueManager QueueManager;
    public FoodTrayDropArea FoodTrayDropArea;


    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();
    }

    public void NextCustomer()
    {
        //Debug.Log(CustomerSelectIndex +" | "+  QueueManager.ActiveCustomerQueue.Count);
        if(CustomerSelectIndex < QueueManager.ActiveCustomerQueue.Count - 1)
        {
            CustomerSelectIndex++;
            //Debug.Log(CustomerSelectIndex + " | " + QueueManager.ActiveCustomerQueue.Count);

            FoodTrayDropArea.Order = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
                .GetComponent<OrderGenerator>().Order; 
            // The Array will hold the ref to customer which will have the data needed insted of doing getcomp every time you switch
        }else
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
    }
}
