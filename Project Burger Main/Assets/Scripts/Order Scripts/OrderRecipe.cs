
using System.Collections.Generic;
/// <summary>
/// OrderRecipes will be the recipe after the customer/OrderGenerator has modified it
/// (Removed or added ingredients to base recipe)
/// </summary>
[System.Serializable]
public class OrderRecipe
{
    public Recipe BaseRecipe;
    public List<Ingredient> OrderIngredients = new List<Ingredient>();
    public List<Ingredient> DiscaredIngredients = new List<Ingredient>();
    public List<Ingredient> ExtraIngredients = new List<Ingredient>();
    // public SpecialRequest

    private int _orderRecipePrice;

    public int OrderRecipePrice { get => _orderRecipePrice; set => _orderRecipePrice = value; }
}
