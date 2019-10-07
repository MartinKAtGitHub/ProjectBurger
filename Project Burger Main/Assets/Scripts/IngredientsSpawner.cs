using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientsSpawner : MonoBehaviour/*, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    [SerializeField] private IngredientGameObject _ingredientGameObjectPrefab;
    /// <summary>
    /// The transform that will determine the render order of the dragged object, think of it as sorting layer but with Transform/hierarchy
    /// </summary>
    private Transform _renderOrderTransform;
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
        _renderOrderTransform = GameObject.FindGameObjectWithTag("MainCanvas").transform;
        if (_renderOrderTransform == null)
        {
            Debug.LogError("Cant find top drag layer, make sure MainCanavs Tag is still the same");
        }

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


    private GameObject SpawnIngredient()
    {
        var ingredientClone = Instantiate(_ingredientGameObjectPrefab.gameObject, _ingredientPoolRect.transform);
        //ingredientClone.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        var draggable = ingredientClone.GetComponent<DraggableIngredient>();
        draggable.RenderOrderTransform = _renderOrderTransform;

        var ingredientGO = ingredientClone.GetComponent<IngredientGameObject>();
        ingredientGO.IngredientPoolTrans = _ingredientPoolRect;
       // ingredientGO.RescaleTouchArea(_ingredientPoolRect);
        return ingredientClone;
    }

    private void GenerateMaximumAmountOfIngredients(int maxAmount)
    {
        if(maxAmount <=0)
        {
            Debug.LogError($"Cant spawn ingredients because no food combination zones are inn the scene so the pool is -> {maxAmount}");
            return;
        }

        for (int i = 0; i < maxAmount; i++)
        {
            SpawnIngredient();
        }
    }

}
