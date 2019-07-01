using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{
    public bool TestingRandomTimeout;

    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private string _customerName;
    [SerializeField] private bool _isWaiting;


    private Order _order;
    private CustomerSelect _customerSelect;
    public OrderGenerator OrderGenerator { get; private set; }
    public Order Order { get => _order; }
    public string CustomerName { get => _customerName; }
    public bool IsWaiting { get => _isWaiting; }

    private void Awake()
    {
        OrderGenerator = GetComponent<OrderGenerator>();
    }

    private void OnEnable()
    {
        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;
        // _isWaiting = true;
    }

    private void Start()
    {
        _customerSelect = LevelManager.Instance.CustomerSelect;
        if (TestingRandomTimeout)
        {
            Invoke("CustomerTimeout", Random.Range(5, 5));
        }
    }

    private void Update()
    {
        if(!_isWaiting && !_customerSelect.InSmoothTransition)
        {
            if(transform.parent == _customerNotInFocusContainer )
            {
                TimeOutInstantRemove();
            }
            else if(transform.parent == _customerInteractionContainer)
            {
                TimeOutPlayAnim();
            }
        }
    }

    private void CustomerTimeout()
    {
        Debug.Log($"{name} has been Flagged for Deletion");
        // TODO Customer.cs | Timeout(), We need to make sure the customer doesn't timeout while in a animation.
        _isWaiting = false;
    }

    private void TimeOutInstantRemove()
    {
        Debug.Log($"{name} Removed No Anim");
        LevelManager.Instance.CustomerSelect.OnTimeOut(this);
        LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(this);
    }

    private void TimeOutPlayAnim()
    {
        if (!IsWaiting)
        {
            Debug.Log($"{name} Removed Play Anim");
            LevelManager.Instance.CustomerSelect.OnTimeOut(this);
            LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(this);

        }
    }


}
