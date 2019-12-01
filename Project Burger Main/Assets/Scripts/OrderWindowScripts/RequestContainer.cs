using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestContainer : SlotHorizontal
{
    [SerializeField]private RectTransform _verticalSwiper;
    [SerializeField]private GameObject _requestCardPrefab;

    List<RequestCard> _requestCards = new List<RequestCard>();

    public RectTransform VerticalSwiper { get => _verticalSwiper; }

    private void GenerateRequestCardsFromOrder(Order order)
    {
        for (int i = 0; i < order.OrderRecipes.Count; i++)
        {
            var card = Instantiate(_requestCardPrefab, transform);

            _requestCards.Add(card.GetComponent<RequestCard>());
        }
    }
}
