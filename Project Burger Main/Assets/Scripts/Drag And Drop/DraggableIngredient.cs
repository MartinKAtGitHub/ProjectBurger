using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : Draggable
{
    private bool _onFoodCombiDropArea;

    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }
    public bool OnFoodCombiDropArea
    {
        get
        {
            return _onFoodCombiDropArea;
        }
        set
        {
            _onFoodCombiDropArea = value;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      //  Debug.Log("PULL FOOD DRAG");
        //base.OnBeginDrag(eventData);
        CalculateDragAreaOffset(eventData);

        //   if (!OnDropArea)
        if (!_onFoodCombiDropArea)
        {
            CreatePlaceHolderObj();
        }

        FreeDragMode();

        if (FoodCombinationDropArea != null)
        {
            FoodCombinationDropArea.DropAreaOnBeginDrag();
            FoodCombinationDropArea = null;
        }

        _onFoodCombiDropArea = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {

        //if (OnDropArea)
        if (_onFoodCombiDropArea)
        {
            ResetPositionToDropArea();
        }
        else
        {
            RestToStartPosition();
        }

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


    private void OnFoodIsReady()
    {
        for (int i = 0; i < FoodCombinationDropArea.FoodStack.FoodStackIngredients.Count; i++)
        {
            FoodCombinationDropArea.FoodStack.FoodStackIngredients[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        FoodCombinationDropArea.FoodStack.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

