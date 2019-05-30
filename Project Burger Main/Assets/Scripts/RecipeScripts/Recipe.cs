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
    [SerializeField]
    private foodType _foodType;
    private enum foodType
    {
        NotDefined,
        Hamburger,
        IceCream,
        Pizza
    }

    /// <summary>
    /// Ingredients that go into this recipe
    /// </summary>
    public List<Ingredient> Ingredients;
    /// <summary>
    /// The sprite that shows the finished recipe product
    /// </summary>
    public Sprite RecipeImg { get { return _recipeImg; } }
    public string RecipeName { get { return _recipeName; } }
}
