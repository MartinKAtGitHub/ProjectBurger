using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : Draggable
{
    private bool _onFoodCombiDropArea;

    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }
    //public bool OnFoodCombiDropArea
    //{
    //    get
    //    {
    //        return _onFoodCombiDropArea;
    //    }
    //    set
    //    {
    //        _onFoodCombiDropArea = value;
    //    }
    //}

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if (FoodCombinationDropArea != null)
        {
            FoodCombinationDropArea.DropAreaOnBeginDrag();
            FoodCombinationDropArea = null;
        }

        //_onFoodCombiDropArea = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (FoodCombinationDropArea != null)
        {
            if (FoodCombinationDropArea.IsFoodReady)
            {
                OnFoodIsReady();
            }
            else
            {
                canvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// This disables the drag on ingredients AND enables it on the final Food Gameobject.
    /// </summary>
    private void OnFoodIsReady() // PERFORMANCE DraggableIngredient.cs -> the OnFoodIsReady() is check every time a ingredient is dropped on the foodcombination pad.
    {
        for (int i = 0; i < FoodCombinationDropArea.Food.GameObjectIngredients.Count; i++)
        {
            FoodCombinationDropArea.Food.GameObjectIngredients[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        FoodCombinationDropArea.Food.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

