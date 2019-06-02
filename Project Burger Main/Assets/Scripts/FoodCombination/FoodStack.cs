using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The physical / GameObject version of a recipe
/// </summary>
public class FoodStack : MonoBehaviour
{
    public List<IngredientGameObject> FoodStackIngredients = new List<IngredientGameObject>();

    public bool FailedToMatchOrder { get; set; }
}
