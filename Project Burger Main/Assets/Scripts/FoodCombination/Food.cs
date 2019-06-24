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

    public List<IngredientGameObject> GameObjectIngredients = new List<IngredientGameObject>();

    [SerializeField] private bool _didStackMatchOrder;

    private FoodDrag _foodDrag;
  

    private void Awake()
    {
        _foodDrag = GetComponent<FoodDrag>();
    }

    public void CreateFoodGameObjectWithIngredients()
    {
        for (int i = 0; i < GameObjectIngredients.Count; i++)
        {
            GameObjectIngredients[i].transform.SetParent(transform);
            // Debug.Log(GameObjectIngredients[i].name + "  PARAENT IS = " + transform.name);
        }
    }
}
