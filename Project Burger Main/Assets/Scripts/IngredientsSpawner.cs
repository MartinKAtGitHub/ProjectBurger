using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientsSpawner : MonoBehaviour/*, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    [SerializeField] private IngredientGameObject _ingredientGameObjectPrefab;
    /// <summary>
    /// UI rendering system is driven by hierarchy, so we need to place the dragged object ontop of everything -> main canvas
    /// </summary>
    [SerializeField] private Transform _topLayerTransform;
    /// <summary>
    /// The transform needed to hind the ingredient behind the bin img.
    /// </summary>
    [SerializeField] private RectTransform _ingredientPoolRect;
    /// <summary>
    /// True = spawner will spawn inn all the ingreadiance at the start of the game.
    /// False = Spawner will spawn on click and hopefully u can drag it.
    /// </summary>

    private int _poolSize;

    public IngredientGameObject IngredientPrefab { get => _ingredientGameObjectPrefab; }
    public RectTransform IngredientPoolRect { get => _ingredientPoolRect; }

    private void Awake()
    {

        if (_ingredientGameObjectPrefab == null)
        {
            Debug.LogError($"{name} has no ingredientGameobjectPrefab");
        }
    }
    private void Start()
    {
        _poolSize = Ingredient.MaxIngredientLayersAmount * FoodCombinationDropArea.FoodCombiSpotsAmount;
        GenerateMaximumAmountOfIngredients(_poolSize);
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (!_presetSpawn)
    //    {
    //        SpawnIngredient();
    //    }
    //}

    private GameObject SpawnIngredient()
    {
        var clone = Instantiate(_ingredientGameObjectPrefab.gameObject, _ingredientPoolRect.transform);
        clone.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        var draggable = clone.GetComponent<DraggableIngredient>();
        draggable.TopLayerTransform = _topLayerTransform;

        var ingredientGO = clone.GetComponent<IngredientGameObject>();
        ingredientGO.IngredientPoolTrans = _ingredientPoolRect;
        ingredientGO.RescaleTouchArea(_ingredientPoolRect);
        return clone;
    }

    private void GenerateMaximumAmountOfIngredients(int maxAmount)
    {
        for (int i = 0; i < maxAmount; i++)
        {
            SpawnIngredient();
        }
    }

}
