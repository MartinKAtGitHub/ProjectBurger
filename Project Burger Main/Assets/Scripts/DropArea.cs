using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropArea : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsThisDropAreaOccupied;
    private Draggable _draggedElement;


    public virtual void OnDrop(PointerEventData eventData)
    {
        
        UnlimitedElementsOnDropArea(eventData);
    }

    public void OnlyAllowOneElementOnDropArea(PointerEventData eventData)
    {
        if (!IsThisDropAreaOccupied)
        {
            var draggedObject = eventData.pointerDrag;
            _draggedElement = draggedObject.GetComponent<Draggable>();

            if (_draggedElement != null)
            {
                _draggedElement.ResetDropZone = this.transform;
                _draggedElement.OnDropArea = true;
                _draggedElement.CurrentDropArea = this;
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

    public void UnlimitedElementsOnDropArea(PointerEventData eventData)
    {
        var draggedObject = eventData.pointerDrag;
      
        _draggedElement = draggedObject.GetComponent<Draggable>();

        if (_draggedElement != null) 
        {
            _draggedElement.ResetDropZone = this.transform;
            _draggedElement.OnDropArea = true;
            _draggedElement.CurrentDropArea = this;       
        }
    }
    /// <summary>
    /// When the player Drags FROM the drop zone, We can do different implementation depending on the type of drop area
    /// </summary>
    public abstract void DropAreaOnBeginDrag();
}
