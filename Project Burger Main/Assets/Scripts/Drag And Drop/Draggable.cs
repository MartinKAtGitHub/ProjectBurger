using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public abstract class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool OnDropArea; // I can make a prop of this and have it override in each class and reuse this bool
    public DropArea CurrentDropArea; // This is assigned in the DropArea
    public Transform OriginalParent = null; // If i cant drop in valid spot go back to this

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
    LayoutElement layoutElement;
    private GameObject _placeHolder;

    // UPDATE ------------------------------

    public Transform CurrentParent { get => _currentParent; set => _currentParent = value; }
   

  
    [SerializeField] private Transform _currentParent;
    private RectTransform _rectTransform;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        //layoutElement = GetComponent<LayoutElement>();
        //OriginalParent = transform.parent;

        _rectTransform = GetComponent<RectTransform>();

        _currentParent = transform.parent;
        if (freeMotionParent == null)
        {
            freeMotionParent = transform.parent.parent;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {


        // This can be UNIQUE ---------------------------------------------------
        //if (!OnDropArea)
        //{
        //    CreatePlaceHolderObj();
        //}

        //if (CurrentDropArea != null)
        //{
        //    CurrentDropArea.IsThisDropAreaOccupied = false;
        //    CurrentDropArea.DropAreaOnBeginDrag();
        //    CurrentDropArea = null;
        //}

        //OnDropArea = false;

        // UPDATE -----------------------------------------------------

        CalculateDragAreaOffset(eventData);

        //_resetPosition = transform.position;
        //_currentParent = transform.parent;

        // Debug.Log("PARAENT NAME" + _currentParent.name);
        FreeDragMode();
        canvasGroup.blocksRaycasts = false; // after pick up we turn this off to allow PointerEventData to go through the draggable obj
    }

    public void OnDrag(PointerEventData eventData) // Every drag will have this
    {
        transform.position = eventData.position + (Vector2)_offset;
    }

    public virtual void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () IN DropArea
    {
        // This can be UNIQUE ---------------------------------------------------

        //if (OnDropArea) // If i am on a dropZone
        //{
        //    ResetPositionToDropArea();
        //}
        //else
        //{
        //    RestToPlaceHolderPosition();
        //}
        // -----------------------------------------------------

        // OnDrop happens before this, so if you set the parent in OnDrop then this will snap to the new Position and not the Original
      
         transform.SetParent(_currentParent);
        _rectTransform.localPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;
    }


    //private void Update()
    //{
    //    Debug.Log("NAME: " + name + " Parent: "+ transform.parent);
    //}


    /// <summary>
    /// Creates a placeholder position so the object can snap back to if the item was not dropped on a DropArea
    /// </summary>
    protected void CreatePlaceHolderObj()
    {
        // PERFORMANCE - Draggable We don't really need this. We dont care about the position in the hierarchy only position.
        _placeHolder = new GameObject();
        _placeHolder.name = "PlaceHolder (" + name + ") ";
        _placeHolder.transform.SetParent(transform.parent);

        var le = _placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = layoutElement.preferredWidth;
        le.preferredHeight = layoutElement.preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        _placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }

    /// <summary>
    /// Snaps drag object back to starting position(Placeholder). and Destroys placeholder
    /// </summary>
    protected void RestToPlaceHolderPosition()
    {
        transform.SetParent(OriginalParent);
        transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());

        _rectTransform.localPosition = Vector2.zero; // This works if you turn of VerticalLayoutGroup But not on Drop

        Destroy(_placeHolder);
    }

    protected void FreeDragMode()
    {
        transform.SetParent(freeMotionParent); // This takes us out of the Pnl(layout group) so we can freely move the UI element
       
    }

    /// <summary>
    /// if you slide the draggable object while on a drop area and release, it will slide back to the drop area instead of the starting area
    /// </summary>
    //protected void ResetPositionToDropArea()
    //{
    //    transform.SetParent(DropAreaTransform);
    //    _rectTransform.localPosition = Vector2.zero;
    //    //canvasGroup.blocksRaycasts = false;
    //}

    /// <summary>
    /// The offset allows the player to drag from corners
    /// </summary>
    /// <param name="eventData"></param>
    protected void CalculateDragAreaOffset(PointerEventData eventData)
    {
        // Center of object - the position of the mouse on the object
        _offset = (Vector2)transform.position - eventData.position;
    }
}
