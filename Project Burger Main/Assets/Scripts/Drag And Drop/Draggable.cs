using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public abstract class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// The transform that will determine the render order of the dragged object, think of it as sorting layer but with Transform/hierarchy
    /// </summary>
    private Transform _renderOrderTransform;
    protected Transform _resetPositionParent;
    /// <summary>
    /// Used to calculate drag point. This allows you to drag from anywhere on the img. If we didnt use this it would snap to center of img
    /// </summary>
    protected Vector3 _offset;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
    protected CanvasGroup _canvasGroup;
    /// <summary>
    /// This parent object will be the reset point / snap back position when the player stops dragging in a invalid position. 
    /// </summary>

    private RectTransform _rectTransform;
    public virtual Transform ResetPositionParent { get => _resetPositionParent; set => _resetPositionParent = value; }
    public Transform RenderOrderTransform { get => _renderOrderTransform; set => _renderOrderTransform = value; }
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _resetPositionParent = transform.parent;
      
    }

    private void Start()
    {
        if (_renderOrderTransform == null)
        {
            Debug.LogError($" draggbel.cs| {name} dose not have a _topLayerTransform , might cause the dragged object to be render behind other objects -> set this to be the main canavs");
            _renderOrderTransform = transform.parent.parent;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // CalculateDragAreaOffset(eventData);

        FreeDragMode();
        _canvasGroup.blocksRaycasts = false;
        Debug.Log($"OneBeginDrag -> {name}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + (Vector2)_offset;
        Debug.Log($"OneDrag -> {name} , {transform.parent.name}");
    }

    public virtual void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () IN DropArea
    {
        // OnDrop happens before this, so if you set the parent in OnDrop then this will snap to the new Position and not the Original
        transform.SetParent(_resetPositionParent);
        _rectTransform.localPosition = Vector2.zero;
        _canvasGroup.blocksRaycasts = true;
        Debug.Log($"OneEndDrag -> {name}");
    }

    /// <summary>
    /// Allows the dragged element to be above all the other elements because we set the parent to be that of a higher layer
    /// </summary>
    private void FreeDragMode()
    {
        transform.SetParent(_renderOrderTransform);
    }

    /// <summary>
    /// The offset allows the player to drag from corners, without this the ingredient will snap to the finger
    /// </summary>
    /// <param name="eventData"></param>
    private void CalculateDragAreaOffset(PointerEventData eventData)
    {
        // Center of object - the position of the mouse on the object
        _offset = (Vector2)transform.position - eventData.position;
    }
}
