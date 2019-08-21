using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSlot : MonoBehaviour
{
    [SerializeField]private Customer _currentCustomer;
    public Customer CurrentCustomer { get => _currentCustomer; set => _currentCustomer = value; }
}
