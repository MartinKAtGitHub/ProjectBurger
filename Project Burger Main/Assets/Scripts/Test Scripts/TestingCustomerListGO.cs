using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCustomerListGO : MonoBehaviour
{
    public Customer[] CustomersArray;
    public List<Customer> CustomersList = new List<Customer>();

    private void Awake()
    {
       // CustomersList = GetComponentsInChildren<Customer>(true);
        GetComponentsInChildren<Customer>(true,CustomersList);
    }
}
