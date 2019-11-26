using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelectSwiper : TouchSwipeController
{

    [SerializeField]private QueueSlot[] _queueSlots;
    //private Customer _customerInFocus;
    private QueueSlot _queueSlotInFocus;
   
    public QueueSlot[] QueueSlots { get => _queueSlots; }
    public QueueSlot QueueSlotInFocus { get => _queueSlotInFocus;}

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.CustomerSelectSwiper = this;

        // Maybe make an init
        CacheQueueSlotsFromElements();
        _queueSlotInFocus = _queueSlots[_elementHorizonIndex];

        _elementInFocusHorizontal.GetComponent<QueueSlot>().QueueSlotInFocus = true; // WTF is this <-
    }


    protected override void Start()
    {
        base.Start();

       // LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);
    }

    protected override void LimitedNextElement()
    {
        var index =  _elementHorizonIndex;
        index++;
        if (index > Slots.Length - 1)
        {
            _elementHorizonIndex = Slots.Length - 1;
            return;
        }

        var skipDistance = 0f;
        for (int i = index; i < _queueSlots.Length; i++)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeHorizontalDistance;
            }
            else
            {
                var oldSlot = _queueSlots[_elementHorizonIndex];
                oldSlot.QueueSlotInFocus = false;

                _elementHorizonIndex = i;

                var newSlot = _queueSlots[_elementHorizonIndex];
                newSlot.QueueSlotInFocus = true;
                _queueSlotInFocus = newSlot;

                //SetCustomerInFocus(newSlot.CurrentCustomer);
               // LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);

                _newHorizontalPos += new Vector2(-1 * (_swipeHorizontalDistance + skipDistance), 0);
               // Debug.Log(" NEXT Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
    }

    protected override void LimitedPrevElement()
    {
        var index =  _elementHorizonIndex;
        index--;
        if (index < 0)
        {
            _elementHorizonIndex = 0;
        }

        var skipDistance = 0f;

        for (int i = index; i >= 0; i--)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeHorizontalDistance;
            }
            else
            {
                var oldSlot = _queueSlots[_elementHorizonIndex];
                oldSlot.QueueSlotInFocus = false;

                _elementHorizonIndex = i;

                var newSlot = _queueSlots[_elementHorizonIndex];
                newSlot.QueueSlotInFocus = true;
                _queueSlotInFocus = newSlot;

                // SetCustomerInFocus(newSlot.CurrentCustomer);
                //LevelManager.Instance.FoodTrayManger.SetFoodTrayFocus(_elementIndex);

                _newHorizontalPos += new Vector2(_swipeHorizontalDistance + skipDistance, 0);
               // Debug.Log(" PREV Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
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
