using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BurgerDrag : Draggable {

    public Image TheImage;
    public BurgerInfo TheBurgerInfos;
    public IngredientGameObject TheIngredientGameObject;

    private Transform LastParent;
    private Vector3 LastPosition;
  

    public override void OnBeginDrag(PointerEventData eventData) {

        transform.SetParent(transform.parent.parent.parent);//Hmmmm
        canvasGroup.blocksRaycasts = false;
        ResetPositionParent.GetComponent<OnDropAreaInfo>().DropAreaOnBeginDrag();

    }

    public override void OnEndDrag(PointerEventData eventData) {// THIS FIRES AFTER ONDROP () IN DropArea

        canvasGroup.blocksRaycasts = true;

        if (LastParent != ResetPositionParent) {//This Might Be Changed Later, I Need To Set The Position Of The Burger, And If Not Changed Position Then It Will Snap Back To That Exact Position Not Vector.Zero.
            LastPosition = transform.position;
            LastParent = ResetPositionParent;
        } else {
            transform.position = LastPosition;
            transform.SetParent(ResetPositionParent);
        }

    }

    public void SetStartParent(Vector3 pos, Transform transfor) {
        LastPosition = pos;
        LastParent = transfor;
    }

}
