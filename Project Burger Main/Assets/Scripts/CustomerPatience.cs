using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerPatience
{

    [SerializeField] private float _customerWaitingTime;
    [SerializeField] [Range(0,1)]private float _queueSizeTimeGainModifer;
   // [SerializeField] private float _customerGold;//Currently Just For Testing/Display

    private float RecipeTime = 0;
    private float RecipeOrderTime = 0;


    public float CustomerWaitingTime{ get =>  _customerWaitingTime; }
    
    

    public void SetOrderPatience(Order _order)
    {

        for (int i = 0; i < _order.OrderRecipes.Count; i++)
        {//Setting Time Based On RecipeTimer
            _customerWaitingTime += _order.OrderRecipes[i].BaseRecipe.RecipeTime;
            RecipeTime = 0;
            RecipeOrderTime = 0;

            for (int j = 0; j < _order.OrderRecipes[i].BaseRecipe.Ingredients.Count; j++)
            {
                RecipeOrderTime += _order.OrderRecipes[i].BaseRecipe.Ingredients[j].IngredientTime;
            }

            for (int j = 0; j < _order.OrderRecipes[i].OrderIngredients.Count; j++)
            {
                RecipeTime += _order.OrderRecipes[i].OrderIngredients[j].IngredientTime;
            }
            _customerWaitingTime -= (RecipeOrderTime - RecipeTime);
        }


        AddQueuePatience();//Should Not Be Added Here, But At The Spawner, But For Testing It Was Placed Here, It Could Be Here If You Want The First In Line To Have Highest Priority, Then 2nd Customer And Then 3rd. But If You Want All To Get More
                           //Then All Customers Need To Get Added Some Bonus Time
    }

    private void AddQueuePatience()
    {

        //Aditional Check Can Be Made Here, Like Check If The Other Customers Time Is Almost Out, Or Their Order Is Almost Complete, Then Dont Give As Much Patience Time
        _customerWaitingTime *= 1 + (_queueSizeTimeGainModifer * LevelManager.Instance.QueueManager.QueueSlots.Length);//Just Adding 10% For Every Customer In The Queue

    }

    public void AddQueuePatienceTime(float time)
    {//If We Spawn Something That We Want To Make The Player Focus On, Then That Object Can Add Some Extra Waiting Time, Like If A Music Group Plays Or A Scene (A FoodFight Or Brawl Between Two Dudes)

        //Additional Check Can Be Made Here, Like Check If The Other Customers Time Is Almost Out, Or Their Order Is Almost Complete, Then Dont Give As Much Patience Time
        _customerWaitingTime *= 1 + (_queueSizeTimeGainModifer * LevelManager.Instance.QueueManager.QueueSlots.Length);//Just Adding 10% For Every Customer In The Queue

    }


}
