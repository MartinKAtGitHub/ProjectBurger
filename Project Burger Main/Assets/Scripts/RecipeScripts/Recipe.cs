using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Hamburger recipe", fileName ="Recipes /HamburgerRecipe")]
/// <summary>
/// Holds the ingredients and order for all food types
/// </summary>
public abstract class Recipe : ScriptableObject 
{
    public List<Ingredient> Ingredients;
    public abstract List<Ingredient> GetOrderFromRecpie();
}
