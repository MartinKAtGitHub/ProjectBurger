using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    public CustomerWalk theCustomer;
    public FoodTrayDropArea foodTrayDropArea;


    OrderGenerator orderGenerator;



    // Start is called before the first frame update
    void Start() {
    }

    public void SetOrder(CustomerWalk t) {
        Debug.Log("HERE");
        theCustomer = t;
        orderGenerator = theCustomer.GetComponent<OrderGenerator>();
    //    foodTrayDropArea.Order = orderGenerator.Order;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
