using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField]
    private RecipeBook _recipeBook;

    public List<Ingredient> OrderIngredient = new List<Ingredient>();
    void Start()
    {
        RequestOrder();
    }


    public void RequestOrder()
    {
        // Choose random recipe to use as template for order
        var randomRecipe = _recipeBook.Recipes[Random.Range(0, _recipeBook.Recipes.Count)];

        // Remove ingredient(s) based on RNG
        // Take the new List and use it as Order
        OrderIngredient = randomRecipe.GetOrderFromRecpie();
    }
}
