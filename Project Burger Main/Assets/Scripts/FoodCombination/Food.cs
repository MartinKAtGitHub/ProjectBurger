using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FoodDrag))]
[System.Serializable]
/// <summary>
/// The physical / GameObject version of a recipe
/// </summary>
public class Food : MonoBehaviour
{
    public bool DidStackMatchOrder { get => _didStackMatchOrder;  set => _didStackMatchOrder = value; }
    public FoodDrag FoodDrag { get => _foodDrag; set => _foodDrag = value; }

    public List<IngredientGameObject> IngredientsGO = new List<IngredientGameObject>();

    [SerializeField] private bool _didStackMatchOrder;

    private FoodDrag _foodDrag;
  

    private void Awake()
    {
        _foodDrag = GetComponent<FoodDrag>();
    }

    public void ParentIngredientsToFoodObject()
    {
        for (int i = 0; i < IngredientsGO.Count; i++)
        {
            IngredientsGO[i].transform.SetParent(transform);
            // Debug.Log(GameObjectIngredients[i].name + "  PARAENT IS = " + transform.name);
        }
    }

    public void RemoveFromGame()
    {
        for (int i = 0; i < IngredientsGO.Count; i++)
        {
            IngredientsGO[i].ReturnIngredientToPool();
        }

        Destroy(gameObject); // PERFORMANCE Food.cs | Destroying food gameobject. -> make pooling for these maybe. spawn 1 on each foodcombi and on remove just reset
    }
}
