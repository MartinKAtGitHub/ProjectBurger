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
            ingredientType = _ingredientsSpawner.IngredientPrefab.GetComponent<IngredientGameObject>().ingredient.IngredientType;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggableComponent = eventData.pointerDrag.GetComponent<DraggableIngredient>();
        if (draggableComponent != null)
        {
            if (draggableComponent.GetComponent<IngredientGameObject>().ingredient.IngredientType == ingredientType)
            {
                draggableComponent.ResetPositionParent = this.transform;
                draggableComponent.transform.SetParent(this.transform);
            }
        }
    }
}
