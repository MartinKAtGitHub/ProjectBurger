using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientStorageDropArea : MonoBehaviour, IDropHandler
{
    private IngredientsSpawner _ingredientsSpawner;
    private Ingredient.IngredientTypes ingredientType;
   

    private void Awake()
    {
        _ingredientsSpawner = GetComponent<IngredientsSpawner>();
        
    }
    private void Start()
    {
        if (_ingredientsSpawner.IngredientPrefab != null)
        {
            ingredientType = _ingredientsSpawner.IngredientPrefab.GetComponent<IngredientGameObject>().Ingredient.IngredientType;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggableComponent = eventData.pointerDrag.GetComponent<DraggableIngredient>();
        if (draggableComponent != null)
        {
            var ingredientGo = draggableComponent.GetComponent<IngredientGameObject>();
            if (ingredientGo.Ingredient.IngredientType == ingredientType)
            {
                //draggableComponent.ResetPositionParent = _ingredientsSpawner.IngredientPoolRect.transform;
                draggableComponent.ResetPositionParent = _ingredientsSpawner.IngredientPoolRect;
                //draggableComponent.transform.SetParent(this.transform); // <-- double check this to see if this is need - it might happen in OnEndDrop

                ingredientGo.RescaleTouchArea(_ingredientsSpawner.BinRect); // TODO ingredientGo.RescaleTouchArea happens in all drop areas so make it a parent class
            }
        }
    }


}
