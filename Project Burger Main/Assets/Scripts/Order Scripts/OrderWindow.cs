using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{
    [SerializeField]
    private GameObject _orderWindowPrefab;

    private OrderWindowData _orderWindowData;
    private OrderGenerator _orderGenerator;

    private GameObject _orderWindow;
    private void Awake()
    {
        _orderGenerator = GetComponent<OrderGenerator>();
        CreateOrderWindow();
    }

    public void OpenWindow()
    {
        // Fade INN Window / enable window = true
        UpdateUI();
        _orderWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        // Fade OUT Window / enable window = false
        //set all to null
        _orderWindow.SetActive(false);
       // Destroy(_orderWindow);
    }

    public void UpdateUI()
    {
        _orderWindowData.RecipeNameText.text = _orderGenerator.OrderBaseRecipe.RecipeName;
        _orderWindowData.RecipeImg.sprite = _orderGenerator.OrderBaseRecipe.RecipeImg;

        for (int i = 0; i < _orderGenerator.DiscaredIngredients.Count; i++)
        {
            _orderWindowData.DiscardedIngredients[i].sprite = _orderGenerator.DiscaredIngredients[i].IngredientSprite;
        }
    }

    private void CreateOrderWindow()
    {
        _orderWindow = Instantiate(_orderWindowPrefab,transform.parent);

        _orderWindowData = _orderWindow.GetComponent<OrderWindowData>();
        _orderWindow.SetActive(false);

    }
}
