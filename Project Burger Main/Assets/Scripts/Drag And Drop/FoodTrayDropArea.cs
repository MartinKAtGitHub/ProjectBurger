
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Holds the food order that is ready to be sold
/// </summary>
public class FoodTrayDropArea : MonoBehaviour , IDropHandler
{
    public Image ResultImage;
    public Order Order
    {
        get => _order;

        set
        {
            if (value.OrderRecipes.Count == 0)
            {
                //   Debug.Break();
                Debug.LogError("FoodTray order should never be 0");
            }
            _order = value;
        }
    }

    [SerializeField]
    private Order _order; // Get order from Customer

    private OrderGenerator orderGenerator;
    [Space(20)]
    public List<Food> _foods = new List<Food>();
    /// <summary>
    /// The current order being processed
    /// </summary>


    public void OnDrop(PointerEventData eventData)
    {
        if (_order != null)
        {
            var foodDrag = eventData.pointerDrag.GetComponent<FoodDrag>();
            if (foodDrag != null)
            {
                foodDrag.ResetPositionParent = this.transform;

                var food = eventData.pointerDrag.GetComponent<Food>();
                if (food != null)
                {
                    _foods.Add(food);
                }
            }
            else
            {
                Debug.LogWarning("Only food can be dropped on the foodtray");
            }
        }
        else
        {
            Debug.LogError("We don't have the order for the current customer food tray cant check food");
        }
        // you cant pick the food back up
    }

    public void DropAreaOnBeginDrag()
    {
        Debug.Log("Dragging from food tray ");
        _foods.RemoveAt(_foods.Count - 1);
    }

    /// <summary>
    /// The food will sold the moment the amount of foodstacks = to the amount of foodstack the order requers
    /// </summary>
    private void AutoSell()
    {
        if (_order.OrderRecipes.Count == _foods.Count)
        {
            CheckFoodStacksAgainstOrder();
        }
        else
        {
            Debug.Log("Missing rest of the order -> Order recipes(" + _order.OrderRecipes.Count + ") FoodStacks(" + _foods.Count + ")");
        }
    }
    public void CheckFoodStacksAgainstOrder() //TODO CheckFoodStacksAgainstOrder() in foodtray can possibly be optimized 
    {
        var amountOfOrderRecipes = _order.OrderRecipes.Count;
        var amountOffoodStackMatches = 0;

        if (amountOfOrderRecipes == _foods.Count)
        {
            for (int i = 0; i < _foods.Count; i++) // for every foodstack
            {
                //Debug.Log("This should not decrease " + _order.OrderRecipes.Count);
                //then maybe remove it here _order.OrderRecipes.Count marked as completed or failed
                var tempOrderRecipes = _order.OrderRecipes;
                var ingredientMatchFoundInOrderRecipeIndex = 0;
                var failCounter = 0;

                for (int j = 0; j < _foods[i].GameObjectIngredients.Count; j++) // ingredients in foodstack loop
                {
                    var currentIngredient = _foods[i].GameObjectIngredients[j].ingredient;
                    var currentIngredientIndex = j;

                    for (int k = ingredientMatchFoundInOrderRecipeIndex; k < tempOrderRecipes.Count; k++) // Order recipes Loop
                    {
                        if (_foods[i].GameObjectIngredients.Count <= tempOrderRecipes[k].OrderIngredients.Count)
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
                        _foods[i].DidStackMatchOrder = false;
                        break;
                    }
                    else // TODO foodtray optimization,  _foodStacks[i].DidStackMatchOrder = true;
                    {
                        _foods[i].DidStackMatchOrder = true;
                    }
                }
                
                // Next foodStack
                if (!_foods[i].DidStackMatchOrder)
                {
                    Debug.Log("FAIL " + _foods[i].name);
                    continue;
                }
                else
                {
                    _foods[i].DidStackMatchOrder = true;
                    tempOrderRecipes.RemoveAt(ingredientMatchFoundInOrderRecipeIndex);
                    amountOffoodStackMatches++;
                }
            }

            if (amountOffoodStackMatches == amountOfOrderRecipes)
            {
                ResultImage.color = Color.green;
            }
            else
            {
                ResultImage.color = Color.red;
            }

        }
        else
        {
            Debug.Log("Can not sell food, order is not complete, Give player FAIL for selling to early ?");
        }

    }
}
