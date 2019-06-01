using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OrderRecipes will be the recipe after the customer/OrderGenerator has modified it
/// (Removed or added ingredients to base recipe)
/// </summary>
public class OrderRecipes
{
    public List<Ingredient> OrderIngredients = new List<Ingredient>();
}
