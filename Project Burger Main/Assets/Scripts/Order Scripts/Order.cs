
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// What the customers want, which can be different from a recipe
/// Holds data of the customer and recipes. 
/// </summary>
public class Order
{
    public string CustomerName;
    //public List<Sprite> FoodSpritesInThisOrder; // Holds the sprites of the recipes.
    public List<OrderRecipe> OrderRecipes = new List<OrderRecipe>();
}
