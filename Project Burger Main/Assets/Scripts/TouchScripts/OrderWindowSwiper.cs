using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderWindowSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _foodItemPnl;
    [SerializeField] private int _tmpMax = 6;
    [SerializeField] private float percentThreshold = 0.2f; // How much "force" the player has to use in order to start swiping
    /// <summary>
    /// The container witch will do the smooth slide lerp anim
    /// </summary>
    [SerializeField] private RectTransform _slidingContainer;
    /// <summary>
    /// Holds the pnls with the information on fooditem which are not in focus from the player.
    /// </summary>
    [SerializeField] private RectTransform _notInFocusPnls; 
    [SerializeField] private OrderWindowFoodItemPage[] foodItemPnls;

    private int _pageIndex = 0;
    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private float _distanceBetweenCustomers;
    private bool _inSmoothTransition;
    private OrderWindow _orderWindow;
    private OrderWindowFoodItemPage _activePage;

    private Vector3 startPos;

    private void Awake()
    {
        _orderWindow = GetComponentInParent<OrderWindow>();
        GenerateFoodItemPnls();
    }

    private void Start()
    {
        startPos = _slidingContainer.transform.position;
        Debug.LogError(startPos + "Need to use RECT Trans i dont think we should use world position in UI");
    }


    public void OnDrag(PointerEventData eventData)
    {
        float diff = eventData.pressPosition.x - eventData.position.x;
        _slidingContainer.transform.position = startPos - new Vector3(diff, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            if (percentage > 0)
            {
                NextPnl();
            }
            else if (percentage < 0)
            {
                PrevPnl();
            }
        }
        else
        {

            ResetPnl();
        }
    }

    private void ResetPnl()
    {
        Debug.Log("Reset back");
    }

    private void PrevPnl()
    {
        Debug.Log("Prev Pnl");

    }

    private void NextPnl()
    {

        if (!_inSmoothTransition && _notInFocusPnls.childCount > 0)
        {
            Debug.Log("Next Pnl");
            _pageIndex++;

            if (_pageIndex > _orderWindow.ActiveCustomer.Order.OrderRecipes.Count - 1)
            {
                _pageIndex = 0;
            }

            //_limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
            foodItemPnls[_pageIndex].transform.SetParent(_slidingContainer);
            foodItemPnls[_pageIndex].transform.SetAsLastSibling();

            //var newPos = _customerInteractionContainer.anchoredPosition;
            //newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            //StartCoroutine(TransistionLogic(_customerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

    private void OnCustomerChange() // move this to window
    {
        // Fade out Orderwin text
        // Update all the pnls with Order/ foodItems from this new customer
        // And reset the index back 0(start to swipe from 0)
        // Fade back inn
    }

    private IEnumerator TransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;
        LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _slidingContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        var oldSlot = _activePage;

        if (oldSlot != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
        {
            oldSlot.transform.SetParent(_slidingContainer);
            oldSlot.transform.localPosition = Vector2.zero;
        }

        _activePage = foodItemPnls[_pageIndex];
     

        _slidingContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }


    private void GenerateFoodItemPnls()
    {
        foodItemPnls = new OrderWindowFoodItemPage[_tmpMax]; // TODO OrderwinSipe.cs | Connect the max limit to Manager so its not hardcoded
        for (int i = 0; i < _tmpMax; i++)
        {
            var clone = Instantiate(_foodItemPnl, _notInFocusPnls.transform);
            foodItemPnls[i] = clone.GetComponent<OrderWindowFoodItemPage>();
        }
    }
}
