using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the recipes in the game
/// </summary>

public class RecipeBook : MonoBehaviour  // make this into a SO maybe. then feed it to a RecepioBook GameObject that handles this data
{
    public List<Recipe> Recipes; // Maybe A list for each section (Hamburg, sandwich, whatever)
}
