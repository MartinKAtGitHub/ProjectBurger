using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : MonoBehaviour, IDropHandler
{
    public Food Food { get => _food; set => _food = value; }
    public bool IsFoodReady { get => _isFoodReady; set => _isFoodReady = value; }
    public bool OccupiedByFood { get => _occupiedByFood; set => _occupiedByFood = value; }

    [SerializeField] private GameObject _foodGameObjectPrefab;
    /// <summary>
    /// When this ingredient is added to the stack, it marks that the food is ready to be sold 
    /// </summary>
    [SerializeField] private Ingredient _foodStackFinishConditionIngredient;
    [SerializeField] private RecipeBook _recipeBook;
    [SerializeField] private bool _isFoodReady;
    [SerializeField] private bool _occupiedByFood;
    [SerializeField] private bool _occupiedByIngredient;


    /// <summary>
    /// The position/index  the food stack which will be checked against the recipes.
    /// Needs to start at -1 because of 0 index in start point
    /// </summary>
    private int _foodIngredientsIndex = -1;
    private Food _food;

    public void OnDrop(PointerEventData eventData)
    {
        if (!_occupiedByFood)
        {
            var food = eventData.pointerDrag.GetComponent<Food>();
            var draggableComponent = eventData.pointerDrag.GetComponent<DraggableIngredient>();

            if (food != null && !_occupiedByIngredient) // && Not occipied by ingredient
            {
                _occupiedByFood = true;
                food.FoodDrag.FoodCombinationDropArea = this;
                food.FoodDrag.ResetPositionParent = this.transform;
               // food.FoodDrag.FoodCombinationTransform = this.transform; 
            }
            else if (draggableComponent != null)
            {
               
                CreateFoodGameObject();

                draggableComponent.FoodCombinationDropArea = this;
                draggableComponent.ResetPositionParent = this.transform;

                var ingredientGameObject = eventData.pointerDrag.GetComponent<IngredientGameObject>(); // PERFORMANCE maybe DraggableIngredient inside of this so we dont have to getcomp on every drop

                if (ingredientGameObject != null)
                {
                    AddIngredientsToFood(ingredientGameObject);
                    CheckFoodStackWithRecepies();
                    CheckIFFinalIngredientIsPlaced(ingredientGameObject.ingredient);

                    if(_isFoodReady)
                    {
                        _occupiedByFood = true;
                        _occupiedByIngredient = false;
                        _food.FoodDrag.FoodCombinationDropArea = this;
                        _food.FoodDrag.ResetPositionParent = this.transform;
                        //_food.FoodDrag.FoodCombinationTransform = this.transform;
                        _foodIngredientsIndex = -1;
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
        _food.GameObjectIngredients.Add(ingredient);
        _foodIngredientsIndex++;

        if (_foodIngredientsIndex > -1)
        {
            _occupiedByIngredient = true;
        }
    }

    public void RemoveIngredientFromFood()
    {
        _food.GameObjectIngredients.RemoveAt(_food.GameObjectIngredients.Count - 1); // Removes the top ingredient 
        _foodIngredientsIndex--;

        if (_foodIngredientsIndex <= -1)
        {
            _occupiedByIngredient = false;
        }
    }

    private void CheckFoodStackWithRecepies()
    {
        for (int i = 0; i < _recipeBook.Recipes.Count; i++)
        {
            var currentRecipe = _recipeBook.Recipes[i];

            if (_foodIngredientsIndex < currentRecipe.Ingredients.Count)
            {
                // If(_foodStackIngredients[_foodStackCheckIndex].classType == currentRecipe.Ingredients[_foodStackCheckIndex].classType)
                if (_food.GameObjectIngredients[_foodIngredientsIndex].ingredient.IngredientType == currentRecipe.Ingredients[_foodIngredientsIndex].IngredientType)
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
            Debug.Log("Creating new Food in " + name);
            var clone = Instantiate(_foodGameObjectPrefab, transform);
            _food = clone.GetComponent<Food>();
        }
    }

    private void CheckIFFinalIngredientIsPlaced(Ingredient ingredient)
    {
        if (_food.GameObjectIngredients.Count > 1 && ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType)
        {
            _isFoodReady = true;
        }
        else
        {
            _isFoodReady = false;
        }

        //_isFoodReady = (ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType) ? true : false;
    }
}

