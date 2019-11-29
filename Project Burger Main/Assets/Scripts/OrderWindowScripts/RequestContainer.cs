using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestContainer : SlotHorizontal
{
    [SerializeField]private GameObject RequestCardPrefab;

    List<RequestCard> _requestCards = new List<RequestCard>();

    private void GenerateRequestCardsFromOrder(Order order)
    {
        for (int i = 0; i < order.OrderRecipes.Count; i++)
        {
            var card = Instantiate(RequestCardPrefab, transform);

            _requestCards.Add(card.GetComponent<RequestCard>());
        }
    }
}
