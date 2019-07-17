using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{
    [SerializeField] private GameObject _orderWindowParent;
    [SerializeField] private GameObject _orderWindowPrefab;

    private OrderWindowData _orderWindowData;
    private OrderGenerator _orderGenerator;
    private GameObject _orderWindow;
    private void Awake()
    {
        _orderGenerator = GetComponent<OrderGenerator>();
        CreateOrderWindow();
        _orderWindowParent = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    public void OpenWindow()
    {
        // Anim Fade INN Window / enable window = true
    
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

        //set sprite to REMOVE
        for (int i = 0; i < _orderGenerator.DiscaredIngredients.Count; i++)
        {
            _orderWindowData.DiscardedIngredients[i].sprite = _orderGenerator.DiscaredIngredients[i].IngredientSprite;
        }

        // Set Sprite to ADD

        // FOR loop() -> add extra ingreinds list
   }

    private void CreateOrderWindow() //TODO OrderWindow.cs | Don't need to instantiate this if every Customer has it. Just child prefab to Customer
    {
        //_orderWindow = Instantiate(_orderWindowPrefab, transform.parent);
        _orderWindow = Instantiate(_orderWindowPrefab, _orderWindowParent.transform);

        //_orderWindow.transform.SetParent(_orderWindowParent);
        _orderWindowData = _orderWindow.GetComponent<OrderWindowData>();
        _orderWindow.name = $"ORDER WINDOW {name}";
        _orderWindow.SetActive(false);

    }
}
