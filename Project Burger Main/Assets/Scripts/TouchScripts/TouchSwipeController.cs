﻿using System.Collections;
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
    [Tooltip("The Rect Transform which will be moved when swiped")]
    [SerializeField] private RectTransform _swipeContainer;
    [Tooltip("Container which holds all the elements not visible to the player")]
    [SerializeField] private RectTransform _notInFocusContainer;
    [Tooltip("The element gameobject which will be used inside the Swipe container ")]
    [SerializeField] private GameObject _swipeContainerElementPrefab;
    [Tooltip("Used to calculate the X distance needed to swipe, in case of additional spacing")]
    [SerializeField] private HorizontalLayoutGroup _swipeContainerHorizontalLayoutGroup;


    private Vector2 _currentSwipeContainerPos;
    private float _swipeDistance;
    private bool _inSmoothTransition;
    private int _elementIndex;
    private RectTransform _elementInFocus;
    private RectTransform[] _elements;



    virtual protected void Awake()
    {
        _swipeDistance = _swipeContainerElementPrefab.GetComponent<RectTransform>().sizeDelta.x + _swipeContainerHorizontalLayoutGroup.spacing;

        InitializeTouchControll();
    }

    virtual protected void Start()
    {
        _currentSwipeContainerPos = _swipeContainer.anchoredPosition;
    }


    private void InitializeTouchControll()
    {
        GenerateElements();

        var element = _elements[0];
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

        if (Mathf.Abs(percentage) >= percentSwipeThreshold && _notInFocusContainer.childCount > 0)
        {
            var newPos = _currentSwipeContainerPos;

            if (percentage > 0)
            {

                // newPos += new Vector2(-_swipeDistance, 0);
                NextElement(_tmpMaxElementLimit); // maxlimit needs to be the order.recipe.length
            }
            else if (percentage < 0)
            {

                //newPos += new Vector2(_swipeDistance, 0);
                PreviousElement(_tmpMaxElementLimit); // MaxLimit needs to be the order.recipe.length
            }

            //_swipeContainer.anchoredPosition = newPos;
            //_currentSwipeContainerPos = newPos;
        }
        else
        {
            ResetPnl();
        }
    }

    private void ResetPnl()
    {
        Debug.Log("Reset swipe container back to original position");
        _swipeContainer.anchoredPosition = _currentSwipeContainerPos;
    }


    private IEnumerator TransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
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
        //AfterTrans();

        var oldSlot = _elementInFocus;

        if (oldSlot != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
        {
            oldSlot.transform.SetParent(_swipeContainer);
            oldSlot.transform.localPosition = Vector2.zero;
        }

        _elementInFocus = _elements[_elementIndex];
        _swipeContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }

    /// <summary>
    /// Triggers the swipe anim for the next element in the array. 
    /// </summary>
    /// <param name="MaxLimit"> The length of the elements, used for looping around to the start of the array</param>
    private void NextElement(int MaxLimit)
    {
        if (MaxLimit > _elements.Length)
        {
            Debug.LogError($"element amount({MaxLimit}) is higher then the amount of available element containers ({_elements.Length}), Make sure not to go over the LIMIT");
            return;
        }

        if (!_inSmoothTransition && _notInFocusContainer.childCount > 0)
        {
            Debug.Log("Next Element");
            _elementIndex++;

            if (_elementIndex > MaxLimit - 1)
            {
                _elementIndex = 0;
            }

            //_limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
            _elements[_elementIndex].transform.SetParent(_swipeContainer);
            _elements[_elementIndex].transform.SetAsLastSibling();

            var newPos = _swipeContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_swipeDistance), 0);

            StartCoroutine(TransistionLogic(_swipeContainer.anchoredPosition, newPos, _easing));

        }
    }

    /// <summary>
    /// Triggers the swipe anim for the previous element in the array. 
    /// </summary>
    /// <param name="MaxLimit"> The length of the elements, used for looping to the front of the elements</param>
    private void PreviousElement(int MaxLimit)
    {
        if (MaxLimit > _elements.Length)
        {
            Debug.LogError($"element amount({MaxLimit}) is higher then the amount of available element containers ({_elements.Length}), Make sure not to go over the LIMIT");
            return;
        }

        if (!_inSmoothTransition && _notInFocusContainer.childCount > 0)
        {
            Debug.Log("Previous Element");
            _elementIndex--;

            if (_elementIndex < 0)
            {
                _elementIndex = MaxLimit - 1;
            }

            _elements[_elementIndex].transform.SetParent(_swipeContainer);
            _elements[_elementIndex].transform.SetAsFirstSibling();


            // Because off the way Layout groups work we need to offset the container Immediately when going to the left
            var originalPos = _swipeContainer.anchoredPosition;
            _swipeContainer.anchoredPosition += new Vector2(-1 * (_swipeDistance), 0); // Immediately offset the container

            StartCoroutine(TransistionLogic(_swipeContainer.anchoredPosition, originalPos, _easing)); // smooth transition back to original pos


        }
    }

    protected void GenerateElements()
    {
        _elements = new RectTransform[_tmpMaxElementLimit];

        for (int i = 0; i < _elements.Length; i++)
        {
            var clone = Instantiate(_swipeContainerElementPrefab, _notInFocusContainer.transform);
            clone.name = "ELEMENT " + i;
            _elements[i] = clone.GetComponent<RectTransform>();

        }
    }

    //protected abstract void AfterTrans(); // Then add per script logic
}
