using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    [SerializeField]private Order _order;
    /// <summary>
    /// Food items placed on the foodtray;
    /// </summary>
    private List<Food> _foodItemsOnTray = new List<Food>();
    

    public List<Food> FoodItemsOnTray { get => _foodItemsOnTray;}
    public Order Order { get => _order; }

    private void Awake()
    {
        LevelManager.Instance.TESTING_FoodTray = this;
    }
  
    public bool CheckFoodStacksAgainstOrder(Order order) 
    {
       // _order = order;
        var amountOfOrderRecipes = order.OrderRecipes.Count;
        var amountOffoodStackMatches = 0;

        //if (order != null) // this should technically never happen
        {
            if (_foodItemsOnTray.Count == 0)
            {
                Debug.Log("NO FOOD PLACED ON FOODTRAY !!!!!");
                //OnFailOrder();
                //return;
                return false;
            }

            if (amountOfOrderRecipes == _foodItemsOnTray.Count)
            {
                for (int i = 0; i < _foodItemsOnTray.Count; i++) // for every foodstack
                {
                    //Debug.Log("This should not decrease " + _order.OrderRecipes.Count);

                    var tempOrderRecipes = order.OrderRecipes;
                    var ingredientMatchFoundInOrderRecipeIndex = 0;
                    var failCounter = 0;

                    for (int j = 0; j < _foodItemsOnTray[i].IngredientsGO.Count; j++) // ingredients in foodstack loop
                    {
                        var currentIngredient = _foodItemsOnTray[i].IngredientsGO[j].Ingredient;
                        var currentIngredientIndex = j;

                        for (int k = ingredientMatchFoundInOrderRecipeIndex; k < tempOrderRecipes.Count; k++) // Order recipes Loop
                        {
                            if (_foodItemsOnTray[i].IngredientsGO.Count <= tempOrderRecipes[k].OrderIngredients.Count)
                            {
                                if (currentIngredient.IngredientType == tempOrderRecipes[k].OrderIngredients[currentIngredientIndex].IngredientType)
                                {
                                    // Debug.Log("Match found ! | " + _foodStacks[i].name + "(" + currentIngredient.IngredientType + ") == (" + tempOrderRecipes[k].OrderIngredients[currentIngredientIndex].IngredientType + ")");
                                    ingredientMatchFoundInOrderRecipeIndex = k;
                                    break; // Go to next FoodStack ingredient
                                }
                                else
                                {
                                    // Foodstack ingredient didn't match this order recipe ingredient
                                    failCounter++;
                                }
                            }
                            else
                            {
                                // Foodstack is to big for this OrderRecipe[k]
                                failCounter++;
                            }
                        }

                        // Next Foodstack ingredient
                        if (failCounter == tempOrderRecipes.Count)
                        {
                            // The ingredient didn't match any order recipe ingredient so go to next foodstack
                            _foodItemsOnTray[i].DidStackMatchOrder = false;
                            // If 1 foodstack fails do we fail the whole thing or give half points ?
                            // Beacuse we dont need to loop trhoug the whole thing if we fail here
                            break;
                        }
                        else // TODO foodtray optimization,  _foodStacks[i].DidStackMatchOrder = true;
                        {
                            _foodItemsOnTray[i].DidStackMatchOrder = true;
                        }
                    }

                    // Check this foodStack Fail or success
                    if (!_foodItemsOnTray[i].DidStackMatchOrder)
                    {
                        Debug.Log("FAIL " + _foodItemsOnTray[i].name);
                        continue;
                    }
                    else
                    {
                        _foodItemsOnTray[i].DidStackMatchOrder = true;
                        tempOrderRecipes.RemoveAt(ingredientMatchFoundInOrderRecipeIndex);
                        amountOffoodStackMatches++;
                    }
                }

                if (amountOffoodStackMatches == amountOfOrderRecipes)
                {
                   // OnSuccessfulOrder();
                    return true;
                }
                else
                {
                   // OnFailOrder();
                    return false;
                }
            }
            else
            {
                Debug.Log("Can not sell food, Amount of food is not the same in order, Give player FAIL for selling to early ?");
                //OnFailOrder();
                return false;
            }
        }
        //else
        //{
        //    Debug.LogError("Order = null, Cant check food. This should never happen. If the customer doesn't have a ORDER this method should never be called");
            
        //}
    }

    //private void OnSuccessfulOrder()
    //{
    //    //_orderSuccessful = true;
    //    RemoveFoodFromGame();
    //    LevelManager.Instance.ScoreManager.AddScore(order.PriceTotal);
    //}

    //private void OnFailOrder()
    //{
    //    //_orderSuccessful = false;
    //    RemoveFoodFromGame();
    //    LevelManager.Instance.ScoreManager.RemoveLife();
    //}

    //private void RemoveFoodFromGame()
    //{
    //    for (int i = 0; i < _foodItemsOnTray.Count; i++)
    //    {
    //        var food = _foodItemsOnTray[i];
    //        _foodItemsOnTray.RemoveAt(i);
    //        food.RemoveFromGame();
    //    }
    //}


}
