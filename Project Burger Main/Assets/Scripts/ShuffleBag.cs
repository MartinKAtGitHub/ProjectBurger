
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class that contains an list of Customers, and will at random pick a unique elemnt. Imagine picking something out of a bag
/// </summary>
public class ShuffleBag : MonoBehaviour
{
    public List<Customer> CustomerPool;
    private Customer _customer;
    /// <summary>
    /// This acts as a blocker. The values that are picked get put in a position that will never be evaluated.
    /// Since we count backwards, everything in-front of this as this goes down the list will not be checked
    /// </summary>
    private int _maxCount;
    private void Awake()
    {
        //_testingCustomerList = GetComponent<TestingCustomerListGO>();
        //CustomerPool = _testingCustomerList.CustomersList;
        // THIS SHIT BUGS OUT CUZ ITS IN AWAKE SO THE EXECUTE ORDER IS FUCKED

        GetComponentsInChildren<Customer>(true, CustomerPool);
        _maxCount = CustomerPool.Count - 1;

        Debug.Log("AWAKE -< maxCount = " + _maxCount);

    }

    public Customer Next()
    {
       // var customer = new Customer();

        if (_maxCount < 1)
        {
            _maxCount = CustomerPool.Count - 1; // With this we just start over again when the pool runs out

            _customer = CustomerPool[0];

            Debug.Log("WHY DOSE THIS FIRE FIRST LUL");
            return _customer;
        }

        var randomIndex = Random.Range(0 , _maxCount);

        _customer = CustomerPool[randomIndex];
        CustomerPool[randomIndex] = CustomerPool[_maxCount];
        CustomerPool[_maxCount] = _customer;
        _maxCount--;

        Debug.Log("SUFFLE BAG => RandIndex :" + randomIndex + " MaxCount : " + _maxCount + " CustomerName : " + _customer.name);

        return _customer;
    }
}
