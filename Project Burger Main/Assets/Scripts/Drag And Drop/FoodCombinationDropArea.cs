using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject _foodGameObjectPrefab;
    [SerializeField]private Transform _topLayerTrans;
    [SerializeField]private Transform _dropPoint;
    /// <summary>
    /// When this ingredient is added to the stack, it marks that the food is ready to be sold 
    /// </summary>
    [SerializeField] private Ingredient _foodStackFinishConditionIngredient;
    [SerializeField] private RecipeBook _recipeBook;
    [SerializeField] private bool _isFoodReady;
    [SerializeField] private bool _occupiedByFood;
    [SerializeField] private bool _occupiedByIngredient;
    [SerializeField] private bool _maxIngredientLimitReached;

    /// <summary>
    /// The Layer/index  the food stack which will be checked against the recipes.
    /// Needs to start at -1 because of 0 index in start point
    /// </summary>
    private int _ingredientLayer = -1;
    private static int _foodCombiSpotsAmount;
    private Food _food;
    private RectTransform _touchArea;

    
    public Food Food { get => _food; set => _food = value; }
    public bool IsFoodReady { get => _isFoodReady; set => _isFoodReady = value; }
    public bool OccupiedByFood { get => _occupiedByFood; set => _occupiedByFood = value; }
    public static int FoodCombiSpotsAmount { get => _foodCombiSpotsAmount; set => _foodCombiSpotsAmount = value; }

    private void Awake()
    {
        _foodCombiSpotsAmount++;
        _touchArea = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!_occupiedByFood)
        {
            if(_maxIngredientLimitReached)
            {
                Debug.LogError("FoodCombi.cs  _maxIngredientLimitReached can't place more ingredients notify player" );
                return;
            }

            var food = eventData.pointerDrag.GetComponent<Food>();
            var draggableIngredient = eventData.pointerDrag.GetComponent<DraggableIngredient>();

            if (food != null && !_occupiedByIngredient)
            {
                _occupiedByFood = true;
                food.FoodDrag.FoodCombinationDropArea = this;
                food.FoodDrag.ResetPositionParent = this.transform;
                // food.FoodDrag.FoodCombinationTransform = this.transform; 
            }
            else if (draggableIngredient != null)
            {
                CreateFoodGameObject();

                draggableIngredient.FoodCombinationDropArea = this;
                draggableIngredient.ResetPositionParent = this.transform;

                var ingredientGameObject = eventData.pointerDrag.GetComponent<IngredientGameObject>();

                if (ingredientGameObject != null)
                {
                    AddIngredientsToFood(ingredientGameObject);
                    ingredientGameObject.RescaleTouchArea(_touchArea);  // TODO ingredientGo.RescaleTouchArea happens in all drop areas

                    CheckFoodStackWithRecepies();
                    IsFinalIngredientPlaced(ingredientGameObject.Ingredient);

                    if (_isFoodReady)
                    {
                        _occupiedByFood = true;
                        _occupiedByIngredient = false;
                        _food.FoodDrag.FoodCombinationDropArea = this;
                        _food.FoodDrag.ResetPositionParent = this.transform;
                        //_food.FoodDrag.FoodCombinationTransform = this.transform;
                        _ingredientLayer = -1;
                    }
                }
                else
                {
                    Debug.LogError("Missing IngredientGameObject");
                }
            }
        }
        else
        {
            Debug.Log(name + " Is occupied by Food");
        }
    }

    private void AddIngredientsToFood(IngredientGameObject ingredient)
    {
        _food.IngredientsGO.Add(ingredient);
        _ingredientLayer++;
        ingredient.SetIngredientSpriteForLayer(_ingredientLayer);

        if (_ingredientLayer > -1)
        {
            _occupiedByIngredient = true;
        }

        if (_ingredientLayer >= Ingredient.MaxIngredientLayersAmount - 1)
        {
            _ingredientLayer = Ingredient.MaxIngredientLayersAmount - 1;
            _maxIngredientLimitReached = true;
        }
    }

    public void RemoveIngredientFromFood()
    {
        _food.IngredientsGO.RemoveAt(_food.IngredientsGO.Count - 1);
        _ingredientLayer--;

        if (_ingredientLayer <= -1)
        {
            _occupiedByIngredient = false;
        }

        if (_ingredientLayer < Ingredient.MaxIngredientLayersAmount - 1)
        {
            _maxIngredientLimitReached = false;
        }
      
    }

    /// <summary>
    /// Checks to see if the ingredient matches the position and type of any recipe in the recipe book
    /// If we reach the end of the function, the Warning UI will trigger and single the player that no match was found. 
    /// </summary>
    private void CheckFoodStackWithRecepies()
    {
        for (int i = 0; i < _recipeBook.Recipes.Count; i++)
        {
            var currentRecipe = _recipeBook.Recipes[i];

            if (_ingredientLayer < currentRecipe.Ingredients.Count)
            {
                // If(_foodStackIngredients[_foodStackCheckIndex].classType == currentRecipe.Ingredients[_foodStackCheckIndex].classType)
                if (_food.IngredientsGO[_ingredientLayer].Ingredient.IngredientType == currentRecipe.Ingredients[_ingredientLayer].IngredientType)
                {
                    // Found match 
                    return;
                }
            }
            else
            {
                // out of bounce recipe go to next recipe
                continue;
            }
        }
        // Maybe we cant place the final ingredient(Top bun) if it doesn't match with any recipe
        Debug.LogWarning("NO RECIPE MATCHES THE FOOD YOU ARE MAKEING (MAKE UI FOR THIS)");
    }

    private void CreateFoodGameObject() //PERFORMANCE FoodCombi Create 1 FoodStack and reuse it after each sale/delete instead of spawning a new one
    {
        if (_food == null)
        {
         //   Debug.Log("Creating new Food in " + name);
            var clone = Instantiate(_foodGameObjectPrefab, transform);
            clone.GetComponent<FoodDrag>().RenderOrderTransform = _topLayerTrans;
            _food = clone.GetComponent<Food>();
           
        }
    }

    private void IsFinalIngredientPlaced(Ingredient ingredient)
    {
        if (_food.IngredientsGO.Count > 1 && ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType)
        {
            _isFoodReady = true;
            //MakeFoodDraggable();
            //ParentIngredientsToFood();
        }
        else
        {
            _isFoodReady = false;
        }
        //_isFoodReady = (ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType) ? true : false;
    }

    public void MakeFoodDraggable()
    {
        for (int i = 0; i < _food.IngredientsGO.Count; i++)
        {
            _food.IngredientsGO[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        _food.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void ParentIngredientsToFood()
    {
        _food.ParentIngredientsToFoodObject();
    }

}

