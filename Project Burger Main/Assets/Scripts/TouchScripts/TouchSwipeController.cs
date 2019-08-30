using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public abstract class TouchSwipeController : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [SerializeField] int _tmpMaxElementLimit;
    [Tooltip("The threshold of what should be consider a swipe")]
    [SerializeField] private float percentSwipeThreshold = 0.2f;
    [Tooltip("How smooth the slide/snap anim is after a swipe")]
    [SerializeField] private float _easing = 0.8f;
    [Tooltip("The element gameobject which will be used inside the Swipe container ")]
    [SerializeField] private GameObject _swipeContainerElementPrefab;
    [Tooltip("The Rect Transform which will be moved when swiped")]
    [SerializeField] private RectTransform _swipeContainer;
    [Tooltip("Used to calculate the X distance needed to swipe, in case of additional spacing")]
    [SerializeField] private HorizontalLayoutGroup _swipeContainerHorizontalLayoutGroup;


    private Vector2 _currentSwipeContainerPos;
    private Vector2 _newPos;
    private float _swipeDistance;
    private bool _inSmoothTransition;
    private int _elementIndex = 0;
    private RectTransform _elementInFocus;
    private RectTransform[] _elements;
    private int _activeElements = 3;

    private int _nextCount;
    private int _prevCount;

    public int ActiveElements { get => _activeElements; set => _activeElements = value; }

    virtual protected void Awake()
    {
        _swipeDistance = _swipeContainerElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;

        Debug.Log(_swipeDistance);
        InitializeTouchControll();
    }

    virtual protected void Start()
    {
        _currentSwipeContainerPos = _swipeContainer.anchoredPosition;
    }


    private void InitializeTouchControll()
    {
        GenerateElements();

        var element = _elements[_elementIndex];
        element.SetParent(_swipeContainer);
        _elementInFocus = element;
    }


    public void OnDrag(PointerEventData eventData)
    {
        float diff = eventData.pressPosition.x - eventData.position.x;
        _swipeContainer.anchoredPosition = _currentSwipeContainerPos - new Vector2(diff, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (Mathf.Abs(percentage) >= percentSwipeThreshold/* && _notInFocusContainer.childCount > 0*/)
        {
             _newPos = _currentSwipeContainerPos;
            Debug.Log(_elementIndex);
            if (percentage > 0 && _elementIndex < _activeElements - 1)
            {
                // newPos += new Vector2(-_swipeDistance, 0);
                //InfinitNextElement(_tmpMaxElementLimit); // maxlimit needs to be the order.recipe.length
                LimitedNextElement();
            }
            else if (percentage < 0 && _elementIndex > 0)
            {
                //newPos += new Vector2(_swipeDistance, 0);
                //InfinitPreviousElement(_tmpMaxElementLimit); // MaxLimit needs to be the order.recipe.length
                LimitedPrevElement();
            }

            StartCoroutine(LimitedTransistionLogic(_swipeContainer.anchoredPosition, _newPos, _easing));
        }
        else
        {
            ResetPnl();
        }
    }




    private IEnumerator TransistionNextLogic(Vector2 startPos, Vector2 endPos, float sec, int lastIndex)
    {
        _inSmoothTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _swipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _elements[lastIndex].SetAsLastSibling();

        _swipeContainer.anchoredPosition = Vector2.zero;
        _currentSwipeContainerPos = Vector2.zero;


        _inSmoothTransition = false;

        yield return null;
    }

    private IEnumerator TransistionPrevLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;

        _elements[_elementIndex].SetAsFirstSibling();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _swipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _swipeContainer.anchoredPosition = Vector2.zero;
        _currentSwipeContainerPos = Vector2.zero;


        _inSmoothTransition = false;

        yield return null;
    }

    private IEnumerator TransistionResetLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _swipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _currentSwipeContainerPos = endPos;
        _inSmoothTransition = false;

        yield return null;
    }


    //private IEnumerator TransistionSeperateContainerLogic(Vector2 startPos, Vector2 endPos, float sec)
    //{
    //    _inSmoothTransition = true;
    //    // LevelManager.Instance.OrderWindow.CloseWindow();


    //    float t = 0f;
    //    while (t <= 1.0)
    //    {
    //        t += Time.deltaTime / sec;
    //        _swipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
    //        yield return null;
    //    }

    //    var oldSlot = _elementInFocus;

    //    if (oldSlot != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
    //    {
    //        oldSlot.transform.SetParent(_notInFocusContainer);
    //        oldSlot.transform.localPosition = Vector2.zero;
    //    }

    //    _elementInFocus = _elements[_elementIndex];
    //    _swipeContainer.anchoredPosition = Vector2.zero;
    //    _inSmoothTransition = false;

    //    yield return null;
    //}

    /// <summary>
    /// Triggers the swipe anim for the next element in the array. 
    /// </summary>
    /// <param name="MaxLimit"> The length of the elements, used for looping around to the start of the array</param>
    private void InfinitNextElement(int MaxLimit)
    {
        if (MaxLimit > _elements.Length)
        {
            Debug.LogError($"element amount({MaxLimit}) is higher then the amount of available element containers ({_elements.Length}), Make sure not to go over the LIMIT");
            return;
        }

        if (!_inSmoothTransition /*&& _notInFocusContainer.childCount > 0*/)
        {
            var lastIndex = _elementIndex;
            _elementIndex++;

            if (_elementIndex > MaxLimit - 1)
            {
                _elementIndex = 0;
            }

            var newPos = _currentSwipeContainerPos;
            newPos += new Vector2(-1 * (_swipeDistance), 0);

            StartCoroutine(TransistionNextLogic(_swipeContainer.anchoredPosition, newPos, _easing, lastIndex));

        }
    }

    /// <summary>
    /// Triggers the swipe anim for the previous element in the array. 
    /// </summary>
    /// <param name="MaxLimit"> The length of the elements, used for looping to the front of the elements</param>
    private void InfinitPreviousElement(int MaxLimit)
    {
        if (MaxLimit > _elements.Length)
        {
            Debug.LogError($"element amount({MaxLimit}) is higher then the amount of available element containers ({_elements.Length}), Make sure not to go over the LIMIT");
            return;
        }

        if (!_inSmoothTransition /*&& _notInFocusContainer.childCount > 0*/)
        {
            Debug.Log("Previous Element");

            _elementIndex--;

            if (_elementIndex < 0)
            {
                _elementIndex = MaxLimit - 1;
            }

            var newPos = _currentSwipeContainerPos;
            newPos += new Vector2(_swipeDistance, 0);
            StartCoroutine(TransistionPrevLogic(_swipeContainer.anchoredPosition, newPos, _easing));

        }
    }


    private IEnumerator LimitedTransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;
        // LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _swipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _currentSwipeContainerPos = endPos;
        _inSmoothTransition = false;

        yield return null;
    }

    private void LimitedNextElement()
    {
        Debug.Log("NEXT");
        _elementIndex ++;
       
        _newPos += new Vector2(-1 * (_swipeDistance), 0);

     
    }
    private void LimitedPrevElement()
    {
        Debug.Log("PREV");
        _elementIndex--;
      
        _newPos += new Vector2(_swipeDistance, 0);
   
    }


    private void ResetPnl()
    {
        Debug.Log("Reset swipe container back to original position");

        StartCoroutine(TransistionResetLogic(_swipeContainer.anchoredPosition, _currentSwipeContainerPos, _easing));
    }
    protected void GenerateElements()
    {
        _elements = new RectTransform[_tmpMaxElementLimit];

        for (int i = 0; i < _elements.Length; i++)
        {
            //var clone = Instantiate(_swipeContainerElementPrefab, _notInFocusContainer.transform);
            var clone = Instantiate(_swipeContainerElementPrefab, _swipeContainer.transform);
            clone.GetComponent<OrderWindowFoodItemPage>().RecipeName.text = "ELEMENT " + i;
            _elements[i] = clone.GetComponent<RectTransform>();

        }
    }

}
