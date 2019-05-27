using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Hamburger recipe", fileName = "Recipes /HamburgerRecipe")]
public class HamburgerRecipe : Recipe
{
    public override List<Ingredient> GetOrderFromRecpie()
    {
        List<Ingredient> orderIngredient = new List<Ingredient>();

        for (int i = 0; i < Ingredients.Count; i++)
        {
            var roll = Random.Range(1, 100);

            if (roll < Ingredients[i].RemoveChance)// Roll will never be 0% or 100% => %0 safe %100 removed
            {
                continue; // skip this ingredient
            }
            else
            {
                orderIngredient.Add(Ingredients[i]);
            }
        }
        return orderIngredient;
    }

    //private cloneRecpieToTempList
}
