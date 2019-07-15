using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private int _activeQueueCounter;
    [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>();
    [SerializeField] private Customer[] _limitedActiveCustomerQueue;

    // private CustomerSelect _customerSelect;
    // private QueueDotIndicators _queueDotIndicators;
    public List<Customer> ActiveCustomerQueue { get => _activeCustomerQueue; }
    public Customer[] LimitedActiveCustomerQueue { get => _limitedActiveCustomerQueue; }
    public int ActiveQueueLimit { get => _activeQueueLimit; }
    public int ActiveQueueCounter { get => _activeQueueCounter; }

    private void Awake()
    {
        //_customerSelect = GetComponent<CustomerSelect>();
        //_queueDotIndicators = GetComponent<QueueDotIndicators>();

        _limitedActiveCustomerQueue = new Customer[_activeQueueLimit];
    }

    public void AddCustomerToQueue(Customer customer)
    {
        customer.gameObject.SetActive(true);
        _activeQueueCounter++;

        for (int i = 0; i < _limitedActiveCustomerQueue.Length; i++)
        {
            if (_limitedActiveCustomerQueue[i] == null)
            {
                Debug.Log($"Slot {i} is empty, setting {customer.name} in this Position");
                _limitedActiveCustomerQueue[i] = customer;
                return;
            }
        }


        // _activeCustomerQueue.Add(customer);

        // customer.QueuePositionDot = _queueDotIndicators.SpawnDot();

        //if (_activeCustomerQueue.Count == 1)
        //{
        //  //  _customerSelect.ZeroIndexCustomer();
        //}
        //else
        //{
        //    customer.transform.SetParent(_customerNotInFocusContainer);
        //    customer.transform.localPosition = Vector2.zero;
        //}

    }

    public void RemoveCustomerFromQueue(Customer customer)
    {
        _activeQueueCounter--;

        for (int i = 0; i < _limitedActiveCustomerQueue.Length; i++)
        {
            if (_limitedActiveCustomerQueue[i] != null)
            {
                if (_limitedActiveCustomerQueue[i].Equals(customer))
                {
                    _limitedActiveCustomerQueue[i] = null;
                    Destroy(customer.gameObject); // PERFORMANCE Queumanager.cs | this can cause lags, might need to pool ouer characters
                }
            }

        }



        //if (ActiveCustomerQueue.Count != 0)
        //{
        //    _activeCustomerQueue.Remove(customer);
        //    //  _queueDotIndicators.RemoveDots(customer.QueuePositionDot);
        //    Destroy(customer.gameObject); // PERFORMANCE Queumanager.cs | this can cause lags, might need to pool ouer characters
        //}
        //else
        //{
        //    Debug.LogError("Quemanager.cs  RemoveCustomerFromQueue() | You are trying to remove a customer from a empty list");
        //}
    }



}
