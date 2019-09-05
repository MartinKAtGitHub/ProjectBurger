using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSlot : MonoBehaviour
{
    [SerializeField]private Customer _currentCustomer;
    [SerializeField] private bool _queueSlotInFocus;
    public Customer CurrentCustomer { get => _currentCustomer; set => _currentCustomer = value; }
    public bool QueueSlotInFocus { get => _queueSlotInFocus; set => _queueSlotInFocus = value; }
}
