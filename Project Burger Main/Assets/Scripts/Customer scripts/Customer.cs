using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{

    public bool TestingRandomTimeout;
    public GameObject QueuePositionDot;

    [SerializeField] private CustomerPatience _customerPatience;
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private int _CustomerWaitingTime;
    [SerializeField] private string _customerName;
    [SerializeField] private bool _isWaiting;
    [SerializeField]  private CustomerState _CustomerStates = null;

    private Order _order;
    private CustomerSelect _customerSelect;

    public string CustomerName { get => _customerName; }
    public OrderGenerator OrderGenerator { get; private set; }
    public Order Order { get => _order; }
    public bool IsWaiting { get => _isWaiting; }
    public CustomerState CustomerStates{ get => _CustomerStates; }

    private void OnDestroy()
    {
        //Destoyed
    }

    private void Awake()
    {
        OrderGenerator = GetComponent<OrderGenerator>();

    }

    private void OnEnable()
    {

            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            _order.CustomerName = _customerName;
            _customerPatience.SetOrderPatience(_order);
        
    }

    private void Start()
    {
        //Start Walk
        //Stop At Disc
        //Start Dialog;

       // _CustomerStates.StartCustomerStates();
        _customerSelect = LevelManager.Instance.CustomerSelect;
        if (TestingRandomTimeout)
        {
            Invoke("CustomerTimeout", Random.Range(10, 20));
        }

    }


    void SetWaitingTime()
    {

    }



    public void SetCustomerStates(TheCustomSpawner spawner)
    {
        TransformArray2 Paths = spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups[Random.Range(0, spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups.Length)];
        _CustomerStates.MakeNewInstance(Paths.PathInGroup.Length);

        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;


        for (int i = 0; i < Paths.PathInGroup.Length; i++)
        {
            _CustomerStates.SetBehaviours(
           Paths.PathInGroup[i].PositionsInPath, Paths.PathInGroup[i].Talking, _order.OrderRecipes.Count * 25f * Paths.PathInGroup[i].Patience);
        }
    }

    private void Update()
    {
        if (!_isWaiting && !_customerSelect.InSmoothTransition)
        {
            if (transform.parent == _customerNotInFocusContainer)
            {
                TimeOutInstantRemove();
            }
            else if (transform.parent == _customerInteractionContainer)
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
