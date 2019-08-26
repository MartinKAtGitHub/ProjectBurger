using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{

    [SerializeField] private int _tmpMax = 6; // TODO  OrderWindow.cs | Connect Max limit pnls/ fooditems per customer to Manager
    [SerializeField] private GameObject _foodItemPnlPrefab;
    /// <summary>
    /// Holds the pnls with the information on fooditem which are not in focus from the player.
    /// </summary>
    [SerializeField] private RectTransform _notInFocusPnls;

    private GameObject[] foodItemElements;
    private Customer _activeCustomer;

    public Customer ActiveCustomer { get => _activeCustomer; }
    public RectTransform NotInFocusPnls { get => _notInFocusPnls; }
    public GameObject[] FoodItemElements { get => foodItemElements; }
    public GameObject FoodItemPnlPrefab { get => _foodItemPnlPrefab;  }

    private void Awake()
    {
        LevelManager.Instance.OrderWindow = this;
        //GenerateFoodItemPnls();
    }

    public void OpenWindow(Customer customer)
    {
        // Anim Fade INN Window / enable window = true
        
       // UpdateUI(customer);
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        // Fade OUT Window / enable window = false
        //set all to null
        gameObject.SetActive(false);
        // Destroy(_orderWindow);
    }

    public void UpdateUI(Customer customer)
    {
        _activeCustomer = customer;
        var foodItemsInOrder = _activeCustomer.Order.OrderRecipes.Count;

        if(foodItemsInOrder <= _tmpMax)
        {
            for (int i = 0; i < _activeCustomer.Order.OrderRecipes.Count; i++) // This can never be more the tmpMax 
            {
                var foodItemPnl = FoodItemElements[i];
                foodItemPnl.gameObject.SetActive(true);
                foodItemPnl.GetComponent<OrderWindowFoodItemPage>().UpdateThisPage(_activeCustomer.Order.OrderRecipes[i]);
            }
        }
        else
        {
            Debug.LogError($"Customer {_activeCustomer.name} has more then {_tmpMax} food items Ordered, this will overflow array" );
        }

    }

    //private void GenerateFoodItemPnls() // move to touch
    //{
    //    foodItemElements = new GameObject[_tmpMax]; // TODO OrderwinSipe.cs | Connect the max limit to Manager so its not hardcoded
    //    for (int i = 0; i < _tmpMax; i++)
    //    {
    //        var clone = Instantiate(_foodItemPnlPrefab, _notInFocusPnls.transform);
    //        clone.SetActive(false);
    //       // foodItemPnls[i] = clone.GetComponent<OrderWindowFoodItemPage>();
    //    }
    //}
}
