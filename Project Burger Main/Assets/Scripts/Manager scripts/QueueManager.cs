using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private int _numOfActiveCustomers;
    [SerializeField] private GameObject _queueSlotPrefab;
    [SerializeField] private RectTransform _customerSwipeContainer;
   
    private QueueSlot[] _queueSlots;
    //private RectTransform[] _Slots;

    // [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>();
    // private CustomerSelect _customerSelect;
    // private QueueDotIndicators _queueDotIndicators;
    public List<Customer> ActiveCustomerQueue { get => null /*_activeCustomerQueue*/; }
    public QueueSlot[] QueueSlots { get => _queueSlots; }
    public int ActiveQueueLimit { get => _activeQueueLimit; }
    public int NumOfActiveCustomers { get => _numOfActiveCustomers; }
    public GameObject QueueSlotPrefab { get => _queueSlotPrefab; }

    private void Awake()
    {
        //_customerSelect = GetComponent<CustomerSelect>();
        //_queueDotIndicators = GetComponent<QueueDotIndicators>();
        //_limitedQueueDotIndicators = GetComponent<LimitedQueueDotIndicators>();
        //_limitedCustomerSelect = GetComponent<LimitedCustomerSelect>();
        _customerSwipeContainer = LevelManager.Instance.CustomerSelectSwiper.HorizontalSwipeContainer;
        GenerateQueueSlots();
    }

    private void Start()
    {
        //GenerateQueueSlots();
    }

    private void GenerateQueueSlots()
    {
        _queueSlots = new QueueSlot[_activeQueueLimit];

        for (int i = 0; i < _queueSlots.Length; i++)
        {
            var queueSlot = Instantiate(_queueSlotPrefab, _customerSwipeContainer.transform);
            queueSlot.name = "QSlot " + i;
            _queueSlots[i] = queueSlot.GetComponent<QueueSlot>();
        }

        LevelManager.Instance.CustomerSelectSwiper.QueueSlots = _queueSlots;
    }

    public void AddCustomerToQueue(Customer customer)
    {
        _numOfActiveCustomers++;
        customer.gameObject.SetActive(true);

        InsertCustomerToEmptyQueueSlot(customer);
        
    }

    public void RemoveCustomerFromQueue(Customer customer)
    {
        _numOfActiveCustomers--;

        for (int i = 0; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i].CurrentCustomer != null)
            {
                if (_queueSlots[i].CurrentCustomer.Equals(customer))
                {
                    Debug.Log($"Removing {customer.name} in Slot {i}");
                    _queueSlots[i].CurrentCustomer = null;

                   // _limitedQueueDotIndicators.IsQueueSlotOccupied(i, false);

                    Destroy(customer.gameObject); // PERFORMANCE Queumanager.cs | this can cause lags, might need to pool our characters
                }
            }

        }
    }
    private void InsertCustomerToEmptyQueueSlot(Customer customer)
    {
        for (int i = 0; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {

              //  Debug.Log($"Slot {i} is empty, setting {customer.name} in this Position");
                var slot = _queueSlots[i];
                slot.CurrentCustomer = customer;

                // ANIM AND STUFF WOULD GO HERE
                customer.transform.SetParent(slot.transform);
                customer.transform.localPosition = Vector2.zero;

                if(slot.QueueSlotInFocus)
                {
                   // LevelManager.Instance.OrderWindow.UpdateUI(customer);
                }
                
                //_limitedQueueDotIndicators.IsQueueSlotOccupied(i, true);

                //if (i == _limitedCustomerSelect.QueueSlotIndex)
                //{
                //    _limitedCustomerSelect.InstaFocusSlot();
                //}


                return;
            }
        }
    }

}
