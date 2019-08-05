﻿using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient", fileName = "Ingredient")]
public class Ingredient : ScriptableObject
{

    public float IngredientTime = 1;//Used To Calculate Cost Of The Burger/MenuItem

    [SerializeField] private Sprite[] _ingredientLayerSprites = new Sprite[_maxIngredientLayersAmount];
    [Space(10)]
    [SerializeField] private Sprite _ingredientSprite;
    [SerializeField] private string _ingredientName;
    [SerializeField] IngredientTypes ingredientType = IngredientTypes.NotDefined;

    private const int _maxIngredientLayersAmount = 5;
    private int _ingredientCost = 1;//Used To Calculate Cost Of The Burger/MenuItem
    

    public enum IngredientTypes
    {
        NotDefined,

        HamBurger_BottomBun,
        HamBurger_Meat,
        HamBurger_Meat_Raw,
        HamBurger_Meat_Rare,
        HamBurger_Meat_Medium,
        HamBurger_Meat_MediumWell,
        HamBurger_Meat_WellDone,
        HamBurger_Meat_Overcooked,
        HamBurger_Cheese,
        HamBurger_Lettuce,
        HamBurger_Onions,
        Hamburger_Pickels,
        Hamburger_TopBun
    }
    public IngredientTypes IngredientType { get => ingredientType; }
    /// <summary>
    /// The chance of this ingredient not being included in the Order, 
    /// set this in the recipe so we avoid situation where a cheeseburger doesn't have cheese
    /// </summary>
    [Range(0, 100)] public int RemoveChance = 0;

    public Sprite IngredientSprite { get { return _ingredientSprite; } }
    public int IngredientCost { get => _ingredientCost; }
    /// <summary>
    /// How meny ingredients can be stacked on foodCombi or _ingredientLayerSprites.Length.
    /// </summary>
    public static int MaxIngredientLayersAmount => _maxIngredientLayersAmount;
    public Sprite[] IngredientLayerSprites { get => _ingredientLayerSprites; }
   


    /// <summary>
    /// Call this in Start / Awake because a ScriptableObject doesn't have it
    /// </summary>
    public void InitializeIngredient() 
    {
        CheckAllLayersAreCoverd();
    }

    private void CheckAllLayersAreCoverd()
    {
        if(MaxIngredientLayersAmount != _ingredientLayerSprites.Length)
        {
            Debug.LogError("The max amount of layers and the sprites for the ingredient dose not match, check " + name);
        }

        for (int i = 0; i < _ingredientLayerSprites.Length; i++)
        {
            if(_ingredientLayerSprites[i] == null)
            {
                Debug.LogError($"{name} is missing sprites for Ingredient Layer {i} ");
            }
        }
    }
}

