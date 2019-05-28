using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour // TODO place OrderGenerator per Customer ?
{
    [SerializeField]
    private RecipeBook _recipeBook;
    private Recipe _recipe;

    public Recipe Recipe { get { return _recipe; } }

    /// <summary>
    /// the recipe that is left after we have discarded all the ingredients the customer didn't want
    /// </summary>
    public List<Ingredient> IngredientsForOrder = new List<Ingredient>();
    /// <summary>
    /// The random recipe that this customer choose. 
    /// </summary>
    public Recipe ActiveOrderRecipe{ get { return _recipe; } }
  
    public void RequestOrder()
    {
        // Choose random recipe to use as template for order
        _recipe = _recipeBook.Recipes[Random.Range(0, _recipeBook.Recipes.Count)];

        // Remove ingredient(s) based on RNG
        // Take the new List and use it as Order
        IngredientsForOrder = _recipe.GetOrderFromRecpie();
    }
}
