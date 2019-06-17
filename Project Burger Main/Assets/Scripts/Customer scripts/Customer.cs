using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour
{

    public OrderGenerator OrderGenerator { get; private set; }

    private void Awake()
    {
        OrderGenerator = GetComponent<OrderGenerator>();
        
    }
}
