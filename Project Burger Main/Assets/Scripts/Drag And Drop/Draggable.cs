using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public abstract class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// This will be the Parent that holds all the Drag and drop elements. If a Draggable obj is parented to this, the obj will not have any restrictions to movement due to Layoutgroups
    /// </summary>
    [SerializeField] private Transform freeMotionParent;
    /// <summary>
    /// Used to calculate drag point. This allows you to drag from anywhere on the img. If we didnt use this it would snap to center of img
    /// </summary>
    protected Vector3 _offset;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
    protected CanvasGroup canvasGroup;
    /// <summary>
    /// This parent object will be the reset point / snap back position when the player stops dragging in a invalid position. 
    /// </summary>
    public Transform ResetPositionParent { get => _resetPositionParent; set => _resetPositionParent = value; }

    [SerializeField] private Transform _resetPositionParent;
    private RectTransform _rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _resetPositionParent = transform.parent;
        if (freeMotionParent == null)
        {
            freeMotionParent = transform.parent.parent;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        CalculateDragAreaOffset(eventData);
        FreeDragMode();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + (Vector2)_offset;
    }

    public virtual void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () IN DropArea
    {
        // OnDrop happens before this, so if you set the parent in OnDrop then this will snap to the new Position and not the Original
        transform.SetParent(_resetPositionParent);
        _rectTransform.localPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Allows the dragged element to be above all the other elements because we set the parent to be that of a higher layer
    /// </summary>
    private void FreeDragMode()
    {
        transform.SetParent(freeMotionParent);
    }

    /// <summary>
    /// The offset allows the player to drag from corners
    /// </summary>
    /// <param name="eventData"></param>
    private void CalculateDragAreaOffset(PointerEventData eventData)
    {
        // Center of object - the position of the mouse on the object
        _offset = (Vector2)transform.position - eventData.position;
    }
}
