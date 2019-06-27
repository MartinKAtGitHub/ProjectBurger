﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{
    public bool TestingRandomTimeout;

    [SerializeField] private string _customerName;

    private Order _order;

    public OrderGenerator OrderGenerator { get; private set; }
    public Order Order
    {
        get
        {
            return _order;
        }
    }
    public string CustomerName { get => _customerName; }

    private void Awake()
    {
        OrderGenerator = GetComponent<OrderGenerator>();


    }
    private void OnEnable()
    {
        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;
    }

    private void Start()
    {
        if (TestingRandomTimeout)
        {
          // Invoke("CustomerTimeout", Random.Range(3, 6));
        }
    }

    private void CustomerTimeout()
    {
        //LevelManager.Instance.CustomerSelect.ReFocusCustomerOnTimeOut(this);
        LevelManager.Instance.QueueManager.RemoveCustomerFromQueue(this);
    }
}
