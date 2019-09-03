using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelectSwiper : TouchSwipeController
{

    private QueueSlot[] _queueSlots;

    public QueueSlot[] QueueSlots { get => _queueSlots;}

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.CustomerSelectSwiper = this;
        CacheQueueSlotsFromElements();
    }

    protected override void LimitedNextElement()
    {
        _elementIndex++;
        if(_elementIndex > _elements.Length -1)
        {
            _elementIndex = _elements.Length - 1;
            return;
        }

        var skipDistance = 0f;
        for (int i = _elementIndex; i < _queueSlots.Length; i++)
        {
            if(_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeDistance;
            }
            else
            {
                _elementIndex = i;
                _newPos += new Vector2(-1 * (_swipeDistance + skipDistance), 0);
                Debug.Log("Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }

        //ResetPnl();
    }

    protected override void LimitedPrevElement()
    {
        _elementIndex--;
        if(_elementIndex < 0)
        {
            _elementIndex = 0;
        }

        var skipDistance = 0f;

        for (int i = _elementIndex; i >= 0; i--)
        {
            if (_queueSlots[i].CurrentCustomer == null)
            {
                skipDistance += _swipeDistance;
            }
            else
            {
                _elementIndex = i;
                _newPos += new Vector2(_swipeDistance + skipDistance, 0);
                Debug.Log("Moving " + skipDistance + " New index = " + i + " Customer Name = " + _queueSlots[i].CurrentCustomer.name);
                return;
            }
        }
    }


    private void CheckSkipNextQueueSlot(int index)
    {
        if(index >= _elements.Length - 1)
        {
            // Dont need to move here bacuse there is nothing at the end
            return;
        }
        else
        {
            //if(_elements[index].GetComponent<QueueSlot>().CurrentCustomer == null)
            if (_queueSlots[index].CurrentCustomer == null)
            {
                _newPos += new Vector2(-1 * (_swipeDistance), 0); // Add more distance since the next spot is empty
                index++;
                if (index > _elements.Length - 1)
                {
                    return;
                }
                else
                {
                    CheckSkipNextQueueSlot(index);
                }
            }
            else
            {
                return;
            }
        }

       
    }

    private void CheckSkipQueueSlot()
    {

    }

    private void CacheQueueSlotsFromElements()
    {
        _queueSlots = new QueueSlot[_elements.Length];

        for (int i = 0; i < _elements.Length; i++)
        {
            _queueSlots[i] = _elements[i].GetComponent<QueueSlot>();
        }
    }

}
