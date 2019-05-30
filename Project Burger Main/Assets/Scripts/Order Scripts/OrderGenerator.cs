using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates what food(s) the customer wants and packages it into a Order
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    [SerializeField]
    private RecipeBook _recipeBook;
    private Recipe _orderBaseRecipe;
    /// <summary>
    /// the Order recipe that is left after we have discarded all the ingredients the customer didn't want
    /// </summary>
    private Order _order;
    private List<Ingredient> _discaredIngredients = new List<Ingredient>();

    /// <summary>
    /// The random recipe the order is being generated from
    /// </summary>
    public Recipe OrderBaseRecipe { get { return _orderBaseRecipe; } }
    public List<Ingredient> DiscaredIngredients { get { return _discaredIngredients; } }
    /// <summary>
    /// Generates a new order, it will roll to see which ingredients will be removed 
    /// </summary>
    public void RequestOrder()
    {
        _order = new Order();
        // Choose random recipe to use as template for order
        _orderBaseRecipe = _recipeBook.Recipes[Random.Range(0, _recipeBook.Recipes.Count)];

        for (int i = 0; i < _orderBaseRecipe.Ingredients.Count; i++)
        {
            var roll = Random.Range(1, 100); // 1 included 100 excluded due to arrays -1 

            if (roll < _orderBaseRecipe.Ingredients[i].RemoveChance)// Roll will never be 0% or 100% => %0 safe %100 removed
            {
                _discaredIngredients.Add(_orderBaseRecipe.Ingredients[i]);
            }
            else
            {
                _order.OrderIngredients.Add(_orderBaseRecipe.Ingredients[i]);
            }
        }
    }
}
