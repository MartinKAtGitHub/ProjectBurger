﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates what food(s) the customer wants and packages it into a Order
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    [SerializeField] bool _enableORDERGENEARTIR;

    [SerializeField] private RecipeBook _recipeBook;
    [Range(2, 6)]
    [SerializeField] private int _multiOrderAmount;
    [SerializeField] private int _multiOrderChance = 50;
    /// <summary>
    /// the Order recipe that is left after we have discarded all the ingredients the customer didn't want
    /// </summary>
    [SerializeField] private Order _order;


    private Recipe _orderBaseRecipe;
    private Customer _customer;
  

    /// <summary>
    /// The recipe the order is being generated from
    /// </summary>
    public Recipe OrderBaseRecipe { get { return _orderBaseRecipe; } }
  
    public bool RemoveIngredients;
    
    private void Awake()
    {
        _customer = GetComponent<Customer>();
    }

    /// <summary>
    /// Generates a new order(s) based on chance, the ingredients can be set to be removed at which point
    /// there will be chance to remove the ingredients
    /// </summary>
    public Order RequestOrder()
    {
        _order = new Order();
        var multiOrderRoll = Random.Range(1, 100);
        if (multiOrderRoll < _multiOrderChance)
        {
            //  Debug.Log("Requesting Multi food recipe order");
            for (int i = 0; i < _multiOrderAmount; i++)
            {
                SelectRandomRecipe();
            }
            _customer.IsWaiting = true;
            return _order;
        }
        else
        {
            SelectRandomRecipe();
            _customer.IsWaiting = true;
            return _order;
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
        orderRecipe.BaseRecipe = orderBaseRecipe;
        orderRecipe.OrderRecipePrice = OrderBaseRecipe.Price; 

        for (int i = 0; i < orderBaseRecipe.Ingredients.Count; i++) // Rolls to check if a ingredient will be removed
        {
            var roll = Random.Range(1, 100);
            if (RemoveIngredients && roll < orderBaseRecipe.Ingredients[i].RemoveChance)// Roll will never be 0% or 100% => %0 safe %100 removed
            {
                var ingredient = orderBaseRecipe.Ingredients[i];
                orderRecipe.OrderRecipePrice -= ingredient.IngredientCost;
                orderRecipe.DiscaredIngredients.Add(ingredient);
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
                _orderBaseRecipe = _recipeBook.Recipes[j];

                var orderRecipe = CreateOrderRecipe(_orderBaseRecipe);
                _order.PriceTotal += orderRecipe.OrderRecipePrice; 
                _order.OrderRecipes.Add(orderRecipe);

                return; // If i find a recipe then no need to loop through the rest
            }
        }
        //Debug.LogError("SelectRandomRecipe() Failed to role a recipe | Rollnum: " + recipeRoll);
    }




















    //----------------Trial Code, Need To Get This Somehow To Get My Code To Work-------------------------------------------------
    public void RequestOrderTrial(RecipeBook a, int b, int c)
    {//If We Have 2 ThemeDays At Some Point, Like Green Day And A BurthDay Where A Woman Can Buy A Cake Or Just Plain Salad
        _recipeBook = a;
        _multiOrderAmount = b;
        _multiOrderChance = c;

        RequestOrder();

    }
    //------------------------------------------------------------------------------------------------------------------------------



}
