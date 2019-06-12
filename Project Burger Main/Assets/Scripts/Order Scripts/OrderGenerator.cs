using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates what food(s) the customer wants and packages it into a Order
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    public bool RemoveIngredients;
    [SerializeField]
    private RecipeBook _recipeBook;

    [Range(2,6)]
    [SerializeField]
    private int _multiOrderAmount;
    [SerializeField]
    private int _multiOrderChance = 50;
    private Recipe _orderBaseRecipe;
    /// <summary>
    /// the Order recipe that is left after we have discarded all the ingredients the customer didn't want
    /// </summary>
    [SerializeField]
    private Order _order;

    private List<Ingredient> _discaredIngredients = new List<Ingredient>();

    /// <summary>
    /// The random recipe the order is being generated from
    /// </summary>
    public Recipe OrderBaseRecipe { get { return _orderBaseRecipe; } }
    public List<Ingredient> DiscaredIngredients { get { return _discaredIngredients; } }
    public Order Order { get { return _order; } }

    /// <summary>
    /// Generates a new order(s) based on chance, the ingredients can be set to be removed at which point
    /// there will be chance to remove the ingredients
    /// </summary>
    public void RequestOrder()
    {
        _order = new Order();
        _order.CustomerName = name;
        var multiOrderRoll = Random.Range(1, 100);

        List<OrderRecipe> orderRecipes = new List<OrderRecipe>();

        // Recipe orderBaseRecipe = null;

        if (multiOrderRoll < _multiOrderChance)
        {
            Debug.Log("Requesting Multi food recipe order");
            for (int i = 0; i < _multiOrderAmount; i++)
            {
                SelectRandomRecipe();
            }
        }
        else
        {
            Debug.Log("Requesting Single food order");
            SelectRandomRecipe();
        }

        if (_order.OrderRecipes.Count == 0)
        {
            Debug.LogError("Generator failed to create a valid order, This should never happen| Order count 0" );
        }

    }

    /// <summary>
    /// Removes unwanted ingredients based on chance, and generates a Order Recipe the player will try to match
    /// </summary>
    /// <param name="orderBaseRecipe">The base recipe the OrderRecipe will be based on</param>
    /// <returns></returns>
    private OrderRecipe CreateOrderRecipe(Recipe orderBaseRecipe)
    {
        OrderRecipe orderRecipe = new OrderRecipe();
        //orderRecipe.CustomerName = gameObject.name;

        for (int i = 0; i < orderBaseRecipe.Ingredients.Count; i++) // Rolls to check if a ingredient will be removed
        {
            var roll = Random.Range(1, 100);
            if (RemoveIngredients && roll < orderBaseRecipe.Ingredients[i].RemoveChance)// Roll will never be 0% or 100% => %0 safe %100 removed
            {
                orderRecipe.DiscaredIngredients.Add(orderBaseRecipe.Ingredients[i]);
            }
            else
            {
                orderRecipe.OrderIngredients.Add(orderBaseRecipe.Ingredients[i]);
            }
        }
        return orderRecipe;
    }



    private void SelectRandomRecipe()
    {
        var recipeRoll = Random.Range(1, _recipeBook.totalAccumulatedWight);

        for (int j = 0; j < _recipeBook.Recipes.Count; j++)
        {
            if (_recipeBook.Recipes[j].AccumulatedWight >= recipeRoll)
            {
                // var orderBaseRecipe = _recipeBook.Recipes[j]; // This turns into null for some reason, but only for the first spawn lel
                //Debug.Log("SELECTED RECIPE NAME = " + _recipeBook.Recipes[j].name);
                _order.OrderRecipes.Add(CreateOrderRecipe(_recipeBook.Recipes[j]));

                return; // If i find a recipe then no need to loop through the rest
            }
        }
        Debug.LogError("SelectRandomRecipe() Failed to role a recipe | Rollnum: " + recipeRoll);
    }
}
