using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BurgerDrag : DraggableIngredient
{

    public Image TheImage;
    public BurgerMeatLogic TheBurgerInfos;
    public IngredientGameObject TheIngredientGameObject;
    //public TheGrill TheGrill;

    private Transform LastParent;
    //  private Vector3 LastPosition;
    Vector3 SavedPos = Vector3.zero;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }

    public void SetStartParent(Vector3 pos, Transform transfor)
    {
        //   base(eventData);
        //  LastPosition = pos;
        //  LastParent = transfor;
    }

}
