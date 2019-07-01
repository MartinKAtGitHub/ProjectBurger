using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>(); // TODO QueueManager can be an array if we have a fixed amount of active customers

    private CustomerSelect _customerSelect;

    public List<Customer> ActiveCustomerQueue { get => _activeCustomerQueue; }
    public int ActiveQueueLimit { get => _activeQueueLimit; }

    private void Start()
    {
        _customerSelect = GetComponent<CustomerSelect>();
    }

    public void AddCustomerToQueue(Customer customer)
    {
        customer.gameObject.SetActive(true);
        _activeCustomerQueue.Add(customer);
      
        if (_activeCustomerQueue.Count == 1)
        {
            _customerSelect.ZeroIndexCustomer();
        }
        else
        {
            customer.transform.SetParent(_customerNotInFocusContainer);
            customer.transform.localPosition = Vector2.zero;
        }

    
    }

    public void RemoveCustomerFromQueue(Customer customer)
    {
        if (ActiveCustomerQueue.Count != 0 )
        {
            _activeCustomerQueue.Remove(customer);
            Destroy(customer.gameObject);
        }
        else
        {
            Debug.LogError("Quemanager.cs  RemoveCustomerFromQueue() | You are trying to remove a customer from a empty list");
        }
    }
}
