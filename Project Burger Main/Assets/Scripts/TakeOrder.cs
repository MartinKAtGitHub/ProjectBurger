using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrder : MonoBehaviour
{
    [SerializeField] private GameObject _hasOrderIcon;

    private CustomerSelectSwiper _customerSelect;
    private OrderWindow _orderWindow;

    private void Awake()
    {
        LevelManager.Instance.TakeOrder = this;
    }
    private void Start()
    {
        _customerSelect = LevelManager.Instance.CustomerSelectSwiper;
        _orderWindow = LevelManager.Instance.OrderWindow;
    }

    private void Update()
    {
      
        if (_customerSelect.QueueSlotInFocus.CurrentCustomer != null && !_customerSelect.InSmoothHorizontalTransition)
        {
            if(_customerSelect.QueueSlotInFocus.CurrentCustomer.Order != null && _customerSelect.QueueSlotInFocus.CurrentCustomer.IsWaiting)
            {
                _hasOrderIcon.SetActive(true);
            }
            else
            {
                _hasOrderIcon.SetActive(false);
                
            }
        }
        else
        {
            _hasOrderIcon.SetActive(false);
        }
    }

    public void SendOrderToOrderDisplay()
    {
        _orderWindow.UpdateOrderDisplayUI(_customerSelect.QueueSlotInFocus.CurrentCustomer.Order, _customerSelect.ElementHorizonIndex);
        _customerSelect.QueueSlotInFocus.CurrentCustomer.IsWaiting = false;
    }

}
