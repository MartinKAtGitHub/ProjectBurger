using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class SalesManager : MonoBehaviour, IPointerClickHandler
{

    private Action _onSale;
    public Action OnSale { get => _onSale; set => _onSale = value; }

    private void Awake()
    {
        LevelManager.Instance.SalesManager = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSell();
    }
    public void OnSell()
    {
      
        var queueSlot = LevelManager.Instance.CustomerSelectSwiper.QueueSlotInFocus;

        
        if (queueSlot.CurrentCustomer != null) //if (!_customerSelect.InSmoothTransition might need this)
        {
            var customer = queueSlot.CurrentCustomer;

            if (customer.Order != null)
            {
                // Check the customer order TO foodtray !!!!!!!!!!!!!
                var orderSuccses = LevelManager.Instance.TESTING_FoodTray.CheckFoodStacksAgainstOrder(customer.Order);

                if(orderSuccses)
                {
                    Debug.Log("ORDER WAS SUCCSESFULL");
                }
                else
                {
                    Debug.Log("ORDER FAILED");
                }


            }
            else
            {
                // This will only happen in case the customer is still decide what to order and the player wants to sell something to this person.
                Debug.Log(customer.name + " Has no Order but you are trying to sell. GIVEPLAYER FEEDBACK / WARNING FOR THIS");
            }




        }
        else
        {
            Debug.Log(queueSlot.name + " = empty | cant sell to empty slot. Play/show warning ");
        }

        
        //if (customer != null)
        //{
        //    if (!_customerSelect.InSmoothTransition)
        //    {
        //        LevelManager.Instance.FoodTray.CheckFoodStacksAgainstOrder();

        //       // _customerSelect.OnSell();
        //        _queueManager.RemoveCustomerFromQueue(customer);
        //    }
        //}
        //else
        //{
        //    Debug.Log("No customer too sell to");
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
