using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : Draggable
{
    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }
    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("PULL FOOD DRAG");
        //base.OnBeginDrag(eventData);
        CalculateDragAreaOffset(eventData);

        if (!OnDropArea)
        {
            CreatePlaceHolderObj(); 
        }

        FreeDragMode();

        if (FoodCombinationDropArea != null) 
        {
            FoodCombinationDropArea.DropAreaOnBeginDrag();
            FoodCombinationDropArea = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {

        if (OnDropArea)
        {
            ResetPositionToDropArea();
        }
        else
        {
            RestToStartPosition();
        }

        if(FoodCombinationDropArea != null)
        {
            if(FoodCombinationDropArea.IsFoodReady)
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


    private void OnFoodIsReady()
    {
        for (int i = 0; i < FoodCombinationDropArea.FoodStack.FoodStackIngredients.Count; i++)
        {
            FoodCombinationDropArea.FoodStack.FoodStackIngredients[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        FoodCombinationDropArea.FoodStack.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

