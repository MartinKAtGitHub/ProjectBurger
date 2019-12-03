using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{

    [SerializeField]private GameObject _requestCardsContainerPrefab;

    private OrderWindowSwiper _orderWindowSwiper;
    private RequestContainer[] _requestContainers;

    public RequestContainer[] RequestContainers { get => _requestContainers;}
    private void Awake()
    {
        LevelManager.Instance.OrderWindow = this;
        _orderWindowSwiper = GetComponent<OrderWindowSwiper>();

        GenerateOrderSlots();
    }

    private void GenerateOrderSlots()
    {
        _requestContainers = new RequestContainer[LevelManager.Instance.QueueManager.ActiveQueueLimit];

        for (int i = 0; i < _requestContainers.Length; i++ )
        {
            var orderSlot = Instantiate(_requestCardsContainerPrefab, _orderWindowSwiper.HorizontalSwipeContainer);
            _requestContainers[i] = orderSlot.GetComponent<RequestContainer>();
        }
    }


    public void UpdateOrderDisplayUI(Order order, int slotIndex)
    {
        _requestContainers[slotIndex].GenerateRequestCardsFromOrder(order);
    }
}
