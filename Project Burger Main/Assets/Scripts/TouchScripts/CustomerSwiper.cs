﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerSwiper : MonoBehaviour, IDragHandler, IBeginDragHandler {


    [SerializeField] private float HowFarToSwipe = 0.1f;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private Button _leftbutton = null;
    [SerializeField] private Button _rightbutton = null;

    private float MaxDist = 0;
    private Vector2 StartPos = Vector2.zero;




    private void OnRectTransformDimensionsChange() {//Im Not Sure Why But This Is Called Several Timer, I Think It Has Something To Do With Canvas Scaler.
        MaxDist = canvas.sizeDelta.x * canvas.localScale.x * HowFarToSwipe;//Might Need To Change This At Some Point. But Currently its Working As Intended, by Checking Horizontal Swipe 
        //Debug.Log("Changing Max Screen Size " + MaxDist);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        StartPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Vector2.Distance(eventData.position, StartPos) > MaxDist) {
            StartPos = eventData.position;
            //Debug.Log("Over The Limit, Start Changing Customer");

            if (eventData.position.x < StartPos.x) {
                Debug.Log("Over The Limit, Start Changing Customer"); _leftbutton.onClick.Invoke(); //Script Was Not Attached To The Button, So Could Not Test It
            } else {
                Debug.Log("Over The Limit, Start Changing Customer"); _rightbutton.onClick.Invoke(); //Script Was Not Attached To The Button, So Could Not Test It
            }


        }
    }

}