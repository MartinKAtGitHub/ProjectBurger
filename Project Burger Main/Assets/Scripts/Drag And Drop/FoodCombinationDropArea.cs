using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : DropArea
{
    [SerializeField]
    private GameObject _foodStackPrefab;
    /// <summary>
    /// When this ingredient is added to the stack, it marks that the food is ready to be sold 
    /// </summary>
    [SerializeField]
    private Ingredient _foodStackFinishConditionIngredient;
    [SerializeField]
    private RecipeBook _recipeBook;

    //private List<Ingredient> _foodStackIngredients = new List<Ingredient>();

    /// <summary>
    /// The position/index  the food stack which will be checked against the recipes.
    /// Needs to start at -1 because of 0 index in start point
    /// </summary>
    private int _foodStackCheckIndex = -1;
    private FoodStack _foodStack;
    private bool _isFoodReady;

    public FoodStack FoodStack { get { return _foodStack; } }
    public bool IsFoodReady { get { return _isFoodReady; } }
    public override void OnDrop(PointerEventData eventData)
    {
        if (!_isFoodReady) // Maybe also check if it is a food ingredient draggable
        {
            //base.OnDrop(eventData);
            CreateFoodStackGameObject();

            var draggedObject = eventData.pointerDrag;

            var draggableComponent = draggedObject.GetComponent<DraggableIngredient>();
            if (draggableComponent != null)
            {
                draggableComponent.DropAreaTransform = this.transform;
                draggableComponent.OnDropArea = true;
                draggableComponent.FoodCombinationDropArea = this;
                draggableComponent.DropAreaTransform = _foodStack.transform;
            }


            var ingredientGameObject = eventData.pointerDrag.GetComponent<IngredientGameObject>();
            if (ingredientGameObject != null)
            {
                AddIngredientsToFoodStack(ingredientGameObject);
                CheckFoodStackWithRecepies();
                IsFinalIngredientPlaced(ingredientGameObject.ingredient);

                //if(_isFoodReady)
                //{
                //    OnFoodIsReady();
                //}
            }
            else
            {
                Debug.LogError("Drag object is not a ingredient OR you are missing component");
            }
        }
        else
        {
            Debug.LogWarning("Food is ready to be sold, please sell or delete(trash) the foodstack to clear the FoodCombi plate");
        }
    }

    public override void DropAreaOnBeginDrag()
    {
        RemoveIngredientFromFoodStack();
        _foodStackCheckIndex--;
        Debug.Log( "Pull stack"+ _foodStackCheckIndex);
    }

    private void AddIngredientsToFoodStack(IngredientGameObject ingredient)
    {
        _foodStack.FoodStackIngredients.Add(ingredient);
        _foodStackCheckIndex++;
        Debug.Log("Add stack" + _foodStackCheckIndex);

    }

    private void RemoveIngredientFromFoodStack()
    {
        _foodStack.FoodStackIngredients.RemoveAt(_foodStack.FoodStackIngredients.Count - 1); // Removes the top ingredient 
    }

    private void CheckFoodStackWithRecepies()
    {
        for (int i = 0; i < _recipeBook.Recipes.Count; i++)
        {
            var currentRecipe = _recipeBook.Recipes[i];

            if (_foodStackCheckIndex <= currentRecipe.Ingredients.Count)
            {
                // If(_foodStackIngredients[_foodStackCheckIndex].classType == currentRecipe.Ingredients[_foodStackCheckIndex].classType)
                if (_foodStack.FoodStackIngredients[_foodStackCheckIndex].ingredient.IngredientType == currentRecipe.Ingredients[_foodStackCheckIndex].IngredientType)
                {
                    return;
                }
            }
            else
            {
                continue; // Go to next recipe
            }
        }
        // Maybe we cant place the final ingredient(Top bun) if it doesn't match with any recipe
        Debug.LogWarning("NO RECIPE MATCHES THE FOOD YOU ARE MAKEING (MAKE UI FOR THIS)");
    }

    private void CreateFoodStackGameObject() //TODO FoodCombi Create 1 FoodStack and reuse it after each sale/delete instead of spawning a new one
    {
        if (_foodStack == null)
        {
            var clone = Instantiate(_foodStackPrefab, transform);
            _foodStack = clone.GetComponent<FoodStack>();
        }
    }

    private void IsFinalIngredientPlaced(Ingredient ingredient)
    {
        _isFoodReady = (ingredient.IngredientType == _foodStackFinishConditionIngredient.IngredientType) ? true : false;
    }

    //private void OnFoodIsReady()
    //{
    //    for (int i = 0; i < _foodStack.FoodStackIngredients.Count; i++)
    //    {
    //        _foodStack.FoodStackIngredients[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    }
    //    _foodStack.GetComponent<CanvasGroup>().blocksRaycasts = true;
    //}
}

