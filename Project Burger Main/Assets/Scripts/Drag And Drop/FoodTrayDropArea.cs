
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// DropArea logic for food the player want to be sold
/// </summary>
public class FoodTrayDropArea : MonoBehaviour, IDropHandler
{
   
    //[serializefield] private order _order; // get order from customer
    //[serializefield] private bool _ordersuccessful;

    private RectTransform _thisRectTransform;
    private FoodTray _foodTray;

    //public Order Order { set => _order = value; }
    //public bool OrderSuccessful { get => _orderSuccessful; }

    [Space(20)] public List<Food> _foods = new List<Food>();

    private void Awake()
    {
        //LevelManager.Instance.FoodTrayDropArea = this;
        _foodTray = GetComponent<FoodTray>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_foodTray.Order != null)
        {
            var food = eventData.pointerDrag.GetComponent<Food>();
            if (food != null)
            {
                //food.FoodDrag.ResetPositionParent = this.transform;
                food.FoodDrag.ResetPositionParent = _thisRectTransform;
                //_foods.Add(food);
                _foodTray.FoodItemsOnTray.Add(food);

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
        // _foods.RemoveAt(_foods.Count - 1);
        _foodTray.FoodItemsOnTray.RemoveAt(_foodTray.FoodItemsOnTray.Count - 1);
    }

}
