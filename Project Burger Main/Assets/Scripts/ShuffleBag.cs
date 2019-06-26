
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class that contains an list of Customers, and will at random pick a unique elemnt. Imagine picking something out of a bag
/// </summary>
public class ShuffleBag : MonoBehaviour
{
    public List<Customer> CustomerPool;

    public bool TESTING_LOOPSUFFLEBAG;

    private Customer _customer;
    /// <summary>
    /// This acts as a blocker. The values that are picked get put in a position that will never be evaluated.
    /// Since we count backwards, everything in-front of this as this goes down the list will not be checked
    /// </summary>
    private int _maxCount;

    public int MaxCount { get => _maxCount; }
    private void Awake()
    {
        //_testingCustomerList = GetComponent<TestingCustomerListGO>();
        //CustomerPool = _testingCustomerList.CustomersList;
        // THIS SHIT BUGS OUT CUZ ITS IN AWAKE SO THE EXECUTE ORDER IS FUCKED

        GetComponentsInChildren<Customer>(true, CustomerPool);
        _maxCount = CustomerPool.Count - 1;

        //   Debug.Log("AWAKE -< maxCount = " + _maxCount);

    }

    public Customer Next()
    {
        // var customer = new Customer();
      //  Debug.Log("MAX COUNT = "  + _maxCount);
        if (_maxCount < 1)
        {
            if (TESTING_LOOPSUFFLEBAG)
            {
                _maxCount = CustomerPool.Count - 1; // With this we just start over again when the pool runs out
                _customer = CustomerPool[0];
                 Debug.Log("ShuffleBag Repeating MaxCount = " + _maxCount);
                return _customer;
            }
            else
            {
                _customer = CustomerPool[0];
                 Debug.Log("SuffleBag FINAL Customer = " + _customer.name);
                return _customer;
            }

            _customer = CustomerPool[0];
           // Debug.Log("FINAL Customer = " + _customer.name);
            return _customer;
        }

        var randomIndex = Random.Range(0, _maxCount);

        _customer = CustomerPool[randomIndex];
        CustomerPool[randomIndex] = CustomerPool[_maxCount];
        CustomerPool[_maxCount] = _customer;
        _maxCount--;

       // Debug.Log("ShuffleBAG Customer = " + _customer.name);

        return _customer;
    }
}
