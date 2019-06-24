using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDrag : Draggable
{
    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }
    /// <summary>
    /// A direct reference to FoodCombination Transform, Used to detect if we have left the pad or are still connected to it
    /// </summary>
   // public Transform FoodCombinationTransform { get; set; }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if (FoodCombinationDropArea != null)
        {
            FoodCombinationDropArea.OccupiedByFood = false;
            FoodCombinationDropArea.Food = null;
           // FoodCombinationDropArea = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (FoodCombinationDropArea != null) // Maybe we just need to check for this, Set null in other drop zone
        {
            if (ResetPositionParent == FoodCombinationDropArea.transform)
            {
                FoodCombinationDropArea.OccupiedByFood = true;
            }
            else
            {
                FoodCombinationDropArea = null;
            }
        }

    }
}
