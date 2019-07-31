using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{

    public GameObject QueuePositionDot;

    [SerializeField] private CustomerPatience _customerPatience;
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    //[SerializeField] private int _customerWaitingTime;
    [SerializeField] private string _customerName;
    [SerializeField] private bool _isWaiting;
 //   [SerializeField] private CustomerState _CustomerStates = null;

    private Order _order;
   // private CustomerSelect _customerSelect;
    private OrderWindow _orderWindow;
    private OrderGenerator _orderGenerator;

    public string CustomerName { get => _customerName; }
    public OrderGenerator OrderGenerator { get => _orderGenerator; private set => _orderGenerator = value; }
    public Order Order { get => _order; }
    public bool IsWaiting { get => _isWaiting; }
 //   public CustomerState CustomerStates { get => _CustomerStates; }
    public OrderWindow OrderWindow { get => _orderWindow; }

    private void OnDestroy()
    {
        //Destoyed
    }

    private void Awake() {
        _orderGenerator = GetComponent<OrderGenerator>();
        _orderWindow = GetComponent<OrderWindow>();

        if (_order == null) {
            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            _order.CustomerName = _customerName;
        }
    }

    private void Start()
    {
            _customerPatience.SetOrderPatience(_order);

            Invoke("CustomerTimeout", _customerPatience.CustomerWaitingTime);
    }

    public void SetCustomerStates(TheCustomSpawner spawner)
    {
     //   TransformArray2 Paths = spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups[Random.Range(0, spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups.Length)];
     //   _CustomerStates.MakeNewInstance(Paths.PathInGroup.Length);

        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;


     /*   for (int i = 0; i < Paths.PathInGroup.Length; i++)
        {
            _CustomerStates.SetBehaviours(
           Paths.PathInGroup[i].PositionsInPath, Paths.PathInGroup[i].Talking, _order.OrderRecipes.Count * 25f * Paths.PathInGroup[i].Patience);
        }*/
    }

    private void Update()
    {
        //if (!_isWaiting && !_customerSelect.InSmoothTransition)
        //{
        //    if (transform.parent == _customerNotInFocusContainer)
        //    {
        //        TimeOutInstantRemove();
        //    }
        //    else if (transform.parent == _customerInteractionContainer)
        //    {
        //        TimeOutPlayAnim();
        //    }
        //}
    }

    private void CustomerTimeout()
    {
        Debug.Log($"{name} has been Flagged for Deletion");
        // TODO Customer.cs | Timeout(), We need to make sure the customer doesn't timeout while in a animation.
        _isWaiting = false;
        TimeOutInstantRemove();
    }

    private void TimeOutInstantRemove()
    {
        Debug.Log($"{name} Removed No Anim");
        //  _customerSelect.OnTimeOut(this);
        LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(this);

    }

    private void TimeOutPlayAnim()
    {
        if (!IsWaiting)
        {
            Debug.Log($"{name} Removed Play Anim");
            //_customerSelect.OnTimeOut(this);
            //_customerSelect.CircularRight();
            LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(this);
            OrderWindow.CloseWindow();

        }
    }

}
