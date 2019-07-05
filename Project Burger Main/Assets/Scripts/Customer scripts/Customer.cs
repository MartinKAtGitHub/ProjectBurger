using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour {

    [SerializeField] private float _customerWaitingTime;
    [SerializeField] private float _customerGold;//Currently Just For Testing/Display
    public float CustomerGold {
        get {
            return _customerGold;
        }
    }
    public float CustomerWaitingTime {
        get {
            return _customerWaitingTime;
        }
    }



    [SerializeField] private string _customerName;
    public string CustomerName { get => _customerName; }

    private Order _order;
    public Order Order {
        get {
            return _order;
        }
    }


    [SerializeField]
    private OrderGenerator _OrderGenerator = null;
    public OrderGenerator OrderGenerator {
        get {
            return _OrderGenerator;
        }
    }

    [SerializeField]
    private CustomerState _CustomerStates = null;
    public CustomerState CustomerStates {
        get {
            return _CustomerStates;
        }
    } 

    private void OnDestroy() {
        //Destoyed

        //TODO On Exit Shop, Give happiness Symbol?? 
        //This Should Be On Sell Button, But Later.
        LevelManager.Instance.ScoreManager.CalculateScoreMultipleItems(this);
    }

    private void OnEnable() 
    {
        if (_order == null) {
            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            _order.CustomerName = _customerName;
        }
    }

    float RecipeTime = 0;
    float RecipeOrderTime = 0;

    private void Start() {
        //Start Walk
        //Stop At Disc
        //Start Dialog;

        _CustomerStates.StartCustomerStates();


        for (int i = 0; i < _order.OrderRecipes.Count; i++) {//Setting Time Based On RecipeTimer
            _customerWaitingTime += _order.OrderRecipes[i].BaseRecipe.RecipeTime;
            RecipeTime = 0;
            RecipeOrderTime = 0;

            for (int j = 0; j < _order.OrderRecipes[i].BaseRecipe.Ingredients.Count; j++) {
                RecipeOrderTime += _order.OrderRecipes[i].BaseRecipe.Ingredients[j].IngredientTime;
            }

            for (int j = 0; j < _order.OrderRecipes[i].OrderIngredients.Count; j++) {
                RecipeTime += _order.OrderRecipes[i].OrderIngredients[j].IngredientTime;
            }
            _customerWaitingTime -= (RecipeOrderTime - RecipeTime);
        }

        for (int i = 0; i < _order.OrderRecipes.Count; i++) {//Setting Cost Based On RecipeTimer
            _customerGold += _order.OrderRecipes[i].BaseRecipe.RecipeCost;
            RecipeTime = 0;
            RecipeOrderTime = 0;

            for (int j = 0; j < _order.OrderRecipes[i].BaseRecipe.Ingredients.Count; j++) {
                RecipeOrderTime += _order.OrderRecipes[i].BaseRecipe.Ingredients[j].IngredientCost;
            }

            for (int j = 0; j < _order.OrderRecipes[i].OrderIngredients.Count; j++) {
                RecipeTime += _order.OrderRecipes[i].OrderIngredients[j].IngredientCost;
            }
            _customerGold -= (RecipeOrderTime - RecipeTime);
        }



    }


    void SetWaitingTime() {

    }



    public void SetCustomerStates(TheCustomSpawner spawner) {
     /*   TransformArray2 Paths = spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups[Random.Range(0, spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups.Length)];
        _CustomerStates.MakeNewInstance(Paths.PathInGroup.Length);

        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;


        for (int i = 0; i < Paths.PathInGroup.Length; i++) {
            _CustomerStates.SetBehaviours(
           Paths.PathInGroup[i].PositionsInPath , Paths.PathInGroup[i].Talking, _order.OrderRecipes.Count * 25f * Paths.PathInGroup[i].Patience);
        }

    */

    }
    
}
