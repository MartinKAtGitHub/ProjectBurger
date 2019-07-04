using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    private float _Money = 0;
    [SerializeField]
    private int _Mistakes = 0;
    [SerializeField]
    private float _Combo = 0;

    private float _ComboTime = 0;


    public float Money {
        get { return _Money; }
        set { _Money = value; }
    }

    public int Mistakes {
        get { return _Mistakes; }
     private set { _Mistakes = value; }
    }
    public float Combo {
        get { return _Combo; }
        private set { _Combo = value; }
    }



    private void Start() {
        if(LevelManager.Instance == null) {
            Debug.LogWarning("FORGOT TO ADD A LEVEL MANAGER???");
        } else {
            LevelManager.Instance.ScoreManager = this;
        }
    }

    private void Update() {
        if (_Combo > 0) {

            if(_ComboTime < Time.time) {
                _Combo = 0;
            }

        }
    }

    //Hmm Should we have random generated burgers? or should be instead make multiple burgers and just spawn those?
    //If We just spawn a burger from its recipe it much easier to do the check, if its the correct item.
    //+ If we want a cost on the burger itself, then that wont be possible, cuz its rng generated. then the cost will always be based on the ingredients and how much of them there are.



    //Simple Check If Ingredient Are Correct And Give Score Based On Ingredient Cost, So The More And Expensive Ingredients The More Money Earned.
    public void CalculateScore(Order theOrder, Food theItem) {
        for (int i = 0; i < theOrder.OrderRecipes.Count; i++) {//Going Through All Recipes Connected To The Order

            for (int j = 0; j < theOrder.OrderRecipes[i].OrderIngredients.Count; j++) {//Iterating Through The Ingredient Of The Selected Recipe

                if (theOrder.OrderRecipes[i].OrderIngredients[j] == theItem.GameObjectIngredients[j]) {//If Recipe And Checking Item Have Same Ingredients Continue, If Not Check Next Recipe

                    if (j == theOrder.OrderRecipes[i].OrderIngredients.Count - 1) {//Found A Match, And At The Last Ingredient

                        for (int k = 0; k < theOrder.OrderRecipes[i].OrderIngredients.Count; k++) {
                            Money += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost * (1 + (0.01f * ++_Combo));

                            if (_Combo > 30) {//Just A Simple ComboTime Setter
                                _ComboTime = Time.time + 30;
                            } else {
                                _ComboTime = Time.time + (60 - _Combo);
                            }

                        }
                        return;//Only 1 Item Is Checked Currently

                    }
                } else {
                    break;
                }
            }
        }

        _Combo = 0;
        Mistakes++;
        //Send Info To "Happiness" Display, To Show If Combo Increase And Money Gained When Customer Exiting Building
    }



    //IN PROGRESS, Might Not Even Happen
    public void CalculateScoreMultipleItems(Order theOrder, Food theItem) {

        for (int i = 0; i < theOrder.OrderRecipes.Count; i++) {//Going Through All Recipes Connected To The Order

            for (int j = 0; j < theOrder.OrderRecipes[i].OrderIngredients.Count; j++) {//Iterating Through The Ingredient Of The Selected Recipe

                if (theOrder.OrderRecipes[i].OrderIngredients[j] == theItem.GameObjectIngredients[j]) {//If Recipe And Checking Item Have Same Ingredients Continue, If Not Check Next Recipe

                    if (j == theOrder.OrderRecipes[i].OrderIngredients.Count - 1) {//Found A Match, And At The Last Ingredient

                        for (int k = 0; k < theOrder.OrderRecipes[i].OrderIngredients.Count; k++) {
                            Money += theOrder.OrderRecipes[i].OrderIngredients[k].IngredientCost * (1 + (0.01f * ++_Combo));

                            if (_Combo > 30) {//Just A Simple ComboTime Setter
                                _ComboTime = Time.time + 30;
                            } else {
                                _ComboTime = Time.time + (60 - _Combo);
                            }

                        }
                        return;//Only 1 Item Is Checked Currently

                    }
                } else {
                    break;
                }
            }
        }

        _Combo = 0;
        Mistakes++;
        //Send Info To "Happiness" Display, To Show If Combo Increase And Money Gained When Customer Exiting Building
    }




}
