using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientStorageDropArea : MonoBehaviour , IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var draggableComponent = eventData.pointerDrag.GetComponent<DraggableIngredient>();
        if (draggableComponent != null)
        {
            Debug.Log("IngredientStorageDropArea DROP" );
            draggableComponent.CurrentParent = this.transform;
            draggableComponent.transform.SetParent(this.transform);
        }
    }
}
