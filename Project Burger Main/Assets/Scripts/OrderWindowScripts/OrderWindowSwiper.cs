using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderWindowSwiper : TouchSwipeController
{
    RequestContainer[] _requestContainers;

    public RequestContainer[] RequestContainers { get => _requestContainers; set => _requestContainers = value; }

    protected override void Awake()
    {
        base.Awake();
       
    }
    protected override void Start()
    {
        base.Start();
        _slotsHorizontal = _requestContainers;

        InitializeTouchControll();
    }


    protected override void InitializeTouchControll()
    {
        _verticalSwipeContainer = _requestContainers[0].VerticalSwiper;
        base.InitializeTouchControll();
        Debug.Log("Name ->" + _verticalSwipeContainer.name);
    }
    

    public override void OnDrag(PointerEventData eventData)
    {
        HorizontalDragging(eventData);
        VerticalDragging(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestHorizontalElement(eventData);
        SnapToClosestVerticalElement(eventData);
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


  
}
