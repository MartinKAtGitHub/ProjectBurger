using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Hamburger recipe", fileName ="Recipes /HamburgerRecipe")]
/// <summary>
/// Holds the ingredients and order for all food types
/// </summary>
public abstract class Recipe : ScriptableObject
{
    [SerializeField]
    private Sprite _recipeImg;
    [SerializeField]
    private string _recipeName;

    protected List<Ingredient> _discaredIngredients;

    public List<Ingredient> Ingredients;
    /// <summary>
    /// The sprite that shows the finished recipe product
    /// </summary>
    public Sprite RecipeImg { get { return _recipeImg; } }
    public string RecipeName { get { return _recipeName; } }
    public List<Ingredient> DiscaredIngredients { get { return _discaredIngredients; } }

    public abstract List<Ingredient> GetOrderFromRecpie();
}
