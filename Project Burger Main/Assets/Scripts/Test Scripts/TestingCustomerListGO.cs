using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCustomerListGO : MonoBehaviour
{
    public Customerss[] CustomersArray;
    public List<Customerss> CustomersList = new List<Customerss>();

    private void Awake()
    {
       // CustomersList = GetComponentsInChildren<Customer>(true);
        GetComponentsInChildren<Customerss>(true,CustomersList);
    }
}
