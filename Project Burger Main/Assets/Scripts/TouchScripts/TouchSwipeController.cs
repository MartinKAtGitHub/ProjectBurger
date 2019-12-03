using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

//[RequireComponent(typeof(HorizontalLayoutGroup))]
public abstract class TouchSwipeController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // [SerializeField] int _tmpMaxElementLimit;
    [Tooltip("The threshold of what should be consider a horizontal swipe ")]
    [SerializeField] private float percentHorizontalSwipeThreshold = 0.2f;
    [Tooltip("The threshold of what should be consider a vertical swipe")]
    [SerializeField] private float percentVerticalSwipeThreshold = 0.2f;
    [Tooltip("How fast the slide/snap motion to the next element")]
    [SerializeField] private float _easingSwipe = 0.8f;
    [Tooltip("How fast the slide/snap motion back to the original position is in case the cancels swipe")]
    [SerializeField] private float _easingReset = 0.8f;
    [Tooltip("1 of the element gameobject which will be used inside the Swipe container, used to find the size so swipe distance can be calculated ")]
    [SerializeField] private GameObject _swipeContainerElementPrefab;

    [Tooltip("Used to calculate the X distance needed to swipe, in case of additional spacing")]
    [SerializeField] private HorizontalLayoutGroup _swipeContainerHorizontalLayoutGroup;

    [Tooltip("The Rect Transform which will be moved when swiped")]
    [SerializeField] protected RectTransform _horizontalSwipeContainer;
    [SerializeField] protected RectTransform _verticalSwipeContainer;

    [Tooltip("The Slots / containers which will hold customer,food items can be anything, serves as a position for the swiper to go to")]
    [SerializeField] protected SlotHorizontal[] _slotsHorizontal; // drag and drop -> Length is used as limit | TODO -> make a spawn system for them in a level editor
    [SerializeField] protected List<SlotVertical> _slotsVertical;


    protected Vector2 _currentHorizontalSwipeContainerPos;
    protected Vector2 _currentVerticalSwipeContainerPos;

    private bool _inSmoothHorizontalTransition;
    private bool _inSmoothVerticalTransition;
    // [SerializeField] private int _activeElementsTEMPLIMIT;


    protected Vector2 _newHorizontalPos;
    protected Vector2 _newVerticalPos;

    protected float _swipeHorizontalDistance;
    protected float _swipeVerticalDistance;

    protected int _elementHorizonIndex = 0;
    protected int _elementVerticalIndex = 0;

    // public int ActiveElements { get => _activeElementsTEMPLIMIT; set => _activeElementsTEMPLIMIT = value; 

    public SlotHorizontal[] SlotsHorizontal { get => _slotsHorizontal; }
    public RectTransform HorizontalSwipeContainer { get => _horizontalSwipeContainer; }
    public bool InSmoothHorizontalTransition { get => _inSmoothHorizontalTransition; }
    public bool InSmoothVerticalTransition { get => _inSmoothVerticalTransition; }
    public int ElementHorizonIndex { get => _elementHorizonIndex; set => _elementHorizonIndex = value; }
    public int ElementVerticalIndex { get => _elementVerticalIndex; set => _elementVerticalIndex = value; }

    virtual protected void Awake()
    {
        // _activeElementsTEMPLIMIT = _slots.Length;
        _swipeHorizontalDistance = _swipeContainerElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;

        _slotsHorizontal = new SlotHorizontal[LevelManager.Instance.QueueManager.ActiveQueueLimit];
    }

    virtual protected void Start()
    {
        // InitializeTouchControll();
    }



    protected virtual void InitializeTouchControll()
    {

        _currentHorizontalSwipeContainerPos = _horizontalSwipeContainer.anchoredPosition;
        _currentVerticalSwipeContainerPos = _verticalSwipeContainer.anchoredPosition;


        if (_slotsHorizontal.Length <= 0)
        {
            Debug.LogError("_slotsHorizontal is 0. Assign Slots for the _slotsHorizontal[] or the swiper cant swipe");
        }
    }

    public virtual void OnDrag(PointerEventData eventData) // we dont need these inn here
    {
    }

    public virtual void OnEndDrag(PointerEventData eventData)// we dont need these inn here
    {
    }


    private void GetAllSlotsInSwipeContainer()
    {
        // maybe make slots into a list and get the type from the children insted of drag and drop into arry
    }

    private IEnumerator HorizontalTransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothHorizontalTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _horizontalSwipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _currentHorizontalSwipeContainerPos = endPos;
        _inSmoothHorizontalTransition = false;

        yield return null;
    }
    private IEnumerator VerticalTransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothVerticalTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _verticalSwipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _currentVerticalSwipeContainerPos = endPos;
        _inSmoothVerticalTransition = false;

        yield return null;
    }


    protected virtual void SnapNextHorizontalElement()
    {
        _elementHorizonIndex++;

        if (_elementHorizonIndex > _slotsHorizontal.Length)
        {
            _elementHorizonIndex = _slotsHorizontal.Length - 1;
        }

        _newHorizontalPos += new Vector2(-1 * (_swipeHorizontalDistance), 0);
    }
    protected virtual void SnapPrevHorizontalElement()
    {
        _elementHorizonIndex--;

        if (_elementHorizonIndex < 0)
        {
            _elementHorizonIndex = 0;
        }
        _newHorizontalPos += new Vector2(_swipeHorizontalDistance, 0);
    }

    protected virtual void SnapNextVerticalElement()
    {
        if (_slotsVertical.Count > 1)
        {
            _elementVerticalIndex++; // needs to be reset every horizontal snap

            if (_elementVerticalIndex > _slotsVertical.Count)
            {
                _elementVerticalIndex = _slotsVertical.Count - 1;
            }

            _newVerticalPos += new Vector2(0, -1 * (_swipeVerticalDistance));
        }
    }

    protected virtual void SnapPrevVericalElement()
    {
        if (_slotsVertical.Count > 1)
        {
            _elementVerticalIndex--;

            if (_elementVerticalIndex < 0)
            {
                _elementVerticalIndex = 0;
            }
            _newVerticalPos += new Vector2(0, _swipeVerticalDistance);
        }
    }

    protected virtual void ResetHorizontalElement()
    {
        StartCoroutine(HorizontalTransistionLogic(_horizontalSwipeContainer.anchoredPosition, _currentHorizontalSwipeContainerPos, _easingReset));
    }
    protected virtual void ResetVerticalElement()
    {
        StartCoroutine(VerticalTransistionLogic(_verticalSwipeContainer.anchoredPosition, _currentVerticalSwipeContainerPos, _easingReset));
    }

    protected void HorizontalDragging(PointerEventData eventData)
    {
        float diffX = eventData.pressPosition.x - eventData.position.x;
        _horizontalSwipeContainer.anchoredPosition = _currentHorizontalSwipeContainerPos - new Vector2(diffX, 0);
    }

    protected void VerticalDragging(PointerEventData eventData)
    {
        float diffY = eventData.pressPosition.y - eventData.position.y;
        _verticalSwipeContainer.anchoredPosition = _currentVerticalSwipeContainerPos - new Vector2(0, diffY);
    }

    /// <summary>
    /// Checks to see if player has swiped far enough in the horizontal direction to snap to closest Next/Previous element
    /// </summary>
    protected void SnapToClosestHorizontalElement(PointerEventData eventData)
    {
        float percentHorizontal = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (!_inSmoothHorizontalTransition)
        {
            if (Mathf.Abs(percentHorizontal) >= percentHorizontalSwipeThreshold)
            {
                _newHorizontalPos = _currentHorizontalSwipeContainerPos;

                if (percentHorizontal > 0 && _elementHorizonIndex < /*_activeElementsTEMPLIMIT*/ _slotsHorizontal.Length - 1)
                {
                    SnapNextHorizontalElement();
                }
                else if (percentHorizontal < 0 && _elementHorizonIndex > 0)
                {
                    SnapPrevHorizontalElement();
                }
                StartCoroutine(HorizontalTransistionLogic(_horizontalSwipeContainer.anchoredPosition, _newHorizontalPos, _easingSwipe));
            }
            else
            {
                ResetHorizontalElement();
            }
        }
    }

    /// <summary>
    /// Checks to see if player has swiped far enough in the Vertical direction to snap to closest Next/Previous element
    /// </summary>
    protected void SnapToClosestVerticalElement(PointerEventData eventData)
    {

        float percentVertical = (eventData.pressPosition.y - eventData.position.y) / Screen.height;

        if (!_inSmoothVerticalTransition)
        {
            if (Mathf.Abs(percentVertical) >= percentHorizontalSwipeThreshold)
            {
                _newVerticalPos = _currentVerticalSwipeContainerPos;

                if (percentVertical > 0 && _elementVerticalIndex < _slotsVertical.Count - 1)
                {
                    SnapNextVerticalElement();
                }
                else if (percentVertical < 0 && _elementVerticalIndex > 0)
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
