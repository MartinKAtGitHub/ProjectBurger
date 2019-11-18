using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelectSwiper : TouchSwipeController
{

    [SerializeField]private QueueSlot[] _queueSlots;
    private Customer _customerInFocus;
   
    //private QueueSlot _queueSlotInFocus;
    public QueueSlot[] QueueSlots { get => _queueSlots; }

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.CustomerSelectSwiper = this;

        // Maybe make an init
        CacheQueueSlotsFromElements();
        _elementInFocus.GetComponent<QueueSlot>().QueueSlotInFocus = true;
    }


    protected override void Start()
    {
        base.Start();

       // LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);
    }

    protected override void LimitedNextElement()
    {
        var index =  _elementIndex;
        index++;
        if (index > Slots.Length - 1)
        {
            _elementIndex = Slots.Length - 1;
            return;
        }

        var skipDistance = 0f;
        for (int i = index; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeDistance;
            }
            else
            {
                var oldSlot = _queueSlots[_elementIndex];
                oldSlot.QueueSlotInFocus = false;

                _elementIndex = i;

                var newSlot = _queueSlots[_elementIndex];
                newSlot.QueueSlotInFocus = true;

                //SetCustomerInFocus(newSlot.CurrentCustomer);
                LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);

                _newPos += new Vector2(-1 * (_swipeDistance + skipDistance), 0);
                Debug.Log(" NEXT Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
    }

    protected override void LimitedPrevElement()
    {
        var index =  _elementIndex;
        index--;
        if (index < 0)
        {
            _elementIndex = 0;
        }

        var skipDistance = 0f;

        for (int i = index; i >= 0; i--)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeDistance;
            }
            else
            {
                var oldSlot = _queueSlots[_elementIndex];
                oldSlot.QueueSlotInFocus = false;

                _elementIndex = i;

                var newSlot = _queueSlots[_elementIndex];
                newSlot.QueueSlotInFocus = true;

                // SetCustomerInFocus(newSlot.CurrentCustomer);
                LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);

                _newPos += new Vector2(_swipeDistance + skipDistance, 0);
                Debug.Log(" PREV Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
        Debug.Log(_elementIndex + "INDEX");
    }


    //private void SetCustomerInFocus(Customer currentCustomer) // move to take order logic
    //{
    //    if(currentCustomer != null)
    //    {
    //        _customerInFocus = currentCustomer;
    //        LevelManager.Instance.OrderWindow.UpdateUI(currentCustomer);
    //        LevelManager.Instance.FoodTray.UpdateFoodTray(currentCustomer);
    //    }
    //    else
    //    {
    //        Debug.Log("QueueSlot Is Empty, No customer to update FOODTRAY OR ORDERWINDOW");
    //    }
    //}

    private void CacheQueueSlotsFromElements()
    {
        _queueSlots = new QueueSlot[Slots.Length];

        for (int i = 0; i < Slots.Length; i++)
        {
            _queueSlots[i] = Slots[i].GetComponent<QueueSlot>();
        }
    }

}
