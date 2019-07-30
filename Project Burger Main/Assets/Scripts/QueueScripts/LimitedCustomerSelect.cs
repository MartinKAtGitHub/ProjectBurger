using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitedCustomerSelect : MonoBehaviour
{
    [SerializeField] private RectTransform _customerInteractionContainer;
    [SerializeField] private RectTransform _customerNotInFocusContainer;
    [SerializeField] private int _queueSlotIndex = 0;
    [SerializeField] private QueueSlot _queueSlotInFocus;
    [SerializeField] private float _easing;

    private float _horizontalLayoutGroupSpacing;
    private float _customerWidth;
    private float _distanceBetweenCustomers;
    private bool _inSmoothTransition;
    private QueueManager _queueManager;
    private LimitedQueueDotIndicators _limitedQueueDotIndicators;
    private FoodTrayDropArea _foodTrayDropArea;

    public int QueueSlotIndex { get => _queueSlotIndex;}
    public QueueSlot QueueSlotInFocus { get => _queueSlotInFocus; }
    public bool InSmoothTransition { get => _inSmoothTransition; }

    private delegate void SetSibling();
    private void Awake()
    {
        
        LevelManager.Instance.CustomerSelect = this;
        _queueManager = GetComponent<QueueManager>();
        _limitedQueueDotIndicators = GetComponent<LimitedQueueDotIndicators>();
       
        _horizontalLayoutGroupSpacing = _customerInteractionContainer.GetComponent<HorizontalLayoutGroup>().spacing;
        _customerWidth = _queueManager.QueueSlotPrefab.GetComponent<RectTransform>().sizeDelta.x;
        _distanceBetweenCustomers = _horizontalLayoutGroupSpacing + _customerWidth;
    }

    private void Start()
    {
        Initialize();
        _foodTrayDropArea = LevelManager.Instance.FoodTrayDropArea;
       
    }

    private void Initialize()
    {
        _limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
        SetQueueSlotInFocus(_queueSlotIndex);
        _queueManager.QueueSlots[_queueSlotIndex].transform.SetParent(_customerInteractionContainer);
    }

    private IEnumerator TransistionLogic(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inSmoothTransition = true;
        LevelManager.Instance.OrderWindow.CloseWindow();


        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _customerInteractionContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        var oldSlot = _queueSlotInFocus;

        if (oldSlot != null) // TODO CustomerSelect | SmootTransition -> BUG , when there is only 1 person left the player cant select because he gets sendt back to NotInFocusArea
        {
            oldSlot.transform.SetParent(_customerNotInFocusContainer);
            oldSlot.transform.localPosition = Vector2.zero;

          //  oldSlot.CurrentCustomer?.OrderWindow.CloseWindow();
        }

        SetQueueSlotInFocus(_queueSlotIndex);
      //  _queueSlotInFocus.CurrentCustomer?.OrderWindow.OpenWindow();

        _customerInteractionContainer.anchoredPosition = Vector2.zero;
        _inSmoothTransition = false;

        yield return null;
    }

    private void SetQueueSlotInFocus(int index/*, SetSibling setSibling*/)
    {
        if (_queueManager.QueueSlots.Length > 0)
        {
            _queueSlotInFocus = _queueManager.QueueSlots[index];

            //  Debug.Log($"Setting Customer | {_queueSlotInFocus.name} | In focus");

            if (_queueSlotInFocus.CurrentCustomer != null)
            {
                var customer = _queueSlotInFocus.CurrentCustomer;
                ChangeFoodTrayOrder(customer.Order);
            
                LevelManager.Instance.OrderWindow.OpenWindow(customer);
            }

            // _queueManager.QueueSlots[_queueSlotIndex].transform.SetParent(_customerInteractionContainer);
            // setSibling();
        }
    }
    private void ChangeFoodTrayOrder(Order order)
    {
        _foodTrayDropArea.Order = order;
    }


    public void InstaFocusSlot()
    {
       // Debug.Log("Customer spawnd on Slot in focus, forcing focus");
        SetQueueSlotInFocus(_queueSlotIndex);
    }

    public void NextCustomer()
    {
        if (!_inSmoothTransition && _customerNotInFocusContainer.childCount > 0)
        {
                _queueSlotIndex++;

            if (_queueSlotIndex > _queueManager.QueueSlots.Length - 1)
            {
                _queueSlotIndex = 0;
            }

            _limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
            _queueManager.QueueSlots[_queueSlotIndex].transform.SetParent(_customerInteractionContainer);
            _queueManager.QueueSlots[_queueSlotIndex].transform.SetAsLastSibling();

            var newPos = _customerInteractionContainer.anchoredPosition;
            newPos += new Vector2(-1 * (_distanceBetweenCustomers), 0);

            StartCoroutine(TransistionLogic(_customerInteractionContainer.anchoredPosition, newPos, _easing));

        }

    }
    public void PreviousCustomer()
    {
        if (!_inSmoothTransition && _customerNotInFocusContainer.childCount > 0)
        {
            Debug.Log("Previous Customer");
            _queueSlotIndex--;

            if (_queueSlotIndex < 0)
            {
                _queueSlotIndex = _queueManager.QueueSlots.Length - 1;
            }

            _limitedQueueDotIndicators.SetDotFocus(_queueSlotIndex);
            _queueManager.QueueSlots[_queueSlotIndex].transform.SetParent(_customerInteractionContainer);
            _queueManager.QueueSlots[_queueSlotIndex].transform.SetAsFirstSibling();

            // Because off the way Layoutgroups work we need to offset the container Immediately when going to the left
            var originalPos = _customerInteractionContainer.anchoredPosition;
            _customerInteractionContainer.anchoredPosition += new Vector2(-1 * (_distanceBetweenCustomers), 0); // Immediately offset the container

            StartCoroutine(TransistionLogic(_customerInteractionContainer.anchoredPosition, originalPos, _easing)); // smooth transition back to original pos

        }


    }


   
}
