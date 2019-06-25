using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueManager : MonoBehaviour
{

    [SerializeField] private Transform _customerSwipeContainer;
    [SerializeField] private int _activeQueueLimit;
    [SerializeField] private List<Customer> _activeCustomerQueue = new List<Customer>(); // TODO QueueManager can be an array if we have a fixed amount of active customers

    private CustomerSelect _customerSelect;

    public List<Customer> ActiveCustomerQueue { get => _activeCustomerQueue; }
    public int ActiveQueueLimit { get => _activeQueueLimit;}

    private void Start()
    {
        _customerSelect = GetComponent<CustomerSelect>();
    }

    public void AddCustomerToQueue(Customer customer)
    {
        _activeCustomerQueue.Add(customer);

       
        customer.transform.SetParent(_customerSwipeContainer);
        customer.transform.SetAsFirstSibling();
        customer.gameObject.SetActive(true);
        //_customerSelect.RemovePlaceHolder();
    }

    public void RemoveCustomerFromQueue() // TODO QueueManager.cs | RemoveCustomerFromQueue() Dose not take into account Timeout customer. Maybe make diffrent versions
    {
       if(ActiveCustomerQueue.Count != 0)
        {
            var customer = LevelManager.Instance.CustomerSelect.CustomerInFocus;

          //  Debug.Log($"Destroying {customer.CustomerName} from QueueManger OnRemove");
            _activeCustomerQueue.Remove(customer);

            Destroy(customer.gameObject);
        }
    }
}
