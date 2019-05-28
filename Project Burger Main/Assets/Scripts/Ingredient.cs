using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient", fileName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private Sprite _ingredientSprite;
    [SerializeField]
    private string _ingredientName;
    public enum IngredientTypes
    {
        NotDefined, 

        HamBurger_BottomBun,
        HamBurger_Meat,
        HamBurger_Cheese,
        HamBurger_Lettuce,
        HamBurger_Onions,
        Hamburger_Pickels,
        Hamburger_TopBun
    }

    [SerializeField]
    IngredientTypes ingredientType = IngredientTypes.NotDefined;

    public IngredientTypes IngredientType
    {
        get
        {
            return ingredientType;
        }
    }
    /// <summary>
    /// The chance of this ingredient not being included in the Order, 
    /// set this in the recipe so we avoid situation where a cheeseburger doesn't have cheese
    /// </summary>
     [Range(0,100)]
    public int RemoveChance = 0;

    public Sprite IngredientSprite { get { return _ingredientSprite; } }
}

