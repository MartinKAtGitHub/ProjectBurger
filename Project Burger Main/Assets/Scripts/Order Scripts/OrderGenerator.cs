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
    [SerializeField]
    private int _multiOrderAmount;
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
    public Order ActiveOrder { get { return _order; } }


    /// <summary>
    /// Generates a new order, it will roll to see which ingredients will be removed 
    /// </summary>
    public void RequestOneFoodOrder()
    {
        _order = new Order();
        List<OrderRecipe> orderRecipesIngredients = new List<OrderRecipe>();

        _orderBaseRecipe = _recipeBook.Recipes[Random.Range(0, _recipeBook.Recipes.Count)];    // Choose random recipe to use as template for order

        for (int i = 0; i < _orderBaseRecipe.Ingredients.Count; i++) // Rolles to check if a ingredient will be rmeoved
        {
            var roll = Random.Range(1, 100);

            if (roll < _orderBaseRecipe.Ingredients[i].RemoveChance)// Roll will never be 0% or 100% => %0 safe %100 removed
            {
                _discaredIngredients.Add(_orderBaseRecipe.Ingredients[i]);
            }
            else
            {
                //TODO OrderGenerator We need to create a system for multiple type food order
                orderRecipesIngredients[0].OrderIngredients.Add(_orderBaseRecipe.Ingredients[i]);
            }
        }
        _order.OrderRecipes.Add(orderRecipesIngredients[0]);
    }

    public void RequestOrder()
    {
        _order = new Order();
        var multiOrderChance = 50;
        var multiOrderRoll = Random.Range(1, 100);

        List<OrderRecipe> orderRecipes = new List<OrderRecipe>();

        // Recipe orderBaseRecipe = null;

        if (multiOrderRoll < multiOrderChance)
        {
            Debug.Log("MultiFood");

            for (int i = 0; i < _multiOrderAmount; i++)
            {
                var recipeRoll = Random.Range(1, _recipeBook.totalAccumulatedWight);

                for (int j = 0; j < _recipeBook.Recipes.Count; j++)
                {
                    if (_recipeBook.Recipes[j].AccumulatedWight >= recipeRoll)
                    {
                        Recipe orderBaseRecipe = _recipeBook.Recipes[j];

                        _order.OrderRecipes.Add(CreateOrderRecipe(orderBaseRecipe));
                    }
                }
            }
        }
        else
        {
            Debug.Log("Single food");

            var recipeRoll = Random.Range(1, _recipeBook.totalAccumulatedWight);

            for (int j = 0; j < _recipeBook.Recipes.Count; j++)
            {
                if (_recipeBook.Recipes[j].AccumulatedWight >= recipeRoll)
                {
                    Recipe orderBaseRecipe = _recipeBook.Recipes[j];

                    _order.OrderRecipes.Add(CreateOrderRecipe(orderBaseRecipe));
                }
            }
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
}
