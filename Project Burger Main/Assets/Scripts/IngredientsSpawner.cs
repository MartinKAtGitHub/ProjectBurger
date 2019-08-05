using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientsSpawner : MonoBehaviour, IPointerDownHandler/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    [SerializeField] private GameObject _ingredientPrefab;
    /// <summary>
    /// UI rendering system is driven by hierarchy, so we need to place the dragged object ontop of everything 
    /// </summary>
    [SerializeField] private Transform _topLayerTransform;
    /// <summary>
    /// The transform needed to hind the ingredient behind the bin img.
    /// </summary>
    [SerializeField] private Transform _ingredientPoolTrans;
    /// <summary>
    /// True = spawner will spawn inn all the ingreadiance at the start of the game.
    /// False = Spawner will spawn on click and hopefully u can drag it.
    /// </summary>
    [SerializeField] private bool _presetSpawn;

    private int _poolSize;

    public GameObject IngredientPrefab { get => _ingredientPrefab; }
    private void Awake()
    {

        if (_ingredientPrefab == null)
        {
            Debug.LogError($"{name} has no ingredientPrefab");
        }
        else
        {
            if (_ingredientPrefab.GetComponent<IngredientGameObject>() == null)
            {
                Debug.LogError($"{name} has wrong prefab -> not an ingredient make sure IngredientGameObject is on prefab");
            }
        }

    }
    private void Start()
    {

        _poolSize = Ingredient.MaxIngredientLayersAmount * FoodCombinationDropArea.FoodCombiSpotsAmount;

        if (_presetSpawn)
        {
            for (int i = 0; i < _poolSize; i++)
            {
                SpawnIngredient();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_presetSpawn)
        {
            SpawnIngredient();
        }
    }

    private GameObject SpawnIngredient()
    {
        var clone = Instantiate(_ingredientPrefab, _ingredientPoolTrans);
        clone.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        var draggable = clone.GetComponent<DraggableIngredient>();
        draggable.TopLayerTransform = _topLayerTransform;

        var ingredientGO = clone.GetComponent<IngredientGameObject>();
        ingredientGO.IngredientPoolTrans = _ingredientPoolTrans;
        return clone;
    }


}
