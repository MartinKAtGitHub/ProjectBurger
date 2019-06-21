using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private Transform _customerSwipeContainer;
    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>(); // TODO QueueManager can be an array if we have a fixed amount of active customers

    public List<Customer> ActiveQueueLimit { get => _activeCustomerQueue; }
    public int MaxActiveCustomerAmount { get => _activeQueueLimit;}

    public void Initialize()
    {
        //LevelManager.Instance.QueueManager = this;
        LevelManager.Instance.SalesManager.OnSale += RemoveCustomerFromQueue;
    }

    public void AddCustomerToQueue(Customer customer)
    {
        _activeCustomerQueue.Add(customer);

        customer.transform.SetParent(_customerSwipeContainer);
        customer.transform.SetAsFirstSibling();
        customer.gameObject.SetActive(true);
    }

    public void RemoveCustomerFromQueue() // TODO QueueManager.cs | RemoveCustomerFromQueue() might remove the wrong customer. Test this method to make sure it removes the correct customer
    {
       if(ActiveQueueLimit.Count != 0)
        {
            //var customerIndex = LevelManager.Instance.CustomerSelect.CustomerSelectIndex;
            //var customer = _activeCustomerQueue[customerIndex];
            var customer = LevelManager.Instance.CustomerSelect.CustomerInFocus;

            _activeCustomerQueue.Remove(customer);

            Debug.Log($"Destroying {customer.CustomerName} from QueueManger OnRemove");
            Destroy(customer);
        }
    }
}
