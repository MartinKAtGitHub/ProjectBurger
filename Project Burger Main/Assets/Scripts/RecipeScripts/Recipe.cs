using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the ingredients and order for all food types
/// </summary>
public abstract class Recipe : ScriptableObject
{
    [SerializeField]
    private Sprite _recipeImg;
    [SerializeField]
    private string _recipeName;
    [SerializeField]
    private foodType _foodType;
    /// <summary>
    /// The chance this recipe has to be ordered
    /// </summary>
    [SerializeField]
    private int _orderChance;
    /// <summary>
    /// the sum of order chances. This is the number we will check against the random value to see if this recipe will be chosen 
    /// </summary>
    [SerializeField]
    private int _accumulatedWight;

    [SerializeField]
    private float _recipeCost;


    private int _reputationGain;
    private int _reputationLose;

    private enum foodType
    {
        NotDefined,
        Hamburger,
        IceCream,
        Pizza
    }

    /// <summary>
    /// Ingredients that go into this recipe
    /// </summary>
    public List<Ingredient> Ingredients;
    /// <summary>
    /// The sprite that shows the finished recipe product
    /// </summary>
    public Sprite RecipeImg { get => _recipeImg; }
    public string RecipeName { get => _recipeName; }
    public int OrderChance { get => _orderChance; }
    public int AccumulatedWight { get => _accumulatedWight; set => _accumulatedWight = value; }

    public float RecipeCost {//If The Recipe Have No Cost, Then It Will Give It A New Cost. So It Can Be Overrided If You Want To Give A Recipe A Spesific Cost And Not Just Ingredient Cost.
        get => _recipeCost;
        set  {
            if (_recipeCost != 0) {
                _recipeCost = value;
            }
        }
    }

}
