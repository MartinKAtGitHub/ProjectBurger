using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Handles the order window, assigns data and controls animations
/// </summary>
public class OrderWindow : MonoBehaviour // TODO place OrderWindow per Customer ?
{

    /// <summary>
    /// text object dedicated to holding the name of the recipe
    /// </summary>
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private TextMeshProUGUI _OrderPriceText;
    /// <summary>
    /// Image object which holds the finished food image of a recipe
    /// </summary>
    [SerializeField] private Image _recipeImg;
    /// <summary>
    /// Array of Image objects dedicated to displaying discarded ingredients
    /// </summary>
    [SerializeField] private Image[] _discardedIngredients;

    private GameObject _orderWindow;
    private Customer _activeCustomer;

    public TextMeshProUGUI RecipeNameText { get { return _recipeNameText; } }
    public Image RecipeImg { get { return _recipeImg; } }
    public Image[] DiscardedIngredients { get { return _discardedIngredients; } }

    public Customer ActiveCustomer { get => _activeCustomer;}

    private void Awake()
    {
        LevelManager.Instance.OrderWindow = this;
        _orderWindow = this.gameObject;
        //_orderWindowParent = GameObject.FindGameObjectWithTag("MainCanvas");
        // CreateOrderWindow();
    }

    public void OpenWindow(Customer customer)
    {
        // Anim Fade INN Window / enable window = true
        //UpdateUI(customer);
        _activeCustomer = customer;
        _orderWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        // Fade OUT Window / enable window = false
        //set all to null
        _orderWindow.SetActive(false);
        // Destroy(_orderWindow);
    }

    public void UpdateUI(Customer customer)
    {
       // _OrderPriceText.text = customer.Order.PriceTotal.ToString();

        // for( order.orderrecipes)
        //set all the text and shit



        //   _recipeNameText.text = customer.OrderGenerator.OrderBaseRecipe.RecipeName;
        // _recipeImg.sprite = customer.OrderGenerator.OrderBaseRecipe.RecipeImg;
    }

    //private void CreateOrderWindow() //TODO OrderWindow.cs | Don't need to instantiate this if every Customer has it. Just child prefab to Customer
    //{
    //    //_orderWindow = Instantiate(_orderWindowPrefab, transform.parent);
    //    _orderWindow = Instantiate(_orderWindowPrefab, _orderWindowParent.transform);

    //    //_orderWindow.transform.SetParent(_orderWindowParent);
    //    _orderWindowData = _orderWindow.GetComponent<OrderWindowData>();
    //    _orderWindow.name = $"ORDER WINDOW {name}";
    //    _orderWindow.SetActive(false);

    //}
}
