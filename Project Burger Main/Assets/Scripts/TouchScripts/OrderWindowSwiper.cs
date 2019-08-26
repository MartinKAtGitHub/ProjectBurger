using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderWindowSwiper : TouchSwipeController
{



    //[SerializeField] private float percentThreshold = 0.2f; // How much "force" the player has to use in order to start swiping
    ///// <summary>
    ///// The container witch will do the smooth slide lerp anim
    ///// </summary>
    //[SerializeField] private RectTransform _slidingContainer;



    //private int _pageIndex = 0;
    //private float _spacing;
    //private float _pageWidth;
    //private float _distance;
    //private bool _inSmoothTransition;
    //private OrderWindow _orderWindow;
    //private OrderWindowFoodItemPage _activePage;

    //private Vector2 _currentPos;
    //private float _easing = 0.8f;



    //private void Awake()
    //{
    //    _orderWindow = GetComponentInParent<OrderWindow>();

    //    _distance = _orderWindow.FoodItemPnlPrefab.GetComponent<RectTransform>().sizeDelta.x + _spacing;

    //}


    //private void Start()
    //{
    //    _currentPos = _slidingContainer.anchoredPosition;

    //    SetInitPage();
    //}


    /*public void OnDrag(PointerEventData eventData)
    {
        float diff = eventData.pressPosition.x - eventData.position.x;
        _slidingContainer.anchoredPosition = _currentPos - new Vector2(diff, 0);
        Debug.Log("Drag");
    }*/

    /*public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            var newPos = _currentPos;

            if (percentage > 0)
            {
                Debug.Log("NEXT");
                newPos += new Vector2(-_distance, 0);
               // NextPnl();
            }
            else if (percentage < 0)
            {
                Debug.Log("PREV");
                newPos += new Vector2(_distance, 0);
                // PrevPnl();
            }

            _slidingContainer.anchoredPosition = newPos;
            _currentPos = newPos;
        }
        else
        {
            ResetPnl();
        }
    }*/

    //private void ResetPnl()
    //{

    //    _slidingContainer.anchoredPosition = _currentPos;
    //    Debug.Log("Reset back");
    //}

    //private void PrevPnl()
    //{

    //    Debug.Log("Prev Pnl");
    //}

    //private void NextPnl()
    //{

    //    if (!_inSmoothTransition && _orderWindow.NotInFocusPnls.childCount > 0)
    //    {
    //        Debug.Log("Next Pnl");
    //        _pageIndex++;

    //        if (_pageIndex > _orderWindow.ActiveCustomer.Order.OrderRecipes.Count - 1)
    //        {
    //            _pageIndex = 0;
    //        }

    //        //_limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
    //        _orderWindow.FoodItemPnls[_pageIndex].transform.SetParent(_slidingContainer);
    //        _orderWindow.FoodItemPnls[_pageIndex].transform.SetAsLastSibling();

    //        var newPos = _slidingContainer.anchoredPosition;
    //        newPos += new Vector2(-1 * (_distance), 0);

    //        StartCoroutine(TransistionLogic(_slidingContainer.anchoredPosition, newPos, _easing));

    //    }
    //}

    private void OnCustomerChange() // move this to window
    {
        // Fade out Orderwin text
        // Update all the pnls with Order/ foodItems from this new customer
        // And reset the index back 0(start to swipe from 0)
        // Fade back inn
    }

    //private IEnumerator TransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    //{
    //    _inSmoothTransition = true;
    //   // LevelManager.Instance.OrderWindow.CloseWindow();


    //    float t = 0f;
    //    while (t <= 1.0)
    //    {
    //        t += Time.deltaTime / sec;
    //        _slidingContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
    //        yield return null;
    //    }

    //    var oldSlot = _activePage;

    //    if (oldSlot != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
    //    {
    //        oldSlot.transform.SetParent(_slidingContainer);
    //        oldSlot.transform.localPosition = Vector2.zero;
    //    }

    //    _activePage = _orderWindow.FoodItemPnls[_pageIndex];
     

    //    _slidingContainer.anchoredPosition = Vector2.zero;
    //    _inSmoothTransition = false;

    //    yield return null;
    //}

    private void SetInitPage()
    {
        //var initialFoodPnl = _orderWindow.FoodItemPnls[0];
        //_activePage = initialFoodPnl;
        //initialFoodPnl.transform.SetParent(_slidingContainer);
    }
}
