using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : MonoBehaviour, IDropHandler
{
    public Food Food { get { return _food; } }
    public bool IsFoodReady { get { return _isFoodReady; } }

    [SerializeField] private GameObject _foodGameObjectPrefab;
    /// <summary>
    /// When this ingredient is added to the stack, it marks that the food is ready to be sold 
    /// </summary>
    [SerializeField] private Ingredient _foodStackFinishConditionIngredient;
    [SerializeField] private RecipeBook _recipeBook;

    //private List<Ingredient> _foodStackIngredients = new List<Ingredient>();

    /// <summary>
    /// The position/index  the food stack which will be checked against the recipes.
    /// Needs to start at -1 because of 0 index in start point
    /// </summary>
    private int _foodIngredientsIndex = -1;
    private Food _food;
    [SerializeField] private bool _isFoodReady;

    public void OnDrop(PointerEventData eventData)
    {
        if (!_isFoodReady)
        {
            // var draggedObject = eventData.pointerDrag;
            var draggableComponent = eventData.pointerDrag.GetComponent<DraggableIngredient>();

            if (draggableComponent != null)
            {
                CreateFoodStackGameObject();
               // draggableComponent.OnFoodCombiDropArea = true;
                draggableComponent.FoodCombinationDropArea = this;
                draggableComponent.CurrentParent = this.transform;

                var ingredientGameObject = eventData.pointerDrag.GetComponent<IngredientGameObject>(); // PERFORMANCE maybe DraggableIngredient inside of this so we dont have to getcomp on every drop

                if (ingredientGameObject != null)
                {
                    AddIngredientsToFood(ingredientGameObject);
                    CheckFoodStackWithRecepies();
                    CheckIFFinalIngredientIsPlaced(ingredientGameObject.ingredient);

                }
                else
                {
                    Debug.LogError("Missing IngredientGameObject");
                }
            }
        }
        else
        {
            Debug.LogWarning("Food is ready to be sold, please sell or delete(trash) the foodstack to clear the FoodCombi plate");
        }
    }

    public void DropAreaOnBeginDrag()
    {

        RemoveIngredientFromFoodStack();
    }

    private void AddIngredientsToFood(IngredientGameObject ingredient)
    {
        _food.GameObjectIngredients.Add(ingredient);
        _foodIngredientsIndex++;
        //  Debug.Log("Add stack" + _foodStackCheckIndex);

    }

    private void RemoveIngredientFromFoodStack()
    {
        _food.GameObjectIngredients.RemoveAt(_food.GameObjectIngredients.Count - 1); // Removes the top ingredient 
        _foodIngredientsIndex--;
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

    private void CreateFoodStackGameObject() //PERFORMANCE FoodCombi Create 1 FoodStack and reuse it after each sale/delete instead of spawning a new one
    {
        if (_food == null)
        {
            var clone = Instantiate(_foodGameObjectPrefab, transform);
            _food = clone.GetComponent<Food>();
        }
    }

    private void CheckIFFinalIngredientIsPlaced(Ingredient ingredient)
    {
        if (ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType)
        {
            _isFoodReady = true;
            //_food.CreateFoodGameObjectWithIngredients(); // Since OnEndDrag is run after this, the last obj will get different parent
        }
        else
        {
            _isFoodReady = false;
        }

        //_isFoodReady = (ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType) ? true : false;
    }
}

