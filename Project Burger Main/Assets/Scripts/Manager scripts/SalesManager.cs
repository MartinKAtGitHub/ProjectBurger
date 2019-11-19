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

    public void OnSell()
    {
      
        var queueSlot = LevelManager.Instance.CustomerSelectSwiper.QueueSlotInFocus;

        if (queueSlot.CurrentCustomer != null)
        {
           // Check the customer order TO foodtray !!!!!!!!!!!!!
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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSell();
    }
}
