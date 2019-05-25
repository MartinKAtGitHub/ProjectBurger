using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum IngredientTypes
    {
        NotDefined, // Use this to check for errors

        HamBurger_BottomBun,
        HamBurger_Meat,
        HamBurger_Cheese,
        HamBurger_Lettuce,
        HamBurger_Onions,
        Hamburger_TopBun,

        Sandwich_BotBread,
        Sandwich_Cheese,
        Sandwich_Lettuce,
        Sandwich_Onions,
        Sandwich_TopBread
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

}

