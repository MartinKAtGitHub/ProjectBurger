using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{


    [SerializeField] private CustomerPatience _customerPatience;
    [SerializeField] private string _customerName;
    [SerializeField] private bool _isWaiting;
 
    private Order _order;
    private OrderGenerator _orderGenerator;

    public string CustomerName { get => _customerName; }
    public OrderGenerator OrderGenerator { get => _orderGenerator; private set => _orderGenerator = value; }
    public Order Order { get => _order; }
    public bool IsWaiting { get => _isWaiting; set => _isWaiting = value; }
    
    public GameObject QueuePositionDot;

    private void Awake()
    {
        _orderGenerator = GetComponent<OrderGenerator>();
        
        if (_order == null)
        {
            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            //_order.CustomerName = _customerName;
        }
    }

    private void Start()
    {
        if(_order != null)
        {
            _customerPatience.SetOrderPatience(_order); // move to generator. We want to do this when we generate the order
        }
        
        //Invoke("CustomerTimeout", _customerPatience.CustomerWaitingTime);
    }

}
