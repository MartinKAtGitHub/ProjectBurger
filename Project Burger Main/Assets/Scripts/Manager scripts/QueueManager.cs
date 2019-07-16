using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private GameObject _queueSlotPrefab;
    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private int _currentActiveCustomer;
    // [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>();
    [SerializeField] private QueueSlot[] _queueSlots;

    // private CustomerSelect _customerSelect;
    // private QueueDotIndicators _queueDotIndicators;
    public List<Customer> ActiveCustomerQueue { get => null /*_activeCustomerQueue*/; }
    public QueueSlot[] QueueSlots { get => _queueSlots; }
    public int ActiveQueueLimit { get => _activeQueueLimit; }
    public int CurrentActiveCustomer { get => _currentActiveCustomer; }
    public GameObject QueueSlotPrefab { get => _queueSlotPrefab;  }

    private void Awake()
    {
        //_customerSelect = GetComponent<CustomerSelect>();
        //_queueDotIndicators = GetComponent<QueueDotIndicators>();

       
        GenerateQueueSlots();
    }

    private void GenerateQueueSlots()
    {
        _queueSlots = new QueueSlot[_activeQueueLimit];

        for (int i = 0; i < _queueSlots.Length; i++)
        {
            var queueSlot = Instantiate(_queueSlotPrefab, _customerNotInFocusContainer.transform);
            _queueSlots[i] = queueSlot.GetComponent<QueueSlot>();

        }

    }

    private void SearchQueueSlotsForEmptySlot(Customer customer)
    {
        for (int i = 0; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                Debug.Log($"Slot {i} is empty, setting {customer.name} in this Position");
                var slot = _queueSlots[i];

                slot.CurrentCustomer = customer;
                customer.transform.SetParent(slot.transform);
                customer.transform.localPosition = Vector2.zero;
                return;
            }
        }
    }

    public void AddCustomerToQueue(Customer customer)
    {
        customer.gameObject.SetActive(true);

        _currentActiveCustomer++;

        SearchQueueSlotsForEmptySlot(customer);


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
        _currentActiveCustomer--;

        for (int i = 0; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i] != null)
            {
                if (_queueSlots[i].Equals(customer))
                {
                    _queueSlots[i] = null;
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
