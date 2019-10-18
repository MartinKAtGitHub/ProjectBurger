using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStationSwiper : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

	 Vector3 _PanelLocation;
	 Vector3 _GoFromLocation;
     Vector3 _SavedStartLocation;

    public float _DragThreshold = 0.2f;

    bool _UpdatePosition = false;
	int _PanelsToTravelToInDistance = 0;//Setting The Maximum Value That The Most Furthest Panel To Go To Is, Then That Position Dictates Other Logic In The Code, Like Blocking Points At Which The Player Cannot Drag Outside

    float _DragSpeed = 0;
    float _SavedFirstTouchPosition = 0;
    float _PanelMoveSpeed = 0;
    float _TheTime = 0f;
    float _ScreenDragPercentage = 0;

    public bool ListeningForDrag = true;
     
    void Start() {
        _PanelLocation = transform.position;
        
        //Find Panels To Go To. Just counting childs. this will most likely get checked agains an upgrading system or something of that sort.
        //unless we make every lvl with separate panels, and when getting the sandwitch panel its added as a child then this will work fine. if not then a small change need to be made
        _PanelsToTravelToInDistance = (transform.childCount - 1) * (Screen.width * -1) + (Screen.width / 2);
        
    }


    public void OnBeginDrag(PointerEventData eventData) {

        if (ListeningForDrag == false) {
            _SavedFirstTouchPosition = eventData.position.x;
            _DragSpeed = 0;
            _UpdatePosition = false;
            _SavedStartLocation = transform.position;

        }
    }

    public void OnDrag(PointerEventData data) {

        if (ListeningForDrag == false) {

            if (_SavedFirstTouchPosition - data.position.x < 0) {//Calculating Top DragSpeed.
                if (_DragSpeed < (_SavedFirstTouchPosition - data.position.x) * -1)
                    _DragSpeed = (_SavedFirstTouchPosition - data.position.x) * -1;
            } else {
                if (_DragSpeed < (_SavedFirstTouchPosition - data.position.x))
                    _DragSpeed = (_SavedFirstTouchPosition - data.position.x);
            }
            if (transform.localPosition.x >= 0) {//Blocking The Side So That The Player Cannot Drag Outside The Left Side
                if (_SavedFirstTouchPosition - data.position.x < 0) {
                    _SavedFirstTouchPosition = data.position.x;
                    _DragSpeed = 0;
                    return;
                }
            }


            if (transform.localPosition.x <= _PanelsToTravelToInDistance - (Screen.width / 2)) {//Blocking The Side So That The Player Cannot Drag Outside The Right Side
                if (_SavedFirstTouchPosition - data.position.x > 0) {
                    _SavedFirstTouchPosition = data.position.x;
                    _DragSpeed = 0;
                    return;
                }
            }

            transform.position = _SavedStartLocation - (Vector3.right * (_SavedFirstTouchPosition - data.position.x));

            if (transform.localPosition.x > 0 || transform.localPosition.x < _PanelsToTravelToInDistance - (Screen.width / 2)) {//If Im Outside Bounds On Left Or Right Side Auto Reset Back.
                transform.position = _PanelLocation;
            }

        }
    }



    public void OnEndDrag(PointerEventData data) {

        if (ListeningForDrag == false) {

            _PanelMoveSpeed = _DragSpeed;
            _UpdatePosition = true;
            _TheTime = 0;
            _ScreenDragPercentage = ((_SavedFirstTouchPosition - data.position.x)) / Screen.width;

            if (Mathf.Abs(_ScreenDragPercentage) >= _DragThreshold) {//if drag is more then the set value == true.

                if (_ScreenDragPercentage > 0) {//going right
                    if (_PanelLocation.x > _PanelsToTravelToInDistance) {
                        _PanelLocation += new Vector3(-Screen.width, 0, 0);
                    }
                } else if (_ScreenDragPercentage < 0) {//going left
                    if (_PanelLocation.x < 0) {
                        _PanelLocation += new Vector3(Screen.width, 0, 0);
                    }
                }
            }

            if (_PanelMoveSpeed <= Screen.width / 4f) {//Setting Minimum DragBack
                _PanelMoveSpeed = (Screen.width / 4f) / Screen.width;
            } else {
                _PanelMoveSpeed /= Screen.width;
            }

            _GoFromLocation = transform.position;//This Is The Position That The Dragging Object Is At.
            _ScreenDragPercentage = Mathf.Abs(_ScreenDragPercentage) * -1f;//Had To Do This Cuz If I Didnt The Start Position Would Jump Ahead, And I Would Get A Nasty Gap At The Start.
            _TheTime = _PanelMoveSpeed;

        }
    }

    void Update() { // PERFORMANCE FoodStationSwiper.cs | use IEnumerator and call when it when it is needed instead of checking a bool every frame?

        if (_UpdatePosition == true) {
            if(_TheTime < 0.75f) {//0.75f Is The Point At Which I Starts The Slowdown "Effect"
                _TheTime += _PanelMoveSpeed * Time.deltaTime * 4f;
            } else {
                _TheTime += _PanelMoveSpeed * Time.deltaTime / (_TheTime / 0.75f * 0.4f);//Just Some Numbers To Decrease The Time At The End
            }
            
            transform.position = Vector3.Lerp(_GoFromLocation, _PanelLocation, Mathf.SmoothStep(_ScreenDragPercentage, 1f, _TheTime));

            if (Vector3.Distance(transform.position, _PanelLocation) == 0) {
                _UpdatePosition = false;
            }
        }
    }

}