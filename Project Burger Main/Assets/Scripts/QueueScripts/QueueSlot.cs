using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueueSlot : SlotHorizontal
{
    [SerializeField]private Customer _currentCustomer;
    [SerializeField] private bool _queueSlotInFocus;
    public Customer CurrentCustomer { get => _currentCustomer; set => _currentCustomer = value; }
    // do i need this ?????
    public bool QueueSlotInFocus { get => _queueSlotInFocus; set => _queueSlotInFocus = value; } 
}
