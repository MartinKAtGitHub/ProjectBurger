using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Time the burgers. Flip them and store them
public class BurgersMeat : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    //make points at which burgers can be place, drag and drop snapping points.

    //every point should have a timer at which starts depending on what burger spawns on it.

    //the timer dictates when the burger is complete, (raw, medium, brick).

    //make a place on the side of the grill where the player can place the burgers that were on the grill.
    //when take off it stores its cooking value if the player decides to take it back on the grill.


    //    public override void OnBeginDrag(PointerEventData eventData) {
    //       base.OnBeginDrag(eventData);

    //      Debug.Log("TEST");


    //  }



    //question how to flip,  skill based flip? if they drop it they need to make a new one?
    //make a path at which the player have to swipe in order to turn it, circular motion?

    //just make a circular motion then the burger flips itself?
    //temperature instead of clock.

    PointerEventData _EventData;
    bool Move = false;
    public void SetEventData(PointerEventData eventData) {
        _EventData = eventData;
    
        Move = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("BurgetDown");
        Move = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        Debug.Log("BurgetUp");
        Move = false;
    }


    void Update() {
        
        if(Move == true) {
            transform.position = _EventData.position;
        }





    }




}





