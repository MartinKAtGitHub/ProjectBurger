using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Holds the food order that is ready to be sold
/// </summary>
public class FoodReadyTray : DropArea
{
    Order _orders;

    public override void DropAreaOnBeginDrag()
    {
        Debug.Log("Dragging from food tray ");
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        // you cant pick the food back up

    }


    private void AutoSell()
    {
        // Check if foodtype(s) for order is == null
        // 
    }
}
