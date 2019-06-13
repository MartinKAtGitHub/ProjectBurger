using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
/// <summary>
/// The physical / GameObject version of a recipe
/// </summary>
public class Food : MonoBehaviour
{
    [SerializeField]
    private bool _didStackMatchOrder;

    public List<IngredientGameObject> GameObjectIngredients = new List<IngredientGameObject>();

    public bool DidStackMatchOrder { get => _didStackMatchOrder;  set => _didStackMatchOrder = value; }

    public void CreateFoodGameObjectWithIngredients()
    {
        for (int i = 0; i < GameObjectIngredients.Count; i++)
        {
            GameObjectIngredients[i].transform.SetParent(transform);
            Debug.Log(GameObjectIngredients[i].name + "  PARAENT IS = " + transform.name);
        }
    }
}
