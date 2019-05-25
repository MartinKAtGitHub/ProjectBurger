using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the ingredients and stack order for a food.
/// </summary>
public class Recipe : MonoBehaviour // make this into a SO maybe. 
{
    enum FoodType
    {
        Undefined, Hamburger, Sandwich
    }
    public List<Ingredient> Ingredients;
}
