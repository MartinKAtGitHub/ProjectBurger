
using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag : MonoBehaviour
{
    private TestingCustomerListGO _testingCustomerList;
    public List<Customerss> CustomerPool;
    private Customerss _customer;
    /// <summary>
    /// This acts as a blocker. The values that are picked get put in a position that will never be evaluated.
    /// Since we count backwards, everything in-front of this as this goes down the list will not be checked
    /// </summary>
    private int _maxCount;
    private void Awake()
    {
        _testingCustomerList = GetComponent<TestingCustomerListGO>();
        CustomerPool = _testingCustomerList.CustomersList;
        _maxCount = CustomerPool.Count - 1;
    }

    public Customerss Next()
    {
       // var customer = new Customer();

        if (_maxCount < 1)
        {
            _maxCount = CustomerPool.Count - 1; // With this we just start over again when the pool runs out

            _customer = CustomerPool[0];
            return _customer;
        }

        var randomIndex = Random.Range(0 , _maxCount);

        _customer = CustomerPool[randomIndex];
        CustomerPool[randomIndex] = CustomerPool[_maxCount];
        CustomerPool[_maxCount] = _customer;
        _maxCount--;

        return _customer;
    }
}
