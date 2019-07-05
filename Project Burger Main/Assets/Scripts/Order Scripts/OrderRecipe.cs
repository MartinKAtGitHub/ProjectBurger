
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// OrderRecipes will be the recipe after the customer/OrderGenerator has modified it
/// (Removed or added ingredients to base recipe)
/// </summary>
public class OrderRecipe
{
    public Recipe BaseRecipe;
    public List<Ingredient> OrderIngredients = new List<Ingredient>();
    public List<Ingredient> DiscaredIngredients = new List<Ingredient>();
}
