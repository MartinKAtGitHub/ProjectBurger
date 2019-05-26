using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : DropArea
{
    [SerializeField]
    private List<Ingredient> _foodStackIngredients = new List<Ingredient>(); // I cant really see the stack 
    [Space(10)]
    [SerializeField]
    private RecipeBook _recipeBook;

    /// <summary>
    /// The position/index  the food stack which will be checked against the recipes.
    /// Needs to start at -1 because of 0 index in start point
    /// </summary>
    private int _foodStackCheckIndex = -1;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        AddIngredientsToFoodStack(eventData);
    }

    public override void DropAreaOnBeginDrag()
    {
        RemoveIngredientFromFoodStack();
        _foodStackCheckIndex--;
        Debug.Log(_foodStackCheckIndex);
    }

    private void AddIngredientsToFoodStack(PointerEventData eventData)
    {
        var ingredient = eventData.pointerDrag.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            _foodStackIngredients.Add(ingredient);
            //stack.push(ingredient)
            _foodStackCheckIndex++;
            Debug.Log(_foodStackCheckIndex);
            CheckFoodStackWithRecepies();
        }
    }

    private void RemoveIngredientFromFoodStack()
    {
        _foodStackIngredients.RemoveAt(_foodStackIngredients.Count-1);
        //stack.Pop
    }

    private void CheckFoodStackWithRecepies()
    {
        for (int i = 0; i < _recipeBook.Recipes.Count; i++)
        {
            var currentRecipe = _recipeBook.Recipes[i];

            if (_foodStackCheckIndex <= currentRecipe.Ingredients.Count)
            {
                if (_foodStackIngredients[_foodStackCheckIndex].IngredientType == currentRecipe.Ingredients[_foodStackCheckIndex].IngredientType)
                {
                   // Debug.Log("MATCH " + _foodStackIngredients[_foodStackCheckIndex].IngredientType + " = " + currentRecipe.Ingredients[_foodStackCheckIndex].IngredientType);
                    return;
                }
            }
            else
            {
                // go to the next recipe
                continue;
            }
        }
        Debug.LogWarning("NO RECIPE MATCHES THE FOOD YOU ARE MAKEING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
