using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine;


public class DraggableBurgerMeat : DraggableGrillableIngredient
{
    private BurgerMeatLogic _burgerMeatLogic;

    [SerializeField]private TheGrill _prevTheGrill;
    [SerializeField]private RectTransform _prevResetRect;

    public BurgerMeatLogic BurgerMeatLogic { get => _burgerMeatLogic; }

    new private void Awake()
    {
        base.Awake();
        _burgerMeatLogic = GetComponent<BurgerMeatLogic>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if(_burgerMeatLogic.TheGrill != null)
        {
            Debug.Log("ONBEGIN  -> I HAVE GRILL, MAKING TEMP REF FOR RESET");
            _prevTheGrill = _burgerMeatLogic.TheGrill;
            _prevResetRect = ResetPositionParent;

            _burgerMeatLogic.TheGrill.GrillSlotDropArea.BurgerMeatLogic = null;
            _burgerMeatLogic.TheGrill = null;
        }
        else
        {
            Debug.Log("NO GRILL FOUND ");
        }
    }

    public override void OnEndDrag(PointerEventData eventData) // THIS FIRES AFTER ONDROP
    {
        base.OnEndDrag(eventData);

        if(_prevResetRect == ResetPositionParent)
        {
            Debug.Log(" ONEND RESET IS THE SAME GRILL REVERTING CHANGES");
            _burgerMeatLogic.TheGrill = _prevTheGrill;
            _burgerMeatLogic.TheGrill.GrillSlotDropArea.BurgerMeatLogic = _burgerMeatLogic;

        }else
        {
            Debug.Log("NEW DROPAREA");
        }
    }

}
