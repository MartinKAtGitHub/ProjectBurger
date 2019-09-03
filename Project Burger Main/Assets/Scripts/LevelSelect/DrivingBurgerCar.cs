﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingBurgerCar : MonoBehaviour {

    [SerializeField]
    private Node _previousNode = null;
    [SerializeField]
    private Node _currentNode = null;
    [SerializeField]
    private Node _goToDirectionNode = null;
    [SerializeField]
    private Node _goToNode = null;
    [SerializeField]
    private Node _endNode = null;
    [SerializeField]
    private List<Node> myPath = null;

    [SerializeField]
    private bool _walking = false;
    [SerializeField]
    private bool _continueMoving = true;
    [SerializeField]
    private float _speed = 1;

    public void ContinueMoving(bool move) {
        if (move == true) {
            _continueMoving = true;
        } else {
            _continueMoving = false;
        }
    }


    private bool _ignoreClick = false;
    public bool IgnoreClick { get => _ignoreClick; set { _ignoreClick = value; } }

    // Update is called once per frame
    void Update() {

        if (_walking == true && _continueMoving == true) {
            transform.position = Vector2.MoveTowards(transform.position, myPath[0].transform.position, 1 * Time.deltaTime * _speed);

            if (Vector2.Distance(transform.position, myPath[0].transform.position) < 0.1f) {
                transform.position = myPath[0].transform.position;

                if (myPath.Count == 1) {//At Last Node
                    _walking = false;

                    if (myPath[0].ThisNodesBehaviours == null) {
                        StopOnNode();
                    } else {
                        myPath[0].ThisNodesBehaviours.SteppingOnEndNodeBehaviour();
                    }

                } else {
                    if (myPath[0].ThisNodesBehaviours == null) {//Continuing Is Default, When No Behaviour Is Present
                        //  StopOnNode(); //If You Want Auto Stop On Node
                        ContinueToNextNode(); //If You Want Auto Walk On Node
                    } else {
                        myPath[0].ThisNodesBehaviours.TransitionNodeBehaviour();
                    }
                }

            }

        }

    }

    public void ContinueToNextNode() {
        _walking = true;
        _previousNode = _goToDirectionNode;
        _currentNode = _goToDirectionNode;

        myPath.RemoveAt(0);

        _goToNode = myPath[0];
        _goToDirectionNode = myPath[0];

    }

    public void StopOnNode() {
        _walking = false;

        if (_goToDirectionNode != null)
            _currentNode = _goToDirectionNode;

        _goToNode = null;
        _goToDirectionNode = null;

        _ignoreClick = false;

    }

    public void StopAndHold() {
        StopOnNode();
        _ignoreClick = true;
    }

    public void CanMove() {
        _ignoreClick = false;
    }



    public void ForceWalkBack() {
        StopAndHold();

        _ignoreClick = false;
        Clicked(_previousNode);
        _ignoreClick = true;

    }

    public void SendPlayerToPreviousPosition() {

        _ignoreClick = false;
        Clicked(_previousNode);
        _ignoreClick = true;

    }

    public void Clicked(Node End) {
        if (_ignoreClick == true)
            return;

        _endNode = End;
        _walking = true;

        myPath = LevelSelectManager.Instance.AStarSearch.StartRunning(_currentNode, _endNode);//Will Always Return 1 Element. Current
        LevelSelectManager.Instance.CameraFollow.CameraFollow();

        if (myPath.Count == 1) {//If Player Is Clicking On A Spot That Isnt Walkable, Then The A* Is Returning CurrentNode As The Path. So If EndNode Isnt EndNode Then I Know That The Player Clicked Outside.

            if (_endNode != myPath[0]) {
                if (_goToNode != null) {
                    myPath[0] = _goToDirectionNode;
                }
            } else {
                if (_goToNode != null) {
                    _goToDirectionNode = myPath[0];
                }
            }
        } else {
            if (_goToNode == null) {//Standing Still
                _previousNode = myPath[0];
                _currentNode = myPath[0];
                if (myPath[0].ThisNodesBehaviours != null) {
                    myPath[0].ThisNodesBehaviours.SteppingOffEndNodeBehaviour();
                }
                myPath.RemoveAt(0);
                _goToNode = myPath[0];
                _goToDirectionNode = myPath[0];
            } else {
                if (myPath[0] == _currentNode && myPath[1] == _goToNode) {
                    myPath.RemoveAt(0);
                    _goToDirectionNode = _goToNode;
                } else {
                    _goToDirectionNode = _currentNode;
                }
            }
        }
    }

}


