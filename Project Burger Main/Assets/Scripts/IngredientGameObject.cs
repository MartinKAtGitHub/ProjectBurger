using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the physical/gameobject version of a ingredient.
/// We need this object so we can hold and get the data from ingredient SO.
/// 
/// </summary>
public class IngredientGameObject : MonoBehaviour //TODO  put this in Ingredient Drag 
{
    public Ingredient ingredient;

    private Image _ingredientImage;

    private void Awake()
    {
        ingredient.InitializeIngredient();

        _ingredientImage = GetComponent<Image>();
    }

    public void SetIngredientSpriteForLayer(int layer)
    {
        _ingredientImage.sprite = ingredient.IngredientLayerSprites[layer];
        // set name
    }
}

