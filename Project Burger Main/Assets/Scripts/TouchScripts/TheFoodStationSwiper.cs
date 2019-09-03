using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TheFoodStationSwiper : MonoBehaviour, IDragHandler, IBeginDragHandler {

    [SerializeField]
    private float HowFarToSwipe = 0.1f;
    [SerializeField]
    private RectTransform canvas = null;

    private float MaxDist = 0;
    private Vector2 StartPos = Vector2.zero;

    [SerializeField]
    private TableSwipeSetter _tableSwipeSetter = null;


    private void OnRectTransformDimensionsChange() {//Im Not Sure Why But This Is Called Several Timer, I Think It Has Something To Do With Canvas Scaler.
        MaxDist = canvas.sizeDelta.x * canvas.localScale.x * HowFarToSwipe;//Might Need To Change This At Some Point. But Currently its Working As Intended, by Checking Horizontal Swipe 
    }

    public void OnBeginDrag(PointerEventData eventData) {
        StartPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Vector2.Distance(eventData.position, StartPos) > MaxDist) {
            if (eventData.position.x < StartPos.x) {
                _tableSwipeSetter.SwipedLeft();
            } else {
                _tableSwipeSetter.SwipedRight();
            }

            StartPos = eventData.position;

        }
    }

}
