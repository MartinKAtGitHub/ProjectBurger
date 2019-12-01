using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{

    // [SerializeField] private int _tmpMax = 6; // TODO  OrderWindow.cs | Connect Max limit pnls/ fooditems per customer to Manager
    //[SerializeField] private GameObject _foodItemPnlPrefab;
    /// <summary>
    /// Holds the pnls with the information on fooditem which are not in focus from the player.
    /// </summary>
    // [SerializeField] private RectTransform _notInFocusPnls;
    [SerializeField]private GameObject _requestCardsContainerPrefab;

    //  private Customer _activeCustomer;
    private OrderWindowSwiper _orderWindowSwiper;
   // private RequestContainer[] _requestContainers;
    //public Customer ActiveCustomer { get => _activeCustomer; }
    //public RectTransform NotInFocusPnls { get => _notInFocusPnls; }
    //public GameObject FoodItemPnlPrefab { get => _foodItemPnlPrefab; }

    private void Awake()
    {
        LevelManager.Instance.OrderWindow = this;
        _orderWindowSwiper = GetComponent<OrderWindowSwiper>();
        GenerateOrderSlots();
       
    }

    private void GenerateOrderSlots()
    {

        _orderWindowSwiper.RequestContainers = new RequestContainer[LevelManager.Instance.QueueManager.ActiveQueueLimit];

        for (int i = 0; i < _orderWindowSwiper.RequestContainers.Length; i++ )
        {
            var orderSlot = Instantiate(_requestCardsContainerPrefab, _orderWindowSwiper.HorizontalSwipeContainer);
            _orderWindowSwiper.RequestContainers[i] = orderSlot.GetComponent<RequestContainer>();
        }
    }


    public void UpdateUI(Customer customer)
    {
        //Debug.Log("UPDATING ORDERWINDOW");
        //_activeCustomer = customer;
        //var foodItemsInOrder = _activeCustomer.Order.OrderRecipes.Count;

        //if (foodItemsInOrder <= _tmpMax)
        //{
        //    for (int i = 0; i < _activeCustomer.Order.OrderRecipes.Count; i++) // This can never be more the tmpMax 
        //    {
        //        var foodItemPnl = _orderWindowSwiper.Slots[i];
        //        foodItemPnl.gameObject.SetActive(true);
        //        foodItemPnl.GetComponent<OrderWindowFoodItemPage>().UpdateThisPage(_activeCustomer.Order.OrderRecipes[i]);
        //    }
        //}
        //else
        //{
        //    Debug.LogError($"Customer {_activeCustomer.name} has more then {_tmpMax} food items Ordered, this will overflow array");
        //}

    }



}
