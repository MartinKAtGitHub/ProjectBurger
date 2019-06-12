using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDrag : Draggable
{
    // Needs to be drop able on FoodTray and FoodCombi
    // Snap Back to Foodcombi 
    public override void OnBeginDrag(PointerEventData eventData)
    {

        base.OnBeginDrag(eventData);

        //base.OnBeginDrag(eventData);

        //if (!OnDropArea)
        //{
        //    CreatePlaceHolderObj();
        //}

        //if (CurrentDropArea != null)
        //{
        //    CurrentDropArea.IsThisDropAreaOccupied = false;
        //    CurrentDropArea.DropAreaOnBeginDrag();
        //    CurrentDropArea = null;
        //}

        //OnDropArea = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }
}
