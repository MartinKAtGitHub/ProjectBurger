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
       // ResetPositionParent.GetComponent<TheGrill>()?.DropAreaOnBeginDrag();

        //transform.SetParent(transform.parent.parent.parent);//Hmmmm
        //canvasGroup.blocksRaycasts = false;

        //LastParent = ResetPositionParent;
        //ResetPositionParent.GetComponent<DropArea>().DropAreaOnBeginDrag();
        //ResetPositionParent = null;//This Is Set In All OnDrop? If Not THis Might Cause Problems

    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
       // ResetPositionParent.GetComponent<TheGrill>()?.Setup();

        //if (ResetPositionParent == null)
        //{//This Is True If The OnDrop Didnt Happen, So Im Calling OnDrop Again To Reapply Info From OnDrop
        //    ResetPositionParent = LastParent;
        //    ResetPositionParent.GetComponent<IDropHandler>().OnDrop(eventData);

        //    SavedPos = transform.position;
        //    base.OnEndDrag(eventData);
        //    transform.position = SavedPos;
        //}
        //else
        //{
        //    if (ResetPositionParent.GetComponent<GrillPlateStack>() == null)
        //    {//This Will Happen For all Other Objects Except Stack 
        //        base.OnEndDrag(eventData);
        //    }

        //    else
        //    {//The Position Of The Item Will Be Set In The OnDrop Object, So To Override The 0,0,0 Position Setter In base() I Need To Save Old And Apply After
        //        SavedPos = transform.position;
        //        base.OnEndDrag(eventData);
        //        transform.position = SavedPos;//Positoin Of Object Is Set To 0,0,0 In Base, So Need To Apply The Change Here, Remove This If Base Is Updated.
        //    }
        //}




        //     if (LastParent != ResetPositionParent) {//This Might Be Changed Later, I Need To Set The Position Of The Burger, And If Not Changed Position Then It Will Snap Back To That Exact Position Not Vector.Zero.
        //         LastPosition = transform.position;
        //         LastParent = ResetPositionParent;
        //     } else {
        //         transform.position = LastPosition;
        ////         transform.SetParent(ResetPositionParent);
        //     }
        //     canvasGroup.blocksRaycasts = true;


    }

    public void SetStartParent(Vector3 pos, Transform transfor)
    {
        //   base(eventData);
        //  LastPosition = pos;
        //  LastParent = transfor;
    }

}
