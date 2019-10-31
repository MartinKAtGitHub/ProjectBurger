using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelectSwiper : TouchSwipeController
{

    private QueueSlot[] _queueSlots;
    private Customer _customerInFocus;
    public QueueSlot[] QueueSlots { get => _queueSlots; }

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.CustomerSelectSwiper = this;

        // Maybe make an init
        CacheQueueSlotsFromElements();
        _elementInFocus.GetComponent<QueueSlot>().QueueSlotInFocus = true;

        Debug.LogError("CustomerSelectSwiper.cs might be broken becasue _swipeContainer" +
            " and _swipeContainerHorizontalLayoutGroup are changed beacase of updated TouchScriped." +
            " Script need to be attached on movign object");

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
                _customerInFocus = newSlot.CurrentCustomer;

                LevelManager.Instance.OrderWindow.UpdateUI(_customerInFocus);

                _newPos += new Vector2(-1 * (_swipeDistance + skipDistance), 0);
                Debug.Log(" NEXT Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }

        Debug.Log(_elementIndex + "INDEX");
        //ResetPnl();
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
                _customerInFocus = newSlot.CurrentCustomer;

                _newPos += new Vector2(_swipeDistance + skipDistance, 0);
                Debug.Log(" PREV Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
        Debug.Log(_elementIndex + "INDEX");
    }


    private void CacheQueueSlotsFromElements()
    {
        _queueSlots = new QueueSlot[Slots.Length];

        for (int i = 0; i < Slots.Length; i++)
        {
            _queueSlots[i] = Slots[i].GetComponent<QueueSlot>();
        }
    }

}
