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
    [Tooltip("The Rect Transform which will be moved when swiped")]
    [SerializeField] private RectTransform _horizontalSwipeContainer;
    [SerializeField] private RectTransform _verticalSwipeContainer;
    [Tooltip("Used to calculate the X distance needed to swipe, in case of additional spacing")]
    [SerializeField]private HorizontalLayoutGroup _swipeContainerHorizontalLayoutGroup;
    [Tooltip("The Slots / containers which will hold customer,food items can be anything, serves as a position for the swiper to go to")]
    [SerializeField] private RectTransform[] _slotsHorizontal; // drag and drop -> Length is used as limit | TODO -> make a spawn system for them in a level editor
   
    [SerializeField] private List< RectTransform> _slotsVertical; 


    private Vector2 _currentHorizontalSwipeContainerPos;
    private Vector2 _currentVerticalSwipeContainerPos;

    private bool _inSmoothTransition;
   // [SerializeField] private int _activeElementsTEMPLIMIT;


    protected Vector2 _newHorizontalPos;
    protected Vector2 _newVerticalPos;

    protected float _swipeHorizontalDistance;
    protected float _swipeVerticalDistance;

    protected RectTransform _elementInFocusHorizontal;
    protected RectTransform _elementInFocusVertical;

    protected int _elementHorizonIndex = 0;
    protected int _elementVerticalIndex = 0;

   // public int ActiveElements { get => _activeElementsTEMPLIMIT; set => _activeElementsTEMPLIMIT = value; }
    public RectTransform[] Slots { get => _slotsHorizontal; }

    virtual protected void Awake()
    {
       // _activeElementsTEMPLIMIT = _slots.Length;
        _swipeHorizontalDistance = _swipeContainerElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;
        InitializeTouchControll();
    }

    virtual protected void Start()
    {
        _currentHorizontalSwipeContainerPos = _horizontalSwipeContainer.anchoredPosition;
        _currentVerticalSwipeContainerPos = _verticalSwipeContainer.anchoredPosition;
    }


    private void InitializeTouchControll() // maybe make this abstract and let the child handle this
    {
        if (_slotsHorizontal.Length > 0)
        {
            var element = _slotsHorizontal[_elementHorizonIndex];
            // element.SetParent(_swipeContainer);
            _elementInFocusHorizontal = element;
        }
        else
        {
            Debug.LogError("TouchSwipeController has no elements to swipe, The SwipeContainer needs to at least have 1 element in order to function");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
      
        float diffX = eventData.pressPosition.x - eventData.position.x;
        float diffY = eventData.pressPosition.y - eventData.position.y;


        _horizontalSwipeContainer.anchoredPosition = _currentHorizontalSwipeContainerPos - new Vector2(diffX, 0);
        _verticalSwipeContainer.anchoredPosition = _currentVerticalSwipeContainerPos - new Vector2(0, diffY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        float percentHorizontal = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        float percentVertical = (eventData.pressPosition.y - eventData.position.y) / Screen.height;

        if (!_inSmoothTransition)
        {
            if (Mathf.Abs(percentHorizontal) >= percentHorizontalSwipeThreshold) 
            {
                _newHorizontalPos = _currentHorizontalSwipeContainerPos;

                if (percentHorizontal > 0 && _elementHorizonIndex < /*_activeElementsTEMPLIMIT*/ _slotsHorizontal.Length - 1)
                {
                    // newPos += new Vector2(-_swipeDistance, 0);
                    //InfinitNextElement(_tmpMaxElementLimit); // maxlimit needs to be the order.recipe.length
                    LimitedNextElement();
                }
                else if (percentHorizontal < 0 && _elementHorizonIndex > 0)
                {
                    //newPos += new Vector2(_swipeDistance, 0);
                    //InfinitPreviousElement(_tmpMaxElementLimit); // MaxLimit needs to be the order.recipe.length
                    LimitedPrevElement();
                }

                StartCoroutine(LimitedTransistionLogic(_horizontalSwipeContainer.anchoredPosition, _newHorizontalPos, _easingSwipe));
            }
            else
            {
               
                ResetHorizontalElement();
            }
        }

        //if(Mathf.Abs(percentVertical) >= percentVerticalSwipeThreshold)
        //{
        //    _newVerticalPos = _currentVerticalSwipeContainerPos;
        //    if(percentVertical > 0 && _elementVerticalIndex < _slotsVertical.Count)
        //    {

        //    }

        //}



    }


    private void GetAllSlotsInSwipeContainer()
    {
        // maybe make slots into a list and get the type from the children insted of drag and drop into arry
    }

    private IEnumerator LimitedTransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _horizontalSwipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _currentHorizontalSwipeContainerPos = endPos;
        _inSmoothTransition = false;

        yield return null;
    }

    protected virtual void LimitedNextElement()
    {
        _elementHorizonIndex++;

        if (_elementHorizonIndex > _slotsHorizontal.Length)
        {
            _elementHorizonIndex = _slotsHorizontal.Length - 1;
        }

        // _elements[_elementIndex] == null then increment agains
        _newHorizontalPos += new Vector2(-1 * (_swipeHorizontalDistance), 0);


    }
    protected virtual void LimitedPrevElement()
    {
        _elementHorizonIndex--;

        if (_elementHorizonIndex < 0)
        {
            _elementHorizonIndex = 0;
        }
        _newHorizontalPos += new Vector2(_swipeHorizontalDistance, 0);
    }


    protected virtual void ResetHorizontalElement()
    {
        StartCoroutine(LimitedTransistionLogic(_horizontalSwipeContainer.anchoredPosition, _currentHorizontalSwipeContainerPos, _easingReset));
    }

}
