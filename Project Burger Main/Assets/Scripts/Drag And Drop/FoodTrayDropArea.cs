using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Holds the food order that is ready to be sold
/// </summary>
public class FoodTrayDropArea : DropArea
{
    [SerializeField]
    private Order _order; // Get order from Customer
    [SerializeField]
    private OrderGenerator orderGenerator;

    private List<FoodStack> _foodStacks = new List<FoodStack>();
    /// <summary>
    /// The current order being processed
    /// </summary>
    public Order Order { get; set; }

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
            // if(_order.OrderRecipes.count ==  foodstacks.count )
            //  CheckFoodStacksAgainstOrder();


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
    }

    private void CheckFoodStacksAgainstOrder()
    {


        for (int i = 0; i < _foodStacks.Count; i++) // for every foodstack
        {
            Debug.Log( "This should not decrease " + _order.OrderRecipes.Count);
            //then maybe remove it here _order.OrderRecipes.Count marked as completed or failed
            var tempOrderRecipes = _order.OrderRecipes;
            var ingredientMatchFoundInOrderRecipeIndex = 0; //  

            for (int j = 0; j < _foodStacks[i].FoodStackIngredients.Count; j++) // ingredients in foodstack
            {
                var currentIngredient = _foodStacks[i].FoodStackIngredients[j].ingredient;
                var currentIngredientIndex = j;
               // var goToNextFoodStackIngredient = false;

                for (int k = ingredientMatchFoundInOrderRecipeIndex; k < tempOrderRecipes.Count; k++) // for every type of food customer recipe
                {
                    if (_foodStacks[i].FoodStackIngredients.Count <= tempOrderRecipes[k].OrderIngredients.Count)
                    {
                        if(currentIngredient.IngredientType == tempOrderRecipes[k].OrderIngredients[currentIngredientIndex].IngredientType)
                        {
                            ingredientMatchFoundInOrderRecipeIndex = k;
                            break; // Go to next FoodStack ingredient
                        }
                        else
                        {
                            
                            continue; //cant find ingredient in this recipe go next recipe ps (what happens if this is the last iteration ?)
                        }

                        // FOODSTACK INGREDIENT DIDNT MATCH ANY ORDERRECIPE INGREDIANT MARK FOODSTACK AS FAIL 
                    }
                    else
                    {
                        //food stack holds more ingredients then this order recipe
                        // loop trough the rest of the recipes and see if you cant find one that we can check against
                        continue;
                    }

                    // FOODSTACK holds more ingredients OR Didn't MATCH ANY ORDERRECIPE, so MARK FOODSTACK AS FAIL
                }

               //Check current FoodStack Status (FAIL or SUCCSES) and add it to a result list or somthing
            }
        }
    }
}
