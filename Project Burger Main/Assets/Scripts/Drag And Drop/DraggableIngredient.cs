using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : Draggable
{
    private bool _onFoodCombiDropArea;

    public FoodCombinationDropArea FoodCombinationDropArea { get; set; }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if (FoodCombinationDropArea != null)
        {
            FoodCombinationDropArea.RemoveIngredientFromFood();
            FoodCombinationDropArea = null;
        }

        //_onFoodCombiDropArea = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
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

            FoodCombinationDropArea.OffsetIngredientObject(this.transform);

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
}

