using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderWindowSwiper : TouchSwipeController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LimitedNextElement()
    {
        base.LimitedNextElement();
       
    }

    protected override void LimitedPrevElement()
    {
        base.LimitedPrevElement();
       
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnCustomerChange() // move this to window
    {
        // Fade out Orderwin text
        // Update all the pnls with Order/ foodItems from this new customer
        // And reset the index back 0(start to swipe from 0)
        // Fade back inn
    }

    private void SetInitPage()
    {
        //var initialFoodPnl = _orderWindow.FoodItemPnls[0];
        //_activePage = initialFoodPnl;
        //initialFoodPnl.transform.SetParent(_slidingContainer);
    }
}
