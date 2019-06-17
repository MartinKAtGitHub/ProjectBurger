
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Holds the food order that is ready to be sold AND checks the Food against the order to see if it was correct or not
/// </summary>
public class FoodTrayDropArea : MonoBehaviour, IDropHandler
{
    public Image ResultImage;
    
    [SerializeField]  private Order _order; // Get order from Customer
    [SerializeField] private bool _orderSuccessful;
    //private OrderGenerator orderGenerator;

    public Order Order { get => _order; set => _order = value; }
    public bool OrderSuccessful { get => _orderSuccessful; }

    [Space(20)]
    public List<Food> _foods = new List<Food>();

    

    private void Awake()
    {
        LevelManager.Instance.FoodTrayDropArea = this;
    }
    private void Start()
    {
      
        LevelManager.Instance.SalesManager.OnSale += CheckFoodStacksAgainstOrder;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_order != null)
        {
            var food = eventData.pointerDrag.GetComponent<Food>();
            if (food != null)
            {
                food.FoodDrag.ResetPositionParent = this.transform;
                _foods.Add(food);

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

    //PERFORMANCE Foodtray CheckFoodStacksAgainstOrder() we can check food per Drop instead of all the food at 1 time. check all the results on sell
    public void CheckFoodStacksAgainstOrder() // TODO FoodTray.cs | CheckFoodStacksAgainstOrder() can be moved to another script to make it cleaner 
    {
        var amountOfOrderRecipes = _order.OrderRecipes.Count;
        var amountOffoodStackMatches = 0;

        if(_foods.Count == 0)
        {
            Debug.Log("NO FOOD PLACED ON FOODTRAY !!!!!");
            _orderSuccessful = false;
        }

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
                _orderSuccessful = true;
            }
            else
            {
                ResultImage.color = Color.red;
                _orderSuccessful = false;
            }

        }
        else
        {
            Debug.Log("Can not sell food, Amount of food is not the same in order, Give player FAIL for selling to early ?");
            _orderSuccessful =  false;
        }
    }
}
