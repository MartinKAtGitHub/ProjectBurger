using UnityEngine;

public class Ingredient: MonoBehaviour
{
    public  enum IngredientTypes
    {
        NotDefined, // Use this to check for errors

        HamBurger_BottomBun,
        HamBurger_Meat,
        HamBurger_Cheese,
        HamBurger_Lettuce,
        HamBurger_Onions,
        Hamburger_TopBun

    }

    [SerializeField]
    IngredientTypes ingredientType = IngredientTypes.NotDefined;

       
}

