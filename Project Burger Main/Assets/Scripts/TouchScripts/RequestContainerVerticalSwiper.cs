
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RequestContainerVerticalSwiper : TouchSwipeController
{

   [SerializeField] private RequestContainer _requestContainer;

    protected override void Awake()
    {
        base.Awake();
        _requestContainer = GetComponent<RequestContainer>();
    }
    protected override void Start()
    {
        base.Start();
        InitializeTouchControll();
    }



    public override void OnDrag(PointerEventData eventData)
    {
        VerticalDragging(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {      
         SnapToClosestVerticalElement(eventData);
    }

    protected override void InitializeTouchControll()
    {
        _verticalSwipeContainer = _requestContainer.VerticalSwiper;

        _swipeVerticalDistance = _requestContainer.RequestCardPrefab.GetComponent<RectTransform>().sizeDelta.y + 
            _verticalSwipeContainer.GetComponent<VerticalLayoutGroup>().spacing;

        _currentVerticalSwipeContainerPos = _verticalSwipeContainer.anchoredPosition;

        //base.InitializeTouchControll();
    }

    protected override void SnapNextVerticalElement()
    {

        _elementVerticalIndex++;

        if (_elementVerticalIndex > _requestContainer.RequestCards.Count)
        {
            _elementVerticalIndex = _requestContainer.RequestCards.Count - 1;
        }

        _newVerticalPos -= new Vector2(0, -1 * (_swipeVerticalDistance));
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

    protected override void SnapToClosestVerticalElement(PointerEventData eventData)
    {

        float percentVertical = (eventData.pressPosition.y - eventData.position.y) / Screen.height;

        if (!_inSmoothVerticalTransition)
        {
            if (Mathf.Abs(percentVertical) >= percentVerticalSwipeThreshold)
            {
                _newVerticalPos = _currentVerticalSwipeContainerPos;

                if (percentVertical < 0 && _elementVerticalIndex < _requestContainer.RequestCards.Count - 1)
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
