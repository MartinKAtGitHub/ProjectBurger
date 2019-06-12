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
        CalulateWeightsForRecipes(); // Find out a way to calculate on Add/Remove recipe (Costume editor)
        Debug.Log("Weight is being calculated in ScriptableObject Awake(), We only need to do this once!");
    }

    private void CalulateWeightsForRecipes()
    {
        _totalAccumulatedWight = 0;

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
            Debug.LogError("Cant calculate weight because the recipe book list is empty");
        }
    }

    public void InitializeRecipeBook()
    {
        CalulateWeightsForRecipes();
    }
}
