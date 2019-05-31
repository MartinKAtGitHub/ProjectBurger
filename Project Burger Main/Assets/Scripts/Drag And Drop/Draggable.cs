using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(LayoutElement))]
public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool OnDropArea; // I can make a prop of this and have it override in each class and reuse this bool
    public DropArea CurrentDropArea; // This is assigned in the DropArea
    /// <summary>
    /// The Transform of the drop area, it should be set from the drop area to avoid confusion 
    /// </summary>
    public Transform DropAreaTransform = null;
    public Transform OriginalParent = null; // If i cant drop in valid spot go back to this
      
    
    /// <summary>
    /// This will be the Parent that holds all the Drag and drop elements. If a Draggable obj is parented to this, the obj will not have any restrictions to movement due to Layoutgroups
    /// </summary>
    [SerializeField] private Transform freeMotionParent;
    /// <summary>
    /// Used to calculate drag point. This allows you to drag from anywhere on the img. If we didnt use this it would snap to center of img
    /// </summary>
   protected Vector3 offset;
    /// <summary>
    /// Canvas group allows us use certain options to allow us to register PointerEventData through draggable objects.
    /// </summary>
   protected CanvasGroup canvasGroup;
    LayoutElement layoutElement;
    RectTransform rectTransform;

    private GameObject PlaceHolder;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();

        OriginalParent = transform.parent;

        if(freeMotionParent == null)
        {
            freeMotionParent = transform.parent.parent;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("PULL DRAG" + name);
        CalculateDragAreaOffset(eventData);

        if (!OnDropArea)
        {
            CreatePlaceHolderObj(); // Move this down maybe ? CurrentDropArea != null
        }

        FreeDragMode();

        if (CurrentDropArea != null)
        {
            CurrentDropArea.IsThisDropAreaOccupied = false;
            CurrentDropArea.DropAreaOnBeginDrag();
       
            CurrentDropArea = null;
        }

        OnDropArea = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + (Vector2)offset;
    }

    public virtual void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP () IN DropArea
    {
        if(OnDropArea) // If i am on a dropZone
        {
            ResetPositionToDropArea();
        }
        else
        {
            RestToStartPosition();
        }
       
        canvasGroup.blocksRaycasts = true; // after pick up we turn this off to allow PointerEventData to go through the draggable obj
    }

    /// <summary>
    /// The PlaceHolder acts as a anchor to the draggable objects which holds the position in the hierarchy.
    /// </summary>
    protected void CreatePlaceHolderObj()
    {
        PlaceHolder = new GameObject();
        PlaceHolder.name = "PlaceHolder (" + name + ") ";
        PlaceHolder.transform.SetParent(transform.parent);
        var le = PlaceHolder.AddComponent<LayoutElement>();
        le.preferredWidth = layoutElement.preferredWidth;
        le.preferredHeight = layoutElement.preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        PlaceHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }
   
    /// <summary>
    /// Snaps drag object back to starting position(Placeholder). and Destroys placeholder
   /// </summary>
    protected void RestToStartPosition()
    {
        transform.SetParent(OriginalParent);
        transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());

        rectTransform.localPosition = Vector2.zero; // This works if you turn of VerticalLayoutGroup But not on Drop

        Destroy(PlaceHolder);
    }

    protected void FreeDragMode()
    {
        transform.SetParent(freeMotionParent); // This takes us out of the Pnl(layout group) so we can freely move the UI element
        canvasGroup.blocksRaycasts = false; // after pick up we turn this off to allow PointerEventData to go through the draggable obj
    }

    /// <summary>
    /// if you slide the draggable object while on a drop area and release, it will slide back to the drop area instead of the starting area
    /// </summary>
    protected void ResetPositionToDropArea()
    {
        transform.SetParent(DropAreaTransform);
        rectTransform.localPosition = Vector2.zero;
        //canvasGroup.blocksRaycasts = false;
    }

    protected void CalculateDragAreaOffset( PointerEventData eventData)
    {
        var currentMousePos = eventData.position;
        offset = (Vector2)transform.position - currentMousePos;
    }
}
