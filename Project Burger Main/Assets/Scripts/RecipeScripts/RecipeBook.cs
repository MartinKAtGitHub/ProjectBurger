using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the recipes in the game
/// </summary>
[CreateAssetMenu(menuName = "RecipeBook", fileName = "RecipeBook")]
public class RecipeBook : ScriptableObject  // Static ? we would really never need to have multiple recipeBook
{
    public List<Recipe> Recipes; //TODO Maybe A list for each section (Hamburg, sandwich, whatever)
}
