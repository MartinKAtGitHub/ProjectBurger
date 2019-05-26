using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Hamburger recipe", fileName ="Recipes /HamburgerRecipe")]
/// <summary>
/// Holds the ingredients and stack order for a food.
/// </summary>
public abstract class Recipe : ScriptableObject 
{
    public List<Ingredient> Ingredients;
    public abstract void GetOrderFromRecpie();

}
