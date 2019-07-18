using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour { 

    #region EventsAndDelegates

    public delegate void GoldChange();
    public static event GoldChange OnGoldChange;

    public delegate void TimeChange();
    public static event TimeChange OnTimeChange;

    public delegate void LifeChange();
    public static event LifeChange OnLifeChange;

    #endregion

    #region ScoreStats

    //Serializing For Testing
    [SerializeField] private float _goldEarned = 0;
    [SerializeField] private float _goldLost = 0;
    [SerializeField] private float _lifeLost = 0;
    [SerializeField] private float _timeUsed = 0;

    [SerializeField] private float _orderItemPartsWaisted = 0;
    [SerializeField] private float _orderItemPartsUsed = 0;
    [SerializeField] private float _orderItemPartsOverused = 0;

    [SerializeField] private float _customersMade = 0;
    [SerializeField] private float _customersLeave = 0;
    [SerializeField] private float _specialEncounters = 0;

    [SerializeField] private float _itemsOrdered = 0;
    [SerializeField] private float _itemsCompleted = 0;
    [SerializeField] private float _itemsFailed = 0;
    [SerializeField] private float _itemsIgnored = 0;

    [SerializeField] private float _ordersOrdered = 0;
    [SerializeField] private float _ordersCompleted = 0;
    [SerializeField] private float _ordersFailed = 0;
    [SerializeField] private float _ordersIgnored = 0;

    [SerializeField] private float _combosApplied = 0;
    [SerializeField] private float _comboHighest = 0;

    //Tables Swiped? Customers Swiped? 

    #endregion

    #region Properties
    //Just Made A Bunch Of Info We May Need

    /// <summary>
    /// += value.
    /// If We Want To Display How Much Ingredient Are Wasted And The Gold Lost, And Also Gold Gained 
    /// </summary>
    public float GoldEarned {
        get => _goldEarned;
        set {
            _goldEarned += value;
            OnGoldChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public float GoldLost {
        get => _goldLost;
        set {
            _goldLost += value;
            OnGoldChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public float LifeLost {
        get => _lifeLost;
        set {
            _lifeLost += value;
            OnLifeChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public float TimeUsed {
        get => _timeUsed;
        set {
            _timeUsed += value;
            OnTimeChange();//This Will Be Called Once A Second. Dont Know If This Is Something I Want Or Not.
        }
    }
    public float OrderItemPartsWaisted { get => _orderItemPartsWaisted; set => _orderItemPartsWaisted += value; }
    public float OrderItemPartsUsed { get => _orderItemPartsUsed; set => _orderItemPartsUsed += value; }
    public float OrderItemPartsOverused { get => _orderItemPartsOverused; set => _orderItemPartsOverused += value; }
    public float CustomersMade { get => _customersMade; set => _customersMade += value; }
    public float CustomersLeave { get => _customersLeave; set => _customersLeave += value; }
    public float SpecialEncounters { get => _specialEncounters; set => _specialEncounters += value; }
    public float ItemsOrdered { get => _itemsOrdered; set => _itemsOrdered += value; }
    public float ItemsCompleted { get => _itemsCompleted; set => _itemsCompleted += value; }
    public float ItemsFailed { get => _itemsFailed; set => _itemsFailed += value; }
    public float ItemsIgnored { get => _itemsIgnored; set => _itemsIgnored += value; }
    public float OrdersOrdered { get => _ordersOrdered; set => _ordersOrdered += value; }
    public float OrdersCompleted { get => _ordersCompleted; set => _ordersCompleted += value; }
    public float OrdersFailed { get => _ordersFailed; set => _ordersFailed += value; }
    public float OrdersIgnored { get => _ordersIgnored; set => _ordersIgnored += value; }

    public float CombosApplied {
        get => _combosApplied;
        set {

            if (_combosApplied > 0) {//Stop Old To Reapply Time
                Debug.Log("stopping");
                StopCoroutine(CountDown(0));
            }

            if (_combosApplied > 30) {//Just A Simple ComboTime Setter
                StartCoroutine(CountDown(4));
        //        StartCoroutine(CountDown(Time.time + 30));
            } else {
                StartCoroutine(CountDown(4));
        //        StartCoroutine(CountDown(Time.time + (60 - _combosApplied)));
            }
           
            _combosApplied += value;

            if (_combosApplied > _comboHighest)
                _comboHighest = _combosApplied;
        }
    }

    public float ComboHighest { get => _comboHighest;}

    #endregion


    private void Start() {
        if(LevelManager.Instance == null) {
            Debug.LogWarning("FORGOT TO ADD A LEVEL MANAGER???");
        } else {
            LevelManager.Instance.ScoreManager = this;
        }

    }

    private IEnumerator CountDown(float CountDown) {
        float duration = CountDown; // 3 seconds you can change this 
      
        while (duration > 0) {
            yield return new WaitForSeconds(1);
            duration--;
        }

        _combosApplied = 0;
        yield return null;
    }




    //Simple Check If Ingredient Are Correct And Give Score Based On Ingredient Cost, So The More And Expensive Ingredients The More Money Earned.
    public void CalculateScore(Order theOrder, Food theItem) {
        for (int i = 0; i < theOrder.OrderRecipes.Count; i++) {//Going Through All Recipes Connected To The Order

            for (int j = 0; j < theOrder.OrderRecipes[i].OrderIngredients.Count; j++) {//Iterating Through The Ingredient Of The Selected Recipe

                if (theOrder.OrderRecipes[i].OrderIngredients[j] == theItem.GameObjectIngredients[j]) {//If Recipe And Checking Item Have Same Ingredients Continue, If Not Check Next Recipe

                    if (j == theOrder.OrderRecipes[i].OrderIngredients.Count - 1) {//Found A Match, And At The Last Ingredient

                        for (int k = 0; k < theOrder.OrderRecipes[i].OrderIngredients.Count; k++) {
                            GoldEarned += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost;
                      //      GoldEarned += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost * (1 + (0.01f * (CombosApplied = 1)));
                        }

                        OrdersCompleted++;
                        return;//Only 1 Item Is Checked Currently

                    }
                } else {
                    break;
                }
            }
        }

        _combosApplied = 0;
        _ordersFailed++;
        //Send Info To "Happiness" Display, To Show If Combo Increase And Money Gained When Customer Exiting Building
    }



    //TODO.. IN PROGRESS, Might Not Even Happen
    public void CalculateScoreMultipleItems(Customer customer) {
        //float addingMoney = 0;
        //float foodScore = 0;
        //float foodScoreDiff = 0;

        //for (int i = 0; i < customer.Order.OrderRecipes.Count; i++) {//Setting Cost Based On RecipeTimer
        //    addingMoney += customer.Order.OrderRecipes[i].BaseRecipe.RecipeCost;
        //    foodScoreDiff = 0;
        //    foodScore = 0;

        //    for (int j = 0; j < customer.Order.OrderRecipes[i].BaseRecipe.Ingredients.Count; j++) {//Recipe Cost
        //        foodScore += customer.Order.OrderRecipes[i].BaseRecipe.Ingredients[j].IngredientCost;
        //    }

        //    for (int j = 0; j < customer.Order.OrderRecipes[i].OrderIngredients.Count; j++) {//Order Cost
        //        foodScoreDiff += customer.Order.OrderRecipes[i].OrderIngredients[j].IngredientCost;
        //    }
        //    addingMoney -= (foodScore - foodScoreDiff);
        //}

        //_Money += addingMoney;


        //_Combo = 0;
        //Mistakes++;

    }




}
