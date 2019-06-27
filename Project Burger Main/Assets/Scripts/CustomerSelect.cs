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
    public QueueManager QueueManager;
    public RectTransform CustomerInteractionContainer;
    public GameObject CustomerPrefab;

    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private GameObject _customerQueueSpotPrefab;
    [SerializeField] private int _customerIndex = 0;
    [SerializeField] private Customer _customerInFocus;
    //[SerializeField] private int _positionIndex = 0;
    [SerializeField] private float _easing = 0.5f;
    [SerializeField] private TextMeshProUGUI _customerFocusName;
    [SerializeField] private List<GameObject> _customerQueueSpots = new List<GameObject>();

    private int _loopCounter = 0;
    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private FoodTrayDropArea _foodTrayDropArea;
    private bool _inSmoothTransition;
    private GameObject _animPlaceHolderGameObject;
    private List<GameObject> _placeHolderList = new List<GameObject>();
    private float _distanceBetweenCustomers;
    private List<Customer> _customers;


    public Customer CustomerInFocus { get => _customerInFocus; }
    public int CustomerSelectIndex { get => _customerIndex; }
    public bool InSmoothTransition { get => _inSmoothTransition; }

    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();

        _horizontalLayoutGroupSpacing = CustomerInteractionContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        _customerWidth = CustomerPrefab.GetComponent<RectTransform>().sizeDelta.x;

        _distanceBetweenCustomers = _horizontalLayoutGroupSpacing + _customerWidth;
    }

    private void Start()
    {
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;

        _customers = LevelManager.Instance.QueueManager.ActiveCustomerQueue;
        // GenerateCustomerQueueSpots();
    }


    public void NextCustomer()
    {
        if (!_inSmoothTransition)
        {
            if (_customerIndex < QueueManager.ActiveCustomerQueue.Count - 1)
            {
                _customerIndex++;
                SetNewActiveCustomer(_customerIndex);

                var newPos = CustomerInteractionContainer.anchoredPosition;
                newPos += new Vector2((_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(CustomerInteractionContainer.anchoredPosition, newPos, _easing));
            }
            else
            {
                Debug.Log("NO NEXT CUSTOMER");
            }
        }
    }

    public void PrevCustomer()
    {
        if (!_inSmoothTransition)
        {
            if (_customerIndex > 0)
            {
                _customerIndex--;
                SetNewActiveCustomer(_customerIndex);
                var newPos = CustomerInteractionContainer.anchoredPosition;
                newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(CustomerInteractionContainer.anchoredPosition, newPos, _easing));
            }
            else
            {
                Debug.Log("NO BACK CUSTOMER");
            }
        }
    }

    /// <summary>
    /// When the game starts, the first person to come to the queue will be auto selected for the player.
    /// This method attaches the first customer to the selector
    /// </summary>
    public void SetInitialCustomer()
    {
        _customerIndex = 0;
        SetNewActiveCustomer(_customerIndex);
        _customerInFocus.transform.SetParent(CustomerInteractionContainer);
    }



    //public void ReFocusCustomerOnSell()
    //{
    //    if (QueueManager.ActiveCustomerQueue.Count != 0)
    //    {
    //        if (_customerSelectIndex - 1 == QueueManager.ActiveCustomerQueue.Count - 1)// Because the customer is removed from the list Before this check, we need to take that into account and calculate index from -1
    //        {
    //            // If you sell while at the Start(Left edge) of the queue
    //            // Debug.Log("Edge case | Index = " + _customerSelectIndex + " MaxIndex = " + (QueueManager.ActiveCustomerQueue.Count - 1));
    //            PrevCustomer();
    //        }
    //        else
    //        {
    //            CreateAnimPlaceHolder(_customerInFocus);
    //            SetCustomerFocus(_customerSelectIndex);

    //            var newPos = CustomerSwipeContainer.anchoredPosition;
    //            newPos += new Vector2((_distanceBetweenCustomers), 0);

    //            StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
    //        }
    //    }
    //    else
    //    {
    //        _customerFocusName.text = "No customer";
    //    }
    //}

    //public void ReFocusCustomerOnTimeOut(Customer customer)
    //{
    //    var indexOfCustomer = QueueManager.ActiveCustomerQueue.IndexOf(customer);
    //    // Debug.Log("ReFocusCustomerOnTimeOut ()|  Name = " + customer.name +" IndexOf = " + indexOfCustomer );

    //    if (indexOfCustomer < _customerSelectIndex)
    //    {
    //        Debug.Log("Timeout customer  REMOVE|  Name = " + customer.name + " IndexOf = " + indexOfCustomer);
    //        //CreatePlaceHolder(customer);
    //        CustomerSwipeContainer.anchoredPosition -= new Vector2(_distanceBetweenCustomers, 0);

    //    }
    //    else if (indexOfCustomer == _customerSelectIndex)
    //    {
    //        Debug.Log("Timeout ANIM customer |  Name = " + customer.name + " IndexOf = " + indexOfCustomer);

    //        CreateAnimPlaceHolder(customer);
    //        SetCustomerFocus(_customerSelectIndex);

    //        var newPos = CustomerSwipeContainer.anchoredPosition;
    //        newPos += new Vector2((_distanceBetweenCustomers), 0);

    //        StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
    //    }
    //}


    private void GenerateCustomerQueueSpots()
    {

        for (int i = 0; i < QueueManager.ActiveQueueLimit; i++)
        {
            var clone = Instantiate(_customerQueueSpotPrefab, CustomerInteractionContainer);
            clone.name = $"Spot {i + 1}";
            _customerQueueSpots.Add(clone);
        }
    }


    private void SetNewActiveCustomer(int index)
    {
        if (QueueManager.ActiveCustomerQueue.Count > 0)
        {
            _customerInFocus = QueueManager.ActiveCustomerQueue[index];

            ChangeFoodTrayOrder();
        }
        else
        {
            Debug.LogError("CustomerSelect.cs |  SetCustomerFocus () =  ActiveCustomerQueue is Empty");
        }
    }

    private void ChangeFoodTrayOrder()
    {
        _foodTrayDropArea.Order = CustomerInFocus.Order;
    }

    IEnumerator SmoothTransitionAndSetNewCustomerFocus(Vector2 startPos, Vector2 endPos, float sec)
    {

        _inSmoothTransition = true;
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            CustomerInteractionContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _customerInFocus.transform.SetParent(_customerNotInFocusContainer);
        _customerInFocus.transform.localPosition = Vector2.zero;
        SetNewActiveCustomer(_customerIndex);

        CustomerInteractionContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }


    public void CircularLeft()
    {
        if (!_inSmoothTransition && _customers.Count > 1)
        {


            _customerIndex--;

            if (_customerIndex < 0)
            {
                _customerIndex = _customers.Count - 1;
                Debug.Log("LOOPING AROUND");
            }

            _customers[_customerIndex].transform.SetParent(CustomerInteractionContainer);
            _customers[_customerIndex].transform.SetAsFirstSibling();

            var newPos = CustomerInteractionContainer.anchoredPosition;
            CustomerInteractionContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(CustomerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

    public void CircularRight()
    {
        if (!_inSmoothTransition && _customers.Count > 1)
        {
            _customerIndex++;
            _customerIndex %= _customers.Count - 1;
            _customers[_customerIndex].transform.SetParent(CustomerInteractionContainer);
            _customers[_customerIndex].transform.SetAsLastSibling();

            var newPos = CustomerInteractionContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAndSetNewCustomerFocus(CustomerInteractionContainer.anchoredPosition, newPos, _easing));

        }
    }

}



