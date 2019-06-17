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
        LevelManager.Instance.QueueManager = this;
    }
    private void Start()
    {
      
        LevelManager.Instance.SalesManager.OnSale += RemoveCustomerFromQueue;
    }

    public void AddCustomerToQueue(GameObject customer)
    {
        _activeCustomerQueue.Add(customer);
    }

    public void RemoveCustomerFromQueue()
    {
       if(ActiveCustomerQueue.Count != 0)
        {
            var customerIndex = LevelManager.Instance.CustomerSelect.CustomerSelectIndex;
            var customer = _activeCustomerQueue[customerIndex];

            _activeCustomerQueue.RemoveAt(customerIndex);

            Debug.Log($"Destroying {customer.name} from QueueManger OnRemove");
            Destroy(customer);
        }
    }
}
