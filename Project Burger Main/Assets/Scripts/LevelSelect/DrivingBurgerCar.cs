using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingBurgerCar : MonoBehaviour {

    public Node PreviousNode;

    public Node CurrentNode;
    public Node NextNode;
    public Node EndNode;

    public List<Node> myPath;

    public bool Walking = false;
    public float speed = 1;

    public bool _continueMoving = true;

    public void ContinueMoving(bool move) {
        if (move == true) {
            _continueMoving = true;
        } else {
            _continueMoving = false;
        }
    }

    bool _checkup = true;
    bool _ignoreClick = false;
  
    // Update is called once per frame
    void Update() {

        if (Walking == true && _continueMoving == true) {
            transform.position = Vector2.MoveTowards(transform.position, myPath[0].transform.position, 1 * speed);
            if (Vector2.Distance(transform.position, myPath[0].transform.position) < 0.1f) {
                transform.position = myPath[0].transform.position;

                if (myPath.Count == 1) {//At Last Node
                    Walking = false;

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
        Walking = true;
        PreviousNode = CurrentNode;
        CurrentNode = myPath[0];
        myPath.RemoveAt(0);
        NextNode = myPath[0];

    }

    public void StopOnNode() {
        Walking = false;
        PreviousNode = CurrentNode;
        CurrentNode = myPath[0];
        NextNode = null;
        _ignoreClick = false;

    }


    public void ForceWalkBack() {
        PreviousNode = CurrentNode;
        CurrentNode = myPath[0];

        Clicked(PreviousNode);
        _ignoreClick = true;

    }

    public void Clicked(Node End) {
        if (_ignoreClick == true)
            return;

        EndNode = End;
        Walking = true;

        myPath = LevelSelectManager.Instance.AStarSearch.StartRunning(CurrentNode, EndNode);//Will Always Return 1 Element
        LevelSelectManager.Instance.CameraFollow.CameraFollow();

        if (NextNode != null) {//!Null Means That Im Walking
            if (myPath.Count > 1) {
                if (myPath[1] == NextNode) {
                    myPath.RemoveAt(0);//Ignoring The First Node, Cuz Alrdy Walking To It
                }
            }
        }

        if (Vector2.Distance(transform.position, myPath[0].transform.position) == 0) {//If Im On The First Node
            if (myPath[0].ThisNodesBehaviours == null) {

            } else {
                myPath[0].ThisNodesBehaviours.SteppingOffEndNodeBehaviour();
            }

            PreviousNode = myPath[0];
            CurrentNode = myPath[0];
            myPath.RemoveAt(0);

            if (myPath.Count >= 1) {
                NextNode = myPath[0];
            } else {
                Walking = false;
            }

        }

    }

}
