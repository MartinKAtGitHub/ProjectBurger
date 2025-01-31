﻿using UnityEngine.EventSystems;
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
    protected RectTransform _resetPositionParent;
    protected Vector2 _resetPosOffset;
    /// <summary>
    /// Used to calculate drag point. This allows you to drag from anywhere on the img. If we didnt use this it would snap to center of img
    /// </summary>
    protected Vector3 _touchOffset;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
    protected CanvasGroup _canvasGroup;
    /// <summary>
    /// This parent object will be the reset point / snap back position when the player stops dragging in a invalid position. 
    /// </summary>

    private RectTransform _rectTransform;
    public virtual RectTransform ResetPositionParent { get => _resetPositionParent; set => _resetPositionParent = value; }
    public Transform RenderOrderTransform { get => _renderOrderTransform; set => _renderOrderTransform = value; }
    public Vector2 ResetPosOffset { set => _resetPosOffset = value; }

    protected void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        //_resetPositionParent = transform.parent;
        _resetPositionParent = transform.parent.GetComponent<RectTransform>();

       // Debug.Log(_resetPositionParent.name);
    }

    protected void Start()
    {
        if (_renderOrderTransform == null)
        {
            _renderOrderTransform = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            Debug.Log("Finding Tag -> MainCanvas Setting as RenderLayer transform");
            //Debug.LogError($" draggbel.cs| {name} dose not have a _renderOrderTransform , might cause the dragged object to be render behind other objects -> Setting -> {_renderOrderTransform.name}");
            // _renderOrderTransform = transform.parent.parent;
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
        transform.position = eventData.position + (Vector2)_touchOffset;
        Debug.Log($"OneDrag -> {name} , {transform.parent.name}");
    }

    public virtual void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () IN DropArea
    {
        transform.SetParent(_resetPositionParent);
        _rectTransform.anchoredPosition = Vector2.zero + _resetPosOffset;
        _canvasGroup.blocksRaycasts = true;
        Debug.Log($"OneEndDrag -> {name} | My Pos {_rectTransform.anchoredPosition} - offset { (Vector2.zero + _resetPosOffset)} PARENT { transform.parent.name}");
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
        _touchOffset = (Vector2)transform.position - eventData.position;
    }
}
