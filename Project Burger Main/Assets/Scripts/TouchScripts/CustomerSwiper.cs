using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerSwiper : MonoBehaviour, IDragHandler, IBeginDragHandler {


    //[SerializeField] private float HowFarToSwipe = 0.1f;
    //[SerializeField] private RectTransform canvas = null;
    [SerializeField] private Button _leftbutton = null;
    [SerializeField] private Button _rightbutton = null;

    private float MaxDist = 1920;
    private Vector2 StartPos = Vector2.zero;



    public void OnBeginDrag(PointerEventData eventData) {
        StartPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Vector2.Distance(eventData.position, StartPos) > MaxDist) {
            //Debug.Log("Over The Limit, Start Changing Customer");

            if (eventData.position.x < StartPos.x) {
                Debug.Log("right"); _rightbutton.onClick.Invoke(); //Script Was Not Attached To The Button, So Could Not Test It
            } else {
                Debug.Log("left"); _leftbutton.onClick.Invoke(); //Script Was Not Attached To The Button, So Could Not Test It
            }

            StartPos = eventData.position; 

        }
    }

}
