using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains all the data points needed to manipulate OrderWindow GameObject. 
/// </summary>
public class OrderWindowData : MonoBehaviour
{
    /// <summary>
    /// text object dedicated to holding the name of the recipe
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _recipeNameText;
    /// <summary>
    /// Image object which holds the finished food image of a recipe
    /// </summary>
    [SerializeField]
    private Image _recipeImg;
    /// <summary>
    /// Array of Image objects dedicated to displaying discarded ingredients
    /// </summary>
    [SerializeField]
    private Image[] _discardedIngredients;


    public TextMeshProUGUI RecipeNameText { get { return _recipeNameText; } }
    public Image RecipeImg { get { return _recipeImg; } }
    public Image[] DiscardedIngredients { get { return _discardedIngredients; } }
}
