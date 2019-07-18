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
    [SerializeField] private QueueSlot[] _queueSlots;

    private LimitedQueueDotIndicators _limitedQueueDotIndicators;
    private LimitedCustomerSelect _limitedCustomerSelect;

    // [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>();
    // private CustomerSelect _customerSelect;
    // private QueueDotIndicators _queueDotIndicators;
    public List<Customer> ActiveCustomerQueue { get => null /*_activeCustomerQueue*/; }
    public QueueSlot[] QueueSlots { get => _queueSlots; }
    public int ActiveQueueLimit { get => _activeQueueLimit; }
    public int CurrentActiveCustomer { get => _currentActiveCustomer; }
    public GameObject QueueSlotPrefab { get => _queueSlotPrefab; }

    private void Awake()
    {
        //_customerSelect = GetComponent<CustomerSelect>();
        //_queueDotIndicators = GetComponent<QueueDotIndicators>();
        _limitedQueueDotIndicators = GetComponent<LimitedQueueDotIndicators>();
        _limitedCustomerSelect = GetComponent<LimitedCustomerSelect>();

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

                _limitedQueueDotIndicators.IsQueueSlotOccupied(i, true);

                if (i == _limitedCustomerSelect.QueueSlotIndex)
                {
                    _limitedCustomerSelect.InstaFocusSlot();
                }
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
            if (_queueSlots[i].CurrentCustomer != null)
            {
                if (_queueSlots[i].CurrentCustomer.Equals(customer))
                {
                    Debug.Log($"Removing {customer.name} in Slot {i}");
                    _queueSlots[i].CurrentCustomer = null;
                    _limitedQueueDotIndicators.IsQueueSlotOccupied(i, false);

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
