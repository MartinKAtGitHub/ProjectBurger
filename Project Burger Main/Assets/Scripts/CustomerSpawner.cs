using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject Customer;
    public QueueManager QueueManager;

    private int cloneNum = 0;
    // There will be a system in place for spawning customers. for now its only on btn press
    public void SpawnCustomer()
    {
        
        var clone = Instantiate(Customer);
        clone.name += cloneNum++;
        clone.GetComponent<OrderGenerator>().RequestOrder();
        Debug.Log("SPAWNING " + clone.name);

        QueueManager.AddCustomerToQueue(clone);
        // maybe some audio, like a bell
    }
}
