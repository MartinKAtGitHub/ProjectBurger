using UnityEngine;
using UnityEngine.EventSystems;


public abstract class DropArea : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsThisDropAreaOccupied;
    private Draggable _draggedElement;

    public virtual void OnDrop(PointerEventData eventData)
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
                Debug.LogWarning(draggedObject.name +" ");
            }
        }
        else
        {
            Debug.Log("DropArea occupied  = " + name);

        }
    }
}
