
using System.Collections.Generic;

/// <summary>
/// Holds the modified food items(recipes) and the customer who requested it
/// </summary>
[System.Serializable]
public class Order
{
    public string CustomerName;
    public int PriceTotal = 0;
    //public List<Sprite> FoodSpritesInThisOrder; // Holds the sprites of the recipes.
    /// <summary>
    /// The food items recipes (discarded / extra / ingredients)
    /// </summary>
    public List<OrderRecipe> OrderRecipes = new List<OrderRecipe>(); // This needs to be capped
    
    public void Initialize()
    {
        OrderRecipes.Capacity = 6; // TODO Order.cs | Need to cap the max amount of food items a customer can have on 1 order
    }

}
