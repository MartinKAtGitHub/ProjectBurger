using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedCustomerSelect : MonoBehaviour
{
    private QueueManager _queueManager;
    private int _customerIndex;
    private List<GameObject> _queueSlots = new List<GameObject>();

    private void Awake()
    {
        _queueManager = GetComponent<QueueManager>();
    }

    public void NextCustomer()
    {
        _customerIndex++;
    }
    public void PreviousCustomer()
    {
        _customerIndex--;
    }
}
