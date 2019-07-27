using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientsSpawner : MonoBehaviour, IPointerDownHandler/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    [SerializeField] private GameObject _ingredientPrefab;
    /// <summary>
    /// True = spawner will spawn inn all the ingreadiance at the start of the game.
    /// False = Spawner will spawn on click and hopefully u can drag it.
    /// </summary>
    [SerializeField] private bool _presetSpawn;
    [SerializeField] private int SpawnAmout;

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
        if (_presetSpawn)
        {
            for (int i = 0; i < SpawnAmout; i++)
            {
                SpawnIngredience();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if(!_presetSpawn)
        {
            // Debug.Log("SPAWN ingredience " + _ingredience.name);
            SpawnIngredience();
        }
    }

    private void SpawnIngredience()
    {
        var clone = Instantiate(_ingredientPrefab, transform);
        clone.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }
}
