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

        base.OnBeginDrag(eventData);

        //CalculateDragAreaOffset(eventData);

        //if (!_onFoodCombiDropArea)
        //{
        //    CreatePlaceHolderObj();
        //}

        //FreeDragMode();

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

        //if (OnDropArea)
        //if (_onFoodCombiDropArea)
        //{
        //    ResetPositionToDropArea();
        //}
        //else
        //{
        //    RestToPlaceHolderPosition();
        //}

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
        for (int i = 0; i < FoodCombinationDropArea.FoodStack.GameObjectIngredients.Count; i++)
        {
            FoodCombinationDropArea.FoodStack.GameObjectIngredients[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        FoodCombinationDropArea.FoodStack.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

