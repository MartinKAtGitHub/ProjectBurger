using UnityEngine;
using UnityEngine.EventSystems;


public class CustomerSelectSwiper : TouchSwipeController
{

    private QueueSlot[] _queueSlots;
    //private Customer _customerInFocus;
    private QueueSlot _queueSlotInFocus;

    public QueueSlot[] QueueSlots { get => _queueSlots; set => _queueSlots = value; }
    public QueueSlot QueueSlotInFocus { get => _queueSlotInFocus; }

    protected override void Awake()
    {
        base.Awake();
        LevelManager.Instance.CustomerSelectSwiper = this;

        //CacheQueueSlotsFromElements();

    }


    protected override void Start()
    {
        base.Start();
        _swipeHorizontalDistance = _swipeContainerHorizontalElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;
        _queueSlotInFocus = _queueSlots[_elementHorizonIndex];
        _queueSlotInFocus.QueueSlotInFocus = true;
        // _elementInFocusHorizontal.GetComponent<QueueSlot>().QueueSlotInFocus = true; // WTF is this <-

        _slotsHorizontal = _queueSlots;
        InitializeTouchControll();

    }

    protected override void InitializeTouchControll()
    {
        _currentHorizontalSwipeContainerPos = _horizontalSwipeContainer.anchoredPosition;
        // _currentVerticalSwipeContainerPos = _verticalSwipeContainer.anchoredPosition;

        if (_slotsHorizontal.Length <= 0)
        {
            Debug.LogError("_slotsHorizontal is 0. Assign Slots for the _slotsHorizontal[] or the swiper cant swipe");
        }

        //base.InitializeTouchControll();

    }

    public override void OnDrag(PointerEventData eventData)
    {
        HorizontalDragging(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestHorizontalElement(eventData);
    }


    protected override void SnapNextHorizontalElement()
    {
        var index = _elementHorizonIndex;
        index++;
        if (index > SlotsHorizontal.Length - 1)
        {
            _elementHorizonIndex = SlotsHorizontal.Length - 1;
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

    protected override void SnapPrevHorizontalElement()
    {
        var index = _elementHorizonIndex;
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

    //private void CacheQueueSlotsFromElements()
    //{
    //    _queueSlots = new QueueSlot[SlotsHorizontal.Length];

    //    for (int i = 0; i < SlotsHorizontal.Length; i++)
    //    {
    //        _queueSlots[i] = SlotsHorizontal[i].GetComponent<QueueSlot>();
    //    }
    //}

}
