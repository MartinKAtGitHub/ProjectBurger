using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the recipes in the game
/// </summary>
[CreateAssetMenu(menuName = "RecipeBook", fileName = "RecipeBook")]
public class RecipeBook : ScriptableObject  // Static ? we would really never need to have multiple recipeBook
{
    private int _totalAccumulatedWight = 0;

    public List<Recipe> Recipes; //TODO Maybe A list for each section (Hamburg, sandwich, whatever)

    public int totalAccumulatedWight { get => _totalAccumulatedWight;}
    public void OnEnable()
    {
        CalulateWeightsForRecipes();
    }

    private void CalulateWeightsForRecipes()
    {
        if(Recipes != null)
        {
            for (int i = 0; i < Recipes.Count; i++)
            {
                _totalAccumulatedWight += Recipes[i].OrderChance;
                Recipes[i].AccumulatedWight = _totalAccumulatedWight;

                Debug.Log(Recipes[i].RecipeName +" Weight is : " + _totalAccumulatedWight);
            }
        }
        else
        {
            Debug.LogError("Cant calulate weight becuse the recipebook list is empty");
        }
    }
}
