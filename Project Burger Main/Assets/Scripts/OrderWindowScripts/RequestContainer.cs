using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestContainer : SlotHorizontal
{
    [SerializeField] private RectTransform _verticalSwiper;
    [SerializeField] private GameObject _requestCardPrefab;

    List<RequestCard> _requestCards = new List<RequestCard>();

    public RectTransform VerticalSwiper { get => _verticalSwiper; }

    public void GenerateRequestCardsFromOrder(Order order)
    {
        for (int i = 0; i < order.OrderRecipes.Count; i++)
        {
            var card = Instantiate(_requestCardPrefab, _verticalSwiper).GetComponent<RequestCard>();
            
            card.RecipeTitleTxt.text = order.OrderRecipes[i].BaseRecipe.name;

            _requestCards.Add(card);
        }
    }
}
