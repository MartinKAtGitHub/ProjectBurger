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
    // NEEDS A BETTER WAY OF CONNECTING PUBLICS
    public QueueManager QueueManager;
    public RectTransform CustomerSwipeContainer;
    public GameObject CustomerPrefab;


    [SerializeField] private float _easing = 0.5f;
    [SerializeField] private int _customerSelectIndex = 0;
    [SerializeField] private TextMeshProUGUI _customerFocusName;
    [SerializeField] private Customer _customerInFocus;

    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private FoodTrayDropArea _foodTrayDropArea;
    private bool _inSmoothMotion;
    private GameObject _placeHolderGameObject;

    private float _distanceBetweenCustomers; 

    public Customer CustomerInFocus { get => _customerInFocus; }
    public int CustomerSelectIndex { get => _customerSelectIndex; }

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
    }
    //public void Initialize()
    //{
    //    //LevelManager.Instance.CustomerSelect = this;
    //    _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;
    //      LevelManager.Instance.SalesManager.OnSale += ReFocusCustomerOnSell;
    //}

    public void NextCustomer()
    {
        if (!_inSmoothMotion)
        {
            if (_customerSelectIndex < QueueManager.ActiveCustomerQueue.Count - 1)
            {
                _customerSelectIndex++;
                SetCustomerFocus(_customerSelectIndex);

                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2((_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothMotion(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
            }
            else
            {
                Debug.Log("NO NEXT CUSTOMER");
            }
        }
    }

    public void PrevCustomer()
    {
        if (!_inSmoothMotion)
        {
            if (_customerSelectIndex > 0)
            {
                _customerSelectIndex--;
                SetCustomerFocus(_customerSelectIndex);
                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothMotion(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
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
        _customerSelectIndex = 0;
        SetCustomerFocus(_customerSelectIndex);
    }

    public void ReFocusCustomerOnSell()
    {
        if (QueueManager.ActiveCustomerQueue.Count != 0)
        {
            // _customerSelectIndex--;
            // Debug.Log("INDEX IS = " + _customerSelectIndex + "MaxIndex IS =" + (QueueManager.ActiveCustomerQueue.Count - 1));

            if (_customerSelectIndex - 1 == QueueManager.ActiveCustomerQueue.Count - 1)// Because the customer is removed from the list Before this check, we need to take that into account and calculate index from -1
            {
                // _customerSelectIndex--;
                Debug.Log("Edge case | Index = " + _customerSelectIndex + " MaxIndex = " + (QueueManager.ActiveCustomerQueue.Count - 1));
                //SetCustomerFocus(_customerSelectIndex);
                PrevCustomer();
            }
            else
            {

                CreatePlaceHolder();
                SetCustomerFocus(_customerSelectIndex);

                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2((_distanceBetweenCustomers), 0);

                StartCoroutine(SmoothMotion(CustomerSwipeContainer.anchoredPosition, newPos, _easing));
              
                // After Anim we want to Placeholder.stLast/firstSibling

            }
        }
        else
        {
            _customerFocusName.text = "No customer";
        }
    }





    public void CircularNextItem()
    {
        _customerSelectIndex++;
        _customerSelectIndex %= QueueManager.ActiveCustomerQueue.Count; // clip index (turns to 0 if index == items.Count)

        SetCustomerFocus(_customerSelectIndex);
    }

    public void CircularPreviousItem()
    {
        _customerSelectIndex--; // decrement index

        if (_customerSelectIndex < 0)
        {
            _customerSelectIndex = QueueManager.ActiveCustomerQueue.Count - 1;
        }

        SetCustomerFocus(_customerSelectIndex);
    }

    public void RemovePlaceHolder()
    {
        Destroy(_placeHolderGameObject);
        CustomerSwipeContainer.anchoredPosition -= new Vector2(_distanceBetweenCustomers, 0);
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

    IEnumerator SmoothMotion(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothMotion = true;
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            CustomerSwipeContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        _inSmoothMotion = false
            ;
        if(_placeHolderGameObject != null)
        {
            _placeHolderGameObject.transform.SetAsLastSibling();
            RemovePlaceHolder();
        }
        yield return null;
    }

    private void CreatePlaceHolder()
    {
        _placeHolderGameObject = new GameObject("PlaceHolder For Customer");
        _placeHolderGameObject.AddComponent<RectTransform>();

        _placeHolderGameObject.transform.SetParent(CustomerSwipeContainer.transform);
        Debug.Log("Customer in Focus SIB = " + _customerInFocus.transform.GetSiblingIndex());
        _placeHolderGameObject.transform.SetSiblingIndex(_customerInFocus.transform.GetSiblingIndex());

        var placeHolderRectTrans = _placeHolderGameObject.GetComponent<RectTransform>();
        placeHolderRectTrans.localScale = Vector2.one;
        placeHolderRectTrans.sizeDelta = _customerInFocus.GetComponent<RectTransform>().sizeDelta;
    }

}
