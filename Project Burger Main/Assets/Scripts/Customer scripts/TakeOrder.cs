using UnityEngine;
using UnityEngine.EventSystems;

public class TakeOrder : MonoBehaviour
{
    private Customer _customer;
    

    private void Awake()
    {
        _customer = GetComponent<Customer>();
    
    }

    public void TakeCustomerOrder()
    {
        //  IF -> is order ready 
        LevelManager.Instance.OrderWindow.UpdateUI(_customer);
        LevelManager.Instance.FoodTray.UpdateFoodTray(_customer); 
    }

    //public void OnPointerClick(PointerEventData eventData) // dosent work, gets activated on when swiping 
    //{
    //    Debug.Log("CLICK TEST");
    //}
}
