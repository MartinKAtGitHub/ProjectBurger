using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Holds the food order that is ready to be sold
/// </summary>
public class FoodTrayDropArea : DropArea
{
    [SerializeField]
    private Order _order; // Get order from Customer
    [SerializeField]
    private OrderGenerator orderGenerator;
   
    private List <FoodStack> _foodStacks = new List<FoodStack>();
    /// <summary>
    /// The current order being processed
    /// </summary>
    public Order Order { get; set; }
    
    public override void DropAreaOnBeginDrag()
    {
        Debug.Log("Dragging from food tray ");
        _foodStacks.RemoveAt(_foodStacks.Count - 1);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        // base.OnDrop(eventData);
        if(_order != null)
        {
            var foodStack =  eventData.pointerDrag.GetComponent<FoodStack>();
            if (foodStack != null)
            {
                _foodStacks.Add(foodStack);
            }
            
            //do th food combo check here but just with the order

        }


       // you cant pick the food back up

    }


    private void AutoSell()
    {
        // Check if foodtype(s) for order is == null
        // check if in correct station
    }
}
