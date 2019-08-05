using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

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
    [SerializeField] private int _gold = 0;
    [SerializeField] private int _goldLost = 0;
    [SerializeField] private int _lifeLost = 0;
    [SerializeField] private float _timeUsed = 0;

    [SerializeField] private int _orderItemPartsWaisted = 0;
    [SerializeField] private int _orderItemPartsUsed = 0;
    [SerializeField] private int _orderItemPartsOverused = 0;

    [SerializeField] private int _customersMade = 0;
    [SerializeField] private int _customersLeave = 0;
    [SerializeField] private int _specialEncounters = 0;

    [SerializeField] private int _itemsOrdered = 0;
    [SerializeField] private int _itemsCompleted = 0;
    [SerializeField] private int _itemsFailed = 0;
    [SerializeField] private int _itemsIgnored = 0;

    [SerializeField] private int _ordersOrdered = 0;
    [SerializeField] private int _ordersCompleted = 0;
    [SerializeField] private int _ordersFailed = 0;
    [SerializeField] private int _ordersIgnored = 0;

    [SerializeField] private int _combosApplied = 0;
    [SerializeField] private int _comboHighest = 0;

    //Tables Swiped? Customers Swiped? 

    #endregion

    #region Properties
    //Just Made A Bunch Of Info We May Need

    /// <summary>
    /// += value.
    /// If We Want To Display How Much Ingredient Are Wasted And The Gold Lost, And Also Gold Gained 
    /// </summary>
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            OnGoldChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public int GoldLost
    {
        get => _goldLost;
        set
        {
            _goldLost = value;
            OnGoldChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public int LifeLost
    {
        get => _lifeLost;
        set
        {
            _lifeLost = value;
            OnLifeChange();
        }
    }

    /// <summary>
    /// += value.
    /// </summary>
    public float TimeUsed
    {
        get => _timeUsed;
        set
        {
            _timeUsed += value;
            OnTimeChange();//This Will Be Called Once A Second. Dont Know If This Is Something I Want Or Not.
        }
    }
    public int OrderItemPartsWaisted { get => _orderItemPartsWaisted; set => _orderItemPartsWaisted += value; }
    public int OrderItemPartsUsed { get => _orderItemPartsUsed; set => _orderItemPartsUsed += value; }
    public int OrderItemPartsOverused { get => _orderItemPartsOverused; set => _orderItemPartsOverused += value; }
    public int CustomersMade { get => _customersMade; set => _customersMade += value; }
    public int CustomersLeave { get => _customersLeave; set => _customersLeave += value; }
    public int SpecialEncounters { get => _specialEncounters; set => _specialEncounters += value; }
    public int ItemsOrdered { get => _itemsOrdered; set => _itemsOrdered += value; }
    public int ItemsCompleted { get => _itemsCompleted; set => _itemsCompleted += value; }
    public int ItemsFailed { get => _itemsFailed; set => _itemsFailed += value; }
    public int ItemsIgnored { get => _itemsIgnored; set => _itemsIgnored += value; }
    public int OrdersOrdered { get => _ordersOrdered; set => _ordersOrdered += value; }
    public int OrdersCompleted { get => _ordersCompleted; set => _ordersCompleted += value; }
    public int OrdersFailed { get => _ordersFailed; set => _ordersFailed += value; }
    public int OrdersIgnored { get => _ordersIgnored; set => _ordersIgnored += value; }

    public int CombosApplied
    {
        get => _combosApplied;
        set
        {

            if (_combosApplied > 0)
            {//Stop Old To Reapply Time
                Debug.Log("stopping");
                StopCoroutine(ComboCountDown(0));
            }

            if (_combosApplied > 30)
            {//Just A Simple ComboTime Setter
                StartCoroutine(ComboCountDown(4));
                //        StartCoroutine(CountDown(Time.time + 30));
            }
            else
            {
                StartCoroutine(ComboCountDown(4));
                //        StartCoroutine(CountDown(Time.time + (60 - _combosApplied)));
            }

            _combosApplied += value;

            if (_combosApplied > _comboHighest)
                _comboHighest = _combosApplied;
        }
    }

    public float ComboHighest { get => _comboHighest; }

    #endregion


    private void Awake()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogWarning("FORGOT TO ADD A LEVEL MANAGER???");
        }
        else
        {
            LevelManager.Instance.ScoreManager = this;
        }

    }

    private IEnumerator ComboCountDown(float CountDown)
    {
        float duration = CountDown; // 3 seconds you can change this 

        while (duration > 0)
        {
            yield return new WaitForSeconds(1);
            duration--;
        }

        _combosApplied = 0;
        yield return null;
    }




    //Simple Check If Ingredient Are Correct And Give Score Based On Ingredient Cost, So The More And Expensive Ingredients The More Money Earned.
    public void CalculateScore(Order theOrder, Food theItem)
    {
        for (int i = 0; i < theOrder.OrderRecipes.Count; i++)
        {//Going Through All Recipes Connected To The Order

            for (int j = 0; j < theOrder.OrderRecipes[i].OrderIngredients.Count; j++)
            {//Iterating Through The Ingredient Of The Selected Recipe

                if (theOrder.OrderRecipes[i].OrderIngredients[j] == theItem.IngredientsGO[j])
                {//If Recipe And Checking Item Have Same Ingredients Continue, If Not Check Next Recipe

                    if (j == theOrder.OrderRecipes[i].OrderIngredients.Count - 1)
                    {//Found A Match, And At The Last Ingredient

                        for (int k = 0; k < theOrder.OrderRecipes[i].OrderIngredients.Count; k++)
                        {
                            Gold += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost; // TODO scoremanager.cs |wont this be wrong with the property also doing +=
                            //      GoldEarned += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost * (1 + (0.01f * (CombosApplied = 1)));
                        }

                        OrdersCompleted++;
                        return;//Only 1 Item Is Checked Currently

                    }
                }
                else
                {
                    break;
                }
            }
        }

        _combosApplied = 0;
        _ordersFailed++;
        //Send Info To "Happiness" Display, To Show If Combo Increase And Money Gained When Customer Exiting Building
    }


    public void AddScore(int orderPrice)
    {
        Gold += orderPrice;
        Debug.Log($"Score is ={Gold}");
    }

    public void RemoveLife()
    {
        LifeLost++; // This feels wrong. i feel this should just be life-- and lifelost++. Lifelost feels like a counter representing how many times a player has lost a life not the actual life lost
    }


    //TODO.. IN PROGRESS, Might Not Even Happen
    public void CalculateScoreMultipleItems(Customer customer)
    {
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
