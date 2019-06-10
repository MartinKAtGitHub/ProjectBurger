using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField]
    private int _maxActiveCustomerAmount;
    [SerializeField]
    private List<GameObject> _activeCustomerQueue = new List<GameObject>(); // TODO QueueManager can be an array if we have a fixed amount of active customers

    public List<GameObject> ActiveCustomerQueue { get => _activeCustomerQueue; }

    private void Awake()
    {

    }

    private void Start()
    {
        // wait for init prep time
        // Then activate queue
    }

    public void OnProcessCustomer()
    {
        // Send data to OrderWindow // But is it really this class job ?????????
        // StartCorutin(queue.peek.waitForFood())
        //queue.pop
        // queue.peek.GoToCounterToOrderFood();
    }


    public void AddCustomerToQueue(GameObject customer)
    {
        _activeCustomerQueue.Add(customer);
    }

}
