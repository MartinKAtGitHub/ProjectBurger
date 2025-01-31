﻿using UnityEngine;
using TMPro;

public class OrderWindowFoodItemPage : MonoBehaviour
{
    [SerializeField] int _tmpMax = 6; // TODO  OrderWindowFoodItemPage.cs | Connect Max request pnls/discarded/add ingredietn limit to Manager
    [SerializeField] TextMeshProUGUI _recipeName;
    [SerializeField] Transform _specialRequestGroupTransform;
    [SerializeField] GameObject _specialRequestElement;

    private OrderWindow _orderWindow;
    private RectTransform[] _requestElements;

    public TextMeshProUGUI RecipeName { get => _recipeName; set => _recipeName = value; }

    private void Awake()
    {
        _orderWindow = GetComponentInParent<OrderWindow>();
        GenerateSpecialRequestElements();
    }

    private void GenerateSpecialRequestElements()
    {
        _requestElements = new RectTransform[_tmpMax];

        for (int i = 0; i < _tmpMax; i++)
        {
            var clone = Instantiate(_specialRequestElement, _specialRequestGroupTransform);
            _requestElements[i] = clone.GetComponent<RectTransform>();
        }
    }

    public void UpdateThisPage(OrderRecipe foodItem)
    {
        _recipeName.text = foodItem.BaseRecipe.RecipeName;

        // Update all the other shit + and - stuff
    }

    private void SetSpecialRequestFoodIngredients(OrderRecipe foodItem)
    {
        // TODO Need to rework the special request system

    }
}
