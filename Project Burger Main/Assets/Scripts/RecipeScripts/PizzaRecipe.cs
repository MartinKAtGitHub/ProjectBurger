using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pizza recipe", fileName = "Recipes/PizzaRecipe")]
public class PizzaRecipe : Recipe
{
    public override List<Ingredient> GetOrderFromRecpie()
    {
        throw new System.NotImplementedException();
    }
}
