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
    protected override void Start()
    {
        base.Start();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        HorizontalDragging(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestHorizontalElement(eventData);
    }


    //protected override void SnapNextHorizontalElement()
    //{
    //    base.SnapNextHorizontalElement();
    //}

    //protected override void SnapPrevHorizontalElement()
    //{
    //    base.SnapPrevHorizontalElement();
    //}

    //protected override void ResetHorizontalElement()
    //{
    //    base.ResetHorizontalElement();
    //}




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


    protected override void SetSlotRects(SlotHorizontal[] slotHorizontal)
    {
        throw new NotImplementedException();
    }
}
