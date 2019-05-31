
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// What the customers want, which can be different from a recipe
/// </summary>
public class Order
{
    //public List<Sprite> FoodSpritesInThisOrder; // Holds the sprites of the recipes.
    public List<OrderRecipes> OrderRecipes = new List<OrderRecipes>();

}
