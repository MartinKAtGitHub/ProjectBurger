
using UnityEngine;
using UnityEngine.EventSystems;

public class GrillSlotDropArea : DropArea //, IDropHandler
{
    private RectTransform _rect;
    private TheGrill _theGrill;
    private BurgerMeatLogic _burgerMeatLogic;

    public BurgerMeatLogic BurgerMeatLogic { get => _burgerMeatLogic; set => _burgerMeatLogic = value; }

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _theGrill = GetComponent<TheGrill>();
    }
    public override void OnDrop(PointerEventData eventData)
    {
        var draggableBurgerMeat = eventData.pointerDrag.GetComponent<DraggableBurgerMeat>();
        if(draggableBurgerMeat != null)
        {
            draggableBurgerMeat.ResetPositionParent = _rect;
            
            _burgerMeatLogic = draggableBurgerMeat.BurgerMeatLogic;
            _burgerMeatLogic.TheGrill = _theGrill;
        }

    }
}
