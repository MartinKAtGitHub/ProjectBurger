using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject CustomerPrefab;

    [SerializeField] private Transform _customerParent;

    private int cloneNum = 0;
    // There will be a system in place for spawning customers. for now its only on btn press
    public void SpawnCustomer()
    {

        var clone = Instantiate(CustomerPrefab, _customerParent);
        clone.name += cloneNum++;
        var customer = clone.GetComponent<Customer>();
      //  customer.Order = customer.OrderGenerator.RequestOrder;

        LevelManager.Instance.QueueManager.AddCustomerToQueue(clone.GetComponent<Customer>());
        
        // maybe some audio, like a bell
        Debug.Log("SPAWNING " + clone.name);
    }
}
