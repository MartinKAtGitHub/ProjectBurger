﻿
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Holds the food order that is ready to be sold
/// </summary>
public class FoodTrayDropArea : DropArea
{
    public Image ResultImage;

    public Order TESTORDER;

    private Order _order; // Get order from Customer
    private OrderGenerator orderGenerator;
    [Space(20)]
    public List<FoodStack> _foodStacks = new List<FoodStack>();
    /// <summary>
    /// The current order being processed
    /// </summary>
    public Order Order { get; set; }


    private void Start()
    {
        _order = TESTORDER;
        Debug.Log("!!!! TESTORDER IS ENABLED !!!!");
    }

    public override void DropAreaOnBeginDrag()
    {
        Debug.Log("Dragging from food tray ");
        _foodStacks.RemoveAt(_foodStacks.Count - 1);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        // base.OnDrop(eventData);
        if (_order != null)
        {
            AddFoodStack(eventData.pointerDrag.GetComponent<FoodStack>());
            if (_order.OrderRecipes.Count == _foodStacks.Count)
            {
                CheckFoodStacksAgainstOrder();
            }
            else
            {
                Debug.Log("Missing rest of the order -> Order recipes(" + _order.OrderRecipes.Count + ") FoodStacks(" + _foodStacks.Count + ")");
            }


        }
        else
        {
            Debug.LogError("We don't have the order for the current customer food tray cant check food");
        }
        // you cant pick the food back up
    }


    private void AutoSell()
    {
        // if(_order.OrderRecipes.count ==  foodstacks.count )
        // then sell the food
    }

    /// <summary>
    /// We add a food stack to a List(food stacks) so we can check to see when we have the whole order.
    /// </summary>
    /// <param name="foodStack"> the currently dragged and dropped food stack gameobject </param>
    private void AddFoodStack(FoodStack foodStack)
    {
        if (foodStack != null)
        {
            _foodStacks.Add(foodStack);
        }
        else
        {
            Debug.LogError("Something else was dropped on the FoodTray. Only a food stack can be on the food tray");
        }
    }

    public void CheckFoodStacksAgainstOrder()
    {
        var amountOfOrderRecipes = _order.OrderRecipes.Count;
        var amountOffoodStackMatches = 0;

        for (int i = 0; i < _foodStacks.Count; i++) // for every foodstack
        {
            //Debug.Log("This should not decrease " + _order.OrderRecipes.Count);
            //then maybe remove it here _order.OrderRecipes.Count marked as completed or failed
            var tempOrderRecipes = _order.OrderRecipes;
            var ingredientMatchFoundInOrderRecipeIndex = 0;
            var failCounter = 0;

            for (int j = 0; j < _foodStacks[i].GameObjectIngredients.Count; j++) // ingredients in foodstack loop
            {
                var currentIngredient = _foodStacks[i].GameObjectIngredients[j].ingredient;
                var currentIngredientIndex = j;


                for (int k = ingredientMatchFoundInOrderRecipeIndex; k < tempOrderRecipes.Count; k++) // Order recipes Loop
                {
                    if (_foodStacks[i].GameObjectIngredients.Count <= tempOrderRecipes[k].OrderIngredients.Count)
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

                    // Debug.Log("FOOD: " + _foodStacks[i].name + "  Fail count: " + failCounter);
                }

                // Next Foodstack ingredient
                if (failCounter == tempOrderRecipes.Count)
                {
                    // The ingredient didn't match any order recipe ingredient so go to next foodstack
                    _foodStacks[i].DidStackMatchOrder = false;
                    break;
                }
                else // TODO foodtray optimization,  _foodStacks[i].DidStackMatchOrder = true;
                {
                    _foodStacks[i].DidStackMatchOrder = true;
                }

                //Debug.Log("END OF " + _foodStacks[i].name + " Ingredients");

            }
            // Next foodStack

            if (!_foodStacks[i].DidStackMatchOrder)
            {
                Debug.Log("FAIL " + _foodStacks[i].name);
                continue;
            }
            else
            {
                _foodStacks[i].DidStackMatchOrder = true;
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
}