using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientsSpawner : MonoBehaviour, IPointerDownHandler/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    
    [SerializeField]
    private GameObject _ingredience;
    private GameObject _spawnedIngredients;

    /// <summary>
    /// True = spawner will spawn inn all the ingreadiance at the start of the game.
    /// False = Spawner will spawn on click and hopefully u can drag it.
    /// </summary>
    [SerializeField]
    private bool _presetSpawn;

    private Draggable draggableIngredients; 
       

    public int ingredienceSpawnAmout;

    private void Start()
    {
        if(_presetSpawn)
        {
            for (int i = 0; i < ingredienceSpawnAmout; i++)
            {
                SpawnIngredience();
            }
        }
    }

    //public void OnBeginDrag(PointerEventData eventData) // So this Enables the instant drag and sends a trigger message over to the dag scrip ?
    //{
    //    //if (!_presetSpawn)
    //    {
    //        //eventData.pointerDrag = _spawnedIngredients;
    //        Debug.Log("OnBeginDrag  IngredientsSpawner " + eventData.pointerDrag.name);
    //        draggableIngredients.OnBeginDrag(eventData);
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    //if (!_presetSpawn)
    //    {
    //        Debug.Log(" OnDrag  IngredientsSpawner " + eventData.pointerDrag.name);
          
    //        draggableIngredients.OnDrag(eventData);
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //   // if (!_presetSpawn)
    //    {
    //        Debug.Log("OnEndDrag  IngredientsSpawner" + eventData.pointerDrag.name);
    //        draggableIngredients.OnEndDrag(eventData);
    //    }
    //}

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
        _spawnedIngredients = Instantiate(_ingredience, transform.parent);
        _spawnedIngredients.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        draggableIngredients = _spawnedIngredients.GetComponent<Draggable>();
    }
}
