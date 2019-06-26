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
    public RectTransform CustomerSwipeContainer;
    public GameObject CustomerPrefab;

    [SerializeField] private GameObject _customerQueueSpotPrefab;
    [SerializeField] private int _queueSpotIndex = 0;
    [SerializeField] private int _positionIndex = 0;
    [SerializeField] private float _easing = 0.5f;
    [SerializeField] private TextMeshProUGUI _customerFocusName;
    [SerializeField] private Customer _customerInFocus;
    [SerializeField] private List<GameObject> _customerQueueSpots = new List<GameObject>();

    private int _loopCounter = 0;
    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private FoodTrayDropArea _foodTrayDropArea;
    private bool _inSmoothTransition;
    private GameObject _animPlaceHolderGameObject;
    private List<GameObject> _placeHolderList = new List<GameObject>();
    private float _distanceBetweenCustomers;

    public Customer CustomerInFocus { get => _customerInFocus; }
    public int CustomerSelectIndex { get => _queueSpotIndex; }
    public bool InSmoothTransition { get => _inSmoothTransition; }

    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();

        _horizontalLayoutGroupSpacing = CustomerSwipeContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        _customerWidth = CustomerPrefab.GetComponent<RectTransform>().sizeDelta.x;

        _distanceBetweenCustomers = _horizontalLayoutGroupSpacing + _customerWidth;
    }

    private void Start()
    {
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;

        GenerateCustomerQueueSpots();
    }


    public void NextCustomer()
    {
        if (!_inSmoothTransition)
        {
            if (_queueSpotIndex < QueueManager.ActiveCustomerQueue.Count - 1)
            {
                _queueSpotIndex++;
                SetCustomerFocus(_queueSpotIndex);

                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2((_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
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
            if (_queueSpotIndex > 0)
            {
                _queueSpotIndex--;
                SetCustomerFocus(_queueSpotIndex);
                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
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
        _queueSpotIndex = 0;
        SetCustomerFocus(_queueSpotIndex);
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
            var clone = Instantiate(_customerQueueSpotPrefab, CustomerSwipeContainer);
            clone.name = $"Spot {i + 1}";
            _customerQueueSpots.Add(clone);
        }
    }


    private void SetCustomerFocus(int index)
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

    IEnumerator SmoothTransitionAnim(Vector2 startPos, Vector2 endPos, float sec)
    {

        _inSmoothTransition = true;
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            CustomerSwipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _inSmoothTransition = false;


        //  RemoveAnimPlaceHolder();


        yield return null;
    }


    //private void RemoveAnimPlaceHolder()
    //{
    //    if(_animPlaceHolderGameObject != null)
    //    {
    //        Debug.Log("ANIM REMOVE");
    //       // _animPlaceHolderGameObject.transform.SetAsLastSibling();
    //        Destroy(_animPlaceHolderGameObject);
    //        CustomerSwipeContainer.anchoredPosition -= new Vector2(_distanceBetweenCustomers, 0);
    //    }
    //}

    //private void CreateAnimPlaceHolder(Customer customer)
    //{
    //    _animPlaceHolderGameObject = new GameObject();
    //    _animPlaceHolderGameObject.AddComponent<RectTransform>();

    //    _animPlaceHolderGameObject.transform.SetParent(CustomerSwipeContainer.transform);
    //    // Debug.Log("Customer in Focus = " + _customerInFocus.transform.GetSiblingIndex());
    //    _animPlaceHolderGameObject.transform.SetSiblingIndex(customer.transform.GetSiblingIndex());
    //    _animPlaceHolderGameObject.name = $" Placeholder ({customer.name})";

    //    var placeHolderRectTrans = _animPlaceHolderGameObject.GetComponent<RectTransform>();
    //    placeHolderRectTrans.localScale = Vector2.one;
    //    placeHolderRectTrans.sizeDelta = customer.GetComponent<RectTransform>().sizeDelta;

    //}


    //private void CreatePlaceHolder(Customer customer)
    //{
    //    var placeHolderGameObject = new GameObject();
    //    placeHolderGameObject.AddComponent<RectTransform>();

    //    placeHolderGameObject.transform.SetParent(CustomerSwipeContainer.transform);
    //    // Debug.Log("Customer in Focus = " + _customerInFocus.transform.GetSiblingIndex());
    //    placeHolderGameObject.transform.SetSiblingIndex(customer.transform.GetSiblingIndex());
    //    placeHolderGameObject.name = $" Placeholder ({customer.name})";

    //    var placeHolderRectTrans = placeHolderGameObject.GetComponent<RectTransform>();
    //    placeHolderRectTrans.localScale = Vector2.one;
    //    placeHolderRectTrans.sizeDelta = customer.GetComponent<RectTransform>().sizeDelta;

    //    _placeHolderList.Add(placeHolderGameObject);
    //}

    public void CircularLeft()
    {
        if (!_inSmoothTransition)
        {


            _queueSpotIndex--;
            _positionIndex--;

            //  if (_positionIndex < 0)
            if (_queueSpotIndex < 0)
            {
                _positionIndex = 0;
                _loopCounter++;

                _queueSpotIndex = _customerQueueSpots.Count - _loopCounter;
                _customerQueueSpots[_queueSpotIndex].transform.SetAsFirstSibling();

                if (_queueSpotIndex == 0)
                {
                    _loopCounter = 0;
                }

                //_customerSelectIndex = _customerQueueSpots.Count - 1; // reduce 1 ith 1 unitl it beacuse 0 then reset to count
                //_customerQueueSpots[_customerQueueSpots.Count - 1].transform.SetAsFirstSibling();

                CustomerSwipeContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0);
            }

            // SetCustomerFocus(_customerSelectIndex);

            var newPos = CustomerSwipeContainer.anchoredPosition;
            newPos += new Vector2((_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));

        }
    }

    public void CircularRight()
    {
        if (!_inSmoothTransition)
        {
            _queueSpotIndex++;
            _positionIndex++;

            _queueSpotIndex %= _customerQueueSpots.Count;

            if (_positionIndex >= _customerQueueSpots.Count)
            {
                _positionIndex = _customerQueueSpots.Count - 1;
                _customerQueueSpots[_queueSpotIndex].transform.SetAsLastSibling();

                if (_queueSpotIndex == 0)
                {


                    //CustomerSwipeContainer.anchoredPosition = new Vector2(0, 0);
                    //_customerQueueSpots[_queueSpotIndex].transform.SetAsFirstSibling();
                    //_positionIndex = 0;
                }

                CustomerSwipeContainer.anchoredPosition += new Vector2((_distanceBetweenCustomers), 0);
            }

            // SetCustomerFocus(_customerSelectIndex);

            var newPos = CustomerSwipeContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(SmoothTransitionAnim(CustomerSwipeContainer.anchoredPosition, newPos, _easing));

        }

    }


}
