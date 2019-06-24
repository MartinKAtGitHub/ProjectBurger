﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customerss : MonoBehaviour
{
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

    private void Start()
    {
        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
    }
}
