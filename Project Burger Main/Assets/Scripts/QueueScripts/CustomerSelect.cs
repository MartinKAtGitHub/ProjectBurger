using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Handel's the selection of customers in queue.
/// the selected customer will be the one the food is checked against
/// and any other operation needed to be done to a singular customer 
/// </summary>
public class CustomerSelect : MonoBehaviour // TODO CustomerSelect.cs | Update the script to take Customer object instead of directly ref OrderGenerator
{

    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private int _customerIndex = 0;
    [SerializeField] private Customer _customerInFocus;
    [SerializeField] private Customer _customerLostFocus;
    [SerializeField] private float _easing = 0.5f;



    private QueueManager _queueManager;
    private QueueDotIndicators _queueDotIndicators;
    private FoodTrayDropArea _foodTrayDropArea;
    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private float _distanceBetweenCustomers;
    private List<Customer> _customers;
    private List<GameObject> _placeHolderGameObjects = new List<GameObject>();
    private bool _inSmoothTransition;

    public Customer CustomerInFocus { get => _customerInFocus; }
    public int CustomerSelectIndex { get => _customerIndex; }
    public bool InSmoothTransition { get => _inSmoothTransition; }

    private void Awake()
    {
        _queueManager = GetComponent<QueueManager>();
        _queueDotIndicators = GetComponent<QueueDotIndicators>();

        _horizontalLayoutGroupSpacing = _customerInteractionContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        _customerWidth = _customerPrefab.GetComponent<RectTransform>().sizeDelta.x;
        _distanceBetweenCustomers = _horizontalLayoutGroupSpacing + _customerWidth;
    }

    private void Start()
    {
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;
        _customers = _queueManager.ActiveCustomerQueue;
    }

    /// <summary>
    /// When the active list has only 1 customer we want to force the view of the player to that customer
    /// </summary>
    public void ZeroIndexCustomer()
    {
        //   Debug.Log("Zero Index -> force customer to view");
        DeletePlaceholder();

        _customerIndex = 0;
        CreatePlaceHolder();

        _queueDotIndicators.SetDotFocus(_customerIndex);

        _customers[_customerIndex].transform.SetParent(_customerInteractionContainer);
        _customers[_customerIndex].transform.SetAsFirstSibling();

        var newPos = _customerInteractionContainer.anchoredPosition;
        _customerInteractionContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0);

        StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(_customerInteractionContainer.anchoredPosition, newPos, _easing));

    }

    IEnumerator SmoothTransitionAndSetNewCustomerFocus(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;

        var tempIndex = _customerIndex;

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _customerInteractionContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        var oldCustomer = _customerInFocus;

        if (oldCustomer != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
        {
            oldCustomer.transform.SetParent(_customerNotInFocusContainer);
            oldCustomer.transform.localPosition = Vector2.zero;

            oldCustomer.OrderWindow.CloseWindow();
            //Debug.Log($"Old customer Waiting({oldCustomer.IsWaiting})");
            //oldCustomer.CheckCustomerTimout();
        }
        else
        {
            DeletePlaceholder();
        }

        SetNewActiveCustomer(_customerIndex);
        

        _customerInteractionContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }

    private void SetNewActiveCustomer(int index)
    {
        if (_queueManager.ActiveCustomerQueue.Count > 0)
        {
            Debug.Log($"Length = {_queueManager.ActiveCustomerQueue.Count}  index = {index}  MaxIndex ={_queueManager.ActiveCustomerQueue.Count - 1}");
            var customer = _queueManager.ActiveCustomerQueue[index];
            _customerInFocus = customer;
            ChangeFoodTrayOrder(_customerInFocus.Order);
            _customerInFocus.OrderWindow.OpenWindow(_customerInFocus);
        }
        else
        {
            Debug.LogError("CustomerSelect.cs |  SetCustomerFocus () =  ActiveCustomerQueue is Empty OR enemy died before set");
        }
    }

    private void ChangeFoodTrayOrder(Order order)
    {
        _foodTrayDropArea.Order = order;
    }

    public void CircularLeft()
    {
        if (!_inSmoothTransition && _customerNotInFocusContainer.childCount > 0)
        {
            _customerIndex--;

            if (_customerIndex < 0)
            {
                _customerIndex = _customers.Count - 1;
            }

            _queueDotIndicators.SetDotFocus(_customerIndex);

            _customers[_customerIndex].transform.SetParent(_customerInteractionContainer);
            _customers[_customerIndex].transform.SetAsFirstSibling();

            var newPos = _customerInteractionContainer.anchoredPosition;
            _customerInteractionContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(_customerInteractionContainer.anchoredPosition, newPos, _easing));
        }
    }


    public void CircularRight()
    {
        if (!_inSmoothTransition && _customerNotInFocusContainer.childCount > 0)
        {
            _customerIndex++;
            //_customerIndex %= _customers.Count - 1;
            if (_customerIndex > _customers.Count - 1)
            {
                _customerIndex = 0;
            }

            _queueDotIndicators.SetDotFocus(_customerIndex);

            _customers[_customerIndex].transform.SetParent(_customerInteractionContainer);
            _customers[_customerIndex].transform.SetAsLastSibling();

            var newPos = _customerInteractionContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(_customerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

    public void OnSell()
    {
        CreatePlaceHolder();
    }

    public void OnTimeOut(Customer customer)
    {
        if (_customerInFocus == customer)
        {
            CreatePlaceHolder();
            ChangeFoodTrayOrder(null);
        }
    }
    private void CreatePlaceHolder()
    {
        Debug.Log("PlaceHOLDER created");
        var placeHolderGameObject = new GameObject("PlaceHolder For Customer");
        placeHolderGameObject.AddComponent<RectTransform>();

        placeHolderGameObject.transform.SetParent(_customerInteractionContainer.transform);

        if (_customerInFocus != null)
        {
            placeHolderGameObject.transform.SetSiblingIndex(_customerInFocus.transform.GetSiblingIndex());
        }
        else
        {
            placeHolderGameObject.transform.SetAsFirstSibling();
            // Debug.Log("NO customer in focus, creating placeholder on SetAsFirstSibling()");
        }


        var placeHolderRectTrans = placeHolderGameObject.GetComponent<RectTransform>();
        placeHolderRectTrans.localScale = Vector2.one;
        placeHolderRectTrans.sizeDelta = new Vector2(_customerWidth, 0);

        _placeHolderGameObjects.Add(placeHolderGameObject);
    }

    private void DeletePlaceholder()
    {
        if (_placeHolderGameObjects != null)
        {
            for (int i = 0; i < _placeHolderGameObjects.Count; i++)
            {
                Destroy(_placeHolderGameObjects[i]);
            }
        }
        //else
        //{
        //    Debug.Log("CustomerSelector | Placeholder is Null but you are trying to destroy it ");
        //}
    }
}



