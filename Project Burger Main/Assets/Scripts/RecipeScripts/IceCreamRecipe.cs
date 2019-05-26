using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ice cream recipe", fileName = "Recipes/IceCreamRecipe")]
public class IceCreamRecipe : Recipe
{
    public override List<Ingredient> GetOrderFromRecpie()
    {
        throw new System.NotImplementedException();
    }
}
