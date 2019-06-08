using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropArea : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsThisDropAreaOccupied;

   // public abstract void OnDrop(PointerEventData eventData); // we need this. because if we use the other system, then we can drop anything o anything 
    public virtual void OnDrop(PointerEventData eventData)
    {
        UnlimitedElementsOnDropArea(eventData);
    }

    private void OnlyAllowOneElementOnDropArea(PointerEventData eventData)
    {
        if (!IsThisDropAreaOccupied)
        {
            var draggedObject = eventData.pointerDrag;
             var draggableComponent = draggedObject.GetComponent<Draggable>();

            if (draggableComponent != null)
            {
                draggableComponent.DropAreaTransform = this.transform;
                draggableComponent.OnDropArea = true;
                draggableComponent.CurrentDropArea = this;
                IsThisDropAreaOccupied = true;
            }
            else
            {
                Debug.LogWarning(draggedObject.name + " ");
            }
        }
        else
        {
            Debug.Log("DropArea occupied  = " + name);
        }
    }

    private void UnlimitedElementsOnDropArea(PointerEventData eventData) // move this to FoodCombi
    {
        var draggedObject = eventData.pointerDrag;

        var draggableComponent = draggedObject.GetComponent<Draggable>();

        if (draggableComponent != null)
        {
            draggableComponent.DropAreaTransform = this.transform;
            draggableComponent.OnDropArea = true;
            draggableComponent.CurrentDropArea = this;
        }
    }
    /// <summary>
    /// When the player Drags FROM the drop zone, We can do different implementation depending on the type of drop area
    /// </summary>
    public abstract void DropAreaOnBeginDrag();
}
