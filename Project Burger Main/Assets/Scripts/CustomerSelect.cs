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

    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private FoodTrayDropArea _foodTrayDropArea;
    private bool _inSmoothMotion;

    public Customer CustomerInFocus { get; private set; }
    public int CustomerSelectIndex { get => _customerSelectIndex; }

    private void Awake()
    {
        QueueManager = GetComponent<QueueManager>();

        _horizontalLayoutGroupSpacing = CustomerSwipeContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        _customerWidth = CustomerPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void Initialize()
    {
        //LevelManager.Instance.CustomerSelect = this;
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;
        LevelManager.Instance.SalesManager.OnSale += ResetCustomerSelect;
    }

    public void NextCustomer()
    {
        if (!_inSmoothMotion)
        {
            if (_customerSelectIndex < QueueManager.ActiveQueueLimit.Count - 1)
            {
                _customerSelectIndex++;
                SetCustomerFocus(_customerSelectIndex);

                var newPos = CustomerSwipeContainer.anchoredPosition;
                newPos += new Vector2((_horizontalLayoutGroupSpacing + _customerWidth), 0);

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
                newPos += new Vector2(-1 * (_horizontalLayoutGroupSpacing + _customerWidth), 0);

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

    private void ResetCustomerSelect()
    {
        if (QueueManager.ActiveQueueLimit.Count == 0)
        {
            _customerFocusName.text = "No customer";
        }
        else
        {
            _customerSelectIndex = QueueManager.ActiveQueueLimit.Count - 1;
            //_customerFocusName.text = QueueManager.ActiveCustomerQueue[CustomerSelectIndex]
            //   .GetComponent<OrderGenerator>().Order.CustomerName;
        }



        Debug.Log("ResetCustomerSelect() need to set a new Selected customer on Sale");
    }


    public void CircularNextItem()
    {
        _customerSelectIndex++;
        _customerSelectIndex %= QueueManager.ActiveQueueLimit.Count; // clip index (turns to 0 if index == items.Count)

        SetCustomerFocus(_customerSelectIndex);
    }

    public void CircularPreviousItem()
    {
        _customerSelectIndex--; // decrement index

        if (_customerSelectIndex < 0)
        {
            _customerSelectIndex = QueueManager.ActiveQueueLimit.Count - 1;
        }

        SetCustomerFocus(_customerSelectIndex);
    }


    private void SetCustomerFocus(int index)
    {
        if (QueueManager.ActiveQueueLimit.Count > 0)
        {
            CustomerInFocus = QueueManager.ActiveQueueLimit[index];
            ChangeFoodTrayOrder();
        }
        else
        {
            Debug.LogError("CustomerSelect.cs |  SetCustomerFocus () =  ActiveCustomerQueue is Empty");
        }
    }


    private void ChangeFoodTrayOrder() // This runs before Start because of Levelmanager ScriptOrder priority
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
        _inSmoothMotion = false;
        yield return null;
    }
}
