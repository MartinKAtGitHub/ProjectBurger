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
    [SerializeField]private Ingredient _ingredient;
    [SerializeField]private Image _ingredientImage;
    [SerializeField] private RectTransform _touchArea;
    private Transform _ingredientPoolTrans;

    public Transform IngredientPoolTrans { get => _ingredientPoolTrans; set => _ingredientPoolTrans = value; }
    public Ingredient Ingredient { get => _ingredient; set => _ingredient = value; }

    private void Awake()
    {
        _ingredient.InitializeIngredient();
        //_ingredientImage = GetComponent<Image>();
        _ingredientImage.sprite = Ingredient.IngredientSprite; 
    }

    public void SetIngredientSpriteForLayer(int layer)
    {
        _ingredientImage.sprite = _ingredient.IngredientLayerSprites[layer];
    }

    /// <summary>
    /// when you are done using a ingredient (destroying it) use this method so the ingredient can be reused
    /// </summary>
    public void ReturnIngredientToPool()
    {
        transform.SetParent(IngredientPoolTrans);
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// The bin determines the touch register area but ingredient itself needs to control the actual dragging so we send over the size dealta to ingredeint tocuh
    /// keep in mind that the touch area cant be strech mode as it would not have a size delta then
    /// </summary>
    /// <param name="targetTouchArea"></param>
    public void RescaleTouchArea(RectTransform targetTouchArea)
    {
        // Debug.LogError(" DELTA IS -> " + targetTouchArea.sizeDelta);
        _touchArea.sizeDelta = targetTouchArea.sizeDelta;
        //_touchArea.offsetMin = Vector2.zero;
        //_touchArea.offsetMax = Vector2.zero;

    }
}

