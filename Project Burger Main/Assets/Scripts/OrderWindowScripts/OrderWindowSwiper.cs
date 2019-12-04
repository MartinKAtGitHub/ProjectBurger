using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderWindowSwiper : TouchSwipeController
{
 
    private OrderWindow _orderWindow;


    protected override void Awake()
    {
        base.Awake();
        _orderWindow = GetComponent<OrderWindow>();
    }
    protected override void Start()
    {
        base.Start();
       
        InitializeTouchControll();
    }

    protected override void InitializeTouchControll()
    {
        _slotsHorizontal = _orderWindow.RequestContainers;
        _verticalSwipeContainer = _orderWindow.RequestContainers[_elementHorizonIndex].VerticalSwiper;

       //_swipeHorizontalDistance = _orderWindow.RequestCardsContainerPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;
        _swipeVerticalDistance = _orderWindow.RequestCardsContainerPrefab.GetComponent<RequestContainer>().RequestCardPrefab.GetComponent<RectTransform>().sizeDelta.y +
            _orderWindow.RequestCardsContainerPrefab.GetComponent<RequestContainer>().VerticalSwiper.GetComponent<VerticalLayoutGroup>().spacing;

        base.InitializeTouchControll();
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        HorizontalDragging(eventData);
        VerticalDragging(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestHorizontalElement(eventData);
        SnapToClosestVerticalElement(eventData);
    }

    protected override void SnapNextHorizontalElement()
    {
        _elementHorizonIndex++;
        if (_elementHorizonIndex > _slotsHorizontal.Length)
        {
            _elementHorizonIndex = _slotsHorizontal.Length - 1;
        }
        _verticalSwipeContainer = _orderWindow.RequestContainers[_elementHorizonIndex].VerticalSwiper;

        _newHorizontalPos += new Vector2(-1 * (_swipeHorizontalDistance), 0);
    }

    protected override void SnapPrevHorizontalElement()
    {
        _elementHorizonIndex--;

        if (_elementHorizonIndex < 0)
        {
            _elementHorizonIndex = 0;
        }

        _verticalSwipeContainer = _orderWindow.RequestContainers[_elementHorizonIndex].VerticalSwiper;
        _newHorizontalPos += new Vector2(_swipeHorizontalDistance, 0);
    }

    protected override void SnapNextVerticalElement()
    {

        _elementVerticalIndex++; 

        if (_elementVerticalIndex > _orderWindow.RequestContainers[_elementHorizonIndex].RequestCards.Count)
        {
            _elementVerticalIndex = _orderWindow.RequestContainers[_elementHorizonIndex].RequestCards.Count - 1;
        }

        _newVerticalPos -= new Vector2(0, -1 * (_swipeVerticalDistance));
        Debug.Log(_newVerticalPos + "GO NEXT");

    }

    protected override void SnapPrevVericalElement()
    {

        _elementVerticalIndex--;

        if (_elementVerticalIndex < 0)
        {
            _elementVerticalIndex = 0;
        }
        _newVerticalPos -= new Vector2(0, _swipeVerticalDistance);

    }

    protected override void ResetHorizontalElement()
    {
        base.ResetHorizontalElement();
    }

    protected override void SnapToClosestVerticalElement(PointerEventData eventData)
    {

        float percentVertical = (eventData.pressPosition.y - eventData.position.y) / Screen.height;

        if (!_inSmoothVerticalTransition)
        {
            if (Mathf.Abs(percentVertical) >= percentVerticalSwipeThreshold)
            {
                _newVerticalPos = _currentVerticalSwipeContainerPos;

                if (percentVertical < 0 && _elementVerticalIndex < _orderWindow.RequestContainers[_elementHorizonIndex].RequestCards.Count - 1)
                {
                    SnapNextVerticalElement();
                }
                else if (percentVertical > 0 && _elementVerticalIndex > 0)
                {
                    SnapPrevVericalElement();
                }

                StartCoroutine(VerticalTransistionLogic(_verticalSwipeContainer.anchoredPosition, _newVerticalPos, _easingSwipe));
            }
            else
            {
                ResetVerticalElement();
            }
        }
    }


}
