using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : Draggable
{
    private bool _onFoodCombiDropArea;

    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }

    // PERFORMANCE DraggableIngredient.cs | instead of checking when to remove ingredient in OnEndDrag you can do it in when a new reset pos is set
    //public override Transform ResetPositionParent
    //{
    //    get => _resetPositionParent;

    //    set
    //    {
    //        if (FoodCombinationDropArea != null)
    //        {
    //            if (value != FoodCombinationDropArea.transform)
    //            {
    //                FoodCombinationDropArea.RemoveIngredientFromFood();
    //            }
    //        }
    //        else
    //        {
    //            _resetPositionParent = value;
    //        }
    //    }
    //}

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        RemoveIngredientFromFoodCombiCheck();
        OnFoodReady();
    }

    /// <summary>
    /// When the final ingredient is dropped we combine the ingredients into the food object.
    /// </summary>
    private void OnFoodReady()
    {
        // NOTE -> Because OnEndDrag runs after OnDrop any changes done will be overwritten
        // So the HAMABUER TOP is not added if we DONT run code here
        if (FoodCombinationDropArea != null) // This will only run if we drop on foodcombi, not on reset
        {

            //FoodCombinationDropArea.OffsetIngredientObject(this.transform);

            if (FoodCombinationDropArea.IsFoodReady)
            {
                FoodCombinationDropArea.MakeFoodDraggable();
                FoodCombinationDropArea.ParentIngredientsToFood();
                FoodCombinationDropArea.IsFoodReady = false;

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
    private void RemoveIngredientFromFoodCombiCheck()
    {
        if (FoodCombinationDropArea != null)
        {
            if (ResetPositionParent != FoodCombinationDropArea.transform)
            {
                FoodCombinationDropArea.RemoveIngredientFromFood();
                FoodCombinationDropArea = null;
            }
        }
    }

}

