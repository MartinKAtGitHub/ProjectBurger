using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public abstract class TouchSwipeController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // [SerializeField] int _tmpMaxElementLimit;
    [Tooltip("The threshold of what should be consider a swipe")]
    [SerializeField] private float percentSwipeThreshold = 0.2f;
    [Tooltip("How fast the slide/snap motion to the next element")]
    [SerializeField] private float _easingSwipe = 0.8f;
    [Tooltip("How fast the slide/snap motion back to the original position is in case the cancels swipe")]
    [SerializeField] private float _easingReset = 0.8f;
    [Tooltip("1 of the element gameobject which will be used inside the Swipe container, used to find the size so swipe distance can be calculated ")]
    [SerializeField] private GameObject _swipeContainerElementPrefab;
    [Tooltip("The Rect Transform which will be moved when swiped")]
    [SerializeField] private RectTransform _swipeContainer;
   /* [Tooltip("Used to calculate the X distance needed to swipe, in case of additional spacing")]
    [SerializeField] */private HorizontalLayoutGroup _swipeContainerHorizontalLayoutGroup;
    [Tooltip("The ElementPrefabs / containers which will hold data(customer/food item) and will be swiped to")]
    [SerializeField] private RectTransform[] _elements; // drag and drop -> Length is used as limit


    private Vector2 _currentSwipeContainerPos;
    private bool _inSmoothTransition;
    [SerializeField] private int _activeElementsTEMPLIMIT;


    protected Vector2 _newPos;
    protected float _swipeDistance;
    protected RectTransform _elementInFocus;
    protected int _elementIndex = 0;

    public int ActiveElements { get => _activeElementsTEMPLIMIT; set => _activeElementsTEMPLIMIT = value; }
    public RectTransform[] Elements { get => _elements;}

    virtual protected void Awake()
    {
        _swipeContainerHorizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        if(_swipeContainerHorizontalLayoutGroup == null)
        {
            Debug.LogError($"Horizontal layout group on > {name} is NULL");
        }

        _swipeContainer = GetComponent<RectTransform>();

        if (_swipeContainer == null)
        {
            Debug.LogError($"Cant find RectTransform make sure {name} is the swipe object");
        }

        _activeElementsTEMPLIMIT = _elements.Length;
        _swipeDistance = _swipeContainerElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;

        Debug.Log(_swipeDistance);
        InitializeTouchControll();
    }

    virtual protected void Start()
    {
        _currentSwipeContainerPos = _swipeContainer.anchoredPosition;
    }


    private void InitializeTouchControll() // maybe make this abstract and let the child handle this
    {
        if (_elements.Length > 0)
        {
            var element = _elements[_elementIndex];
            // element.SetParent(_swipeContainer);
            _elementInFocus = element;
        }
        else
        {
            Debug.LogError("TouchSwipeController has no elements to swipe, The SwipeContainer needs to at least have 1 element in order to function");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        float diff = eventData.pressPosition.x - eventData.position.x;
        _swipeContainer.anchoredPosition = _currentSwipeContainerPos - new Vector2(diff, 0);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if(!_inSmoothTransition)
        {
            if (Mathf.Abs(percentage) >= percentSwipeThreshold)
            {
                _newPos = _currentSwipeContainerPos;

                if (percentage > 0 && _elementIndex < _activeElementsTEMPLIMIT - 1)
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

                StartCoroutine(LimitedTransistionLogic(_swipeContainer.anchoredPosition, _newPos, _easingSwipe));
            }
            else
            {
                ResetElement();
            }
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

    protected virtual void LimitedNextElement()
    {
        Debug.Log("BASIC NEXT");
        _elementIndex++;

        if (_elementIndex > _elements.Length)
        {
            _elementIndex = _elements.Length - 1;
        }

        // _elements[_elementIndex] == null then increment agains
        _newPos += new Vector2(-1 * (_swipeDistance), 0);


    }
    protected virtual void LimitedPrevElement()
    {
        Debug.Log("PREV");
        _elementIndex--;

        if (_elementIndex < 0)
        {
            _elementIndex = 0;
        }
        _newPos += new Vector2(_swipeDistance, 0);
    }


    protected virtual void ResetElement()
    {
        StartCoroutine(LimitedTransistionLogic(_swipeContainer.anchoredPosition, _newPos, _easingReset));
    }

}
