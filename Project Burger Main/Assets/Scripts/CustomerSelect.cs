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
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private int _customerIndex = 0;
    [SerializeField] private Customer _customerInFocus;
    [SerializeField] private float _easing = 0.5f;



    private QueueManager _queueManager;
    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private FoodTrayDropArea _foodTrayDropArea;
    private bool _inSmoothTransition;
    private float _distanceBetweenCustomers;
    private List<Customer> _customers;
    private GameObject _placeHolderGameObject;

    public Customer CustomerInFocus { get => _customerInFocus; }
    public int CustomerSelectIndex { get => _customerIndex; }
    public bool InSmoothTransition { get => _inSmoothTransition; }

    private void Awake()
    {
        _queueManager = GetComponent<QueueManager>();

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
        Debug.Log("Zero Index -> force customer to view");
        _customerIndex = 0;
        SetNewActiveCustomer(_customerIndex);
        _customerInFocus.transform.SetParent(_customerInteractionContainer);
    }


    private void SetNewActiveCustomer(int index)
    {
        if (_queueManager.ActiveCustomerQueue.Count > 0)
        {
            _customerInFocus = _queueManager.ActiveCustomerQueue[index];

            ChangeFoodTrayOrder();
        }
        else
        {
            Debug.LogError("CustomerSelect.cs |  SetCustomerFocus () =  ActiveCustomerQueue is Empty");
        }
    }

    private void ChangeFoodTrayOrder()
    {
        _foodTrayDropArea.Order = _customerInFocus.Order;
    }

    IEnumerator SmoothTransitionAndSetNewCustomerFocus(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _customerInteractionContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (_customerInFocus != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select beacuse he gets  sendt back to NotInFocusArea
        {
            _customerInFocus.transform.SetParent(_customerNotInFocusContainer);
            _customerInFocus.transform.localPosition = Vector2.zero;
        }
        else
        {
            if(_placeHolderGameObject != null)
            {
                Destroy(_placeHolderGameObject);
            }
            else
            {
                Debug.LogError("CustomerSelector | Placeholder is Null but you are trying to destroy it ");
            }
        }

        SetNewActiveCustomer(_customerIndex);

        _customerInteractionContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }


    public void CircularLeft()
    {
        if (!_inSmoothTransition && _customers.Count != 0)
        {


            _customerIndex--;

            if (_customerIndex < 0)
            {
                _customerIndex = _customers.Count - 1;
             
            }

            _customers[_customerIndex].transform.SetParent(_customerInteractionContainer);
            _customers[_customerIndex].transform.SetAsFirstSibling();

            var newPos = _customerInteractionContainer.anchoredPosition;
            _customerInteractionContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(_customerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

    public void CircularRight()
    {
        if (!_inSmoothTransition && _customers.Count != 0)
        {
            _customerIndex++;
            //_customerIndex %= _customers.Count - 1;
            if(_customerIndex > _customers.Count -1)
            {
                _customerIndex = 0;
            }

            _customers[_customerIndex].transform.SetParent(_customerInteractionContainer);
            _customers[_customerIndex].transform.SetAsLastSibling();

            var newPos = _customerInteractionContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(_customerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

    public void OnSell()
    {
        if (_customers.Count > 0)
        {

        }
    }

    public void OnTimeOut(Customer customer)
    {
        if (_customerInFocus == customer)
        {
            // Debug.Log("Equal InFocus = " + _customerInFocus.name + "  ==  " + customer.name);
            CreatePlaceHolder();
        }
    }
    private void CreatePlaceHolder()
    {
        _placeHolderGameObject = new GameObject("PlaceHolder For Customer");
        _placeHolderGameObject.AddComponent<RectTransform>();

        _placeHolderGameObject.transform.SetParent(_customerInteractionContainer.transform);
        _placeHolderGameObject.transform.SetSiblingIndex(_customerInFocus.transform.GetSiblingIndex());

        var placeHolderRectTrans = _placeHolderGameObject.GetComponent<RectTransform>();
        placeHolderRectTrans.localScale = Vector2.one;
        placeHolderRectTrans.sizeDelta = new Vector2(_customerWidth, 0);
    }

}



