using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meny : MonoBehaviour {

    public Recipe[] OldRecipes;
    public Recipe[] NewRecipes;

    [HideInInspector]
   public MenuItems[] _OldRecipesHolder;
    [HideInInspector]
    public MenuItems[] _NewRecipesHolder;
    public MenuItems[] _AllRecipesHolder;

    [HideInInspector]
    public MenuItems[] RecipesGoldSorted;
    [HideInInspector]
    public MenuItems[] RecipesTimeSorted;

   public void SetItems(float GoldToGet, float TimeLimit) {
        _AllRecipesHolder = new MenuItems[OldRecipes.Length + NewRecipes.Length];
        RecipesGoldSorted = new MenuItems[_AllRecipesHolder.Length];
        RecipesTimeSorted = new MenuItems[_AllRecipesHolder.Length];

        _OldRecipesHolder = new MenuItems[OldRecipes.Length];
        for (int i = 0; i < OldRecipes.Length; i++) {
            _OldRecipesHolder[i] = new MenuItems();

            _OldRecipesHolder[i].cost = OldRecipes[i].Price;
            _OldRecipesHolder[i].theTime = OldRecipes[i].RecipeTime;
            _OldRecipesHolder[i].percentageCostGold = _OldRecipesHolder[i].cost / GoldToGet;
            _OldRecipesHolder[i].percentageCostTime = _OldRecipesHolder[i].theTime / TimeLimit;
            _OldRecipesHolder[i].theRecipe = OldRecipes[i];
            _OldRecipesHolder[i].used = 0;
            _AllRecipesHolder[i] = _OldRecipesHolder[i];
        }

        _NewRecipesHolder = new MenuItems[NewRecipes.Length];
        for (int i = 0; i < NewRecipes.Length; i++) {
            _NewRecipesHolder[i] = new MenuItems();
        
            _NewRecipesHolder[i].cost = NewRecipes[i].Price;
            _NewRecipesHolder[i].theTime = NewRecipes[i].RecipeTime;
            _NewRecipesHolder[i].percentageCostGold = _NewRecipesHolder[i].cost / GoldToGet;
            _NewRecipesHolder[i].percentageCostTime = _NewRecipesHolder[i].theTime / TimeLimit;
            _NewRecipesHolder[i].theRecipe = NewRecipes[i];
            _NewRecipesHolder[i].used = 0;
            _AllRecipesHolder[(OldRecipes.Length) + i] = _NewRecipesHolder[i];
        }

        OrderSortPercentageGold();
        OrderSortPercentageTime();

    }


    void OrderSortPercentageGold() {
        MenuItems saver;
        float average = 0;//Not Realy Sure Why This Shows As Never Used....
        int position = 0;

        for (int i = 0; i < _AllRecipesHolder.Length; i++) {
            average = _AllRecipesHolder[i].percentageCostGold;
            position = i;

            for (int j = i + 1; j < _AllRecipesHolder.Length; j++) {

                if (_AllRecipesHolder[j].percentageCostGold < average) {
                    average = _AllRecipesHolder[j].percentageCostGold;
                    position = j;
                }
            }
            saver = _AllRecipesHolder[i];
            RecipesGoldSorted[i] = _AllRecipesHolder[position];
            _AllRecipesHolder[i] = _AllRecipesHolder[position];
            _AllRecipesHolder[position] = saver;

        }
    }

    void OrderSortPercentageTime() {
        MenuItems saver;
        float average = 0;//Not Realy Sure Why This Shows As Never Used....
        int position = 0;

        for (int i = 0; i < _AllRecipesHolder.Length; i++) {
            average = _AllRecipesHolder[i].percentageCostTime;
            position = i;

            for (int j = i + 1; j < _AllRecipesHolder.Length; j++) {

                if (_AllRecipesHolder[j].percentageCostTime < average) {
                    average = _AllRecipesHolder[j].percentageCostTime;
                    position = j;
                }
            }
            saver = _AllRecipesHolder[i];
            RecipesTimeSorted[i] = _AllRecipesHolder[position];
            _AllRecipesHolder[i] = _AllRecipesHolder[position];
            _AllRecipesHolder[position] = saver;

        }
    }

}


[System.Serializable]
public class MenuItems {

    public float cost;
    public float theTime;
    public float percentageCostGold;
    public float percentageCostTime;

    public Recipe theRecipe;
    public int used;


}

public enum Recipess {doubledeckerlettuce, doubledecker, hamburger, lettuceCarnivores, meatlovers, bloodydelight};

// burger meat  -> 50 price, time 25
// lettuce      -> 1  price, time 2.5
// buns         -> 2  price, time 2
//
//