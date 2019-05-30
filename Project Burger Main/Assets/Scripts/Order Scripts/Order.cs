
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// What the customers want, which can be different from a recipe
/// </summary>
public class Order
{
    //public List<Sprite> FoodSpritesInThisOrder; // Holds the sprites of the recipes.

    /// <summary>
    /// The ingredients needed in this food order
    /// </summary>
    public List<Ingredient> OrderIngredients = new List<Ingredient>();
    
    // This is a list filled with recipes without the discarded ingre
    //public List<Recipe> OrderEditedRecipes = new List<Recipe>(); 
}
