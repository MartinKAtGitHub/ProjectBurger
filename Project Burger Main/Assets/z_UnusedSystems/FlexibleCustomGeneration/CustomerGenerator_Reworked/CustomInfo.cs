using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInfo : MonoBehaviour {

    public Order order;


    public void SetOrder(Order a) {//Copy Order Info
        order = a;
    }

    private void Update() {
        if(order != null)
        Debug.Log(order.OrderRecipes.Count);
    }


}
