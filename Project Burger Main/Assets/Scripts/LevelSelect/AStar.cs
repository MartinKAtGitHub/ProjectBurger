using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    const int AStarMaxLength = 200;

    List<Node> AStarPath = new List<Node>();

    Node[] _OpenList = new Node[AStarMaxLength];
    int openListIndex = 0;

     Node[] _ClosedList = new Node[AStarMaxLength];
    int ClosedListIndex = 0;

    Node _CurrentNode;
    Node _NodeSaver;
    Node StartNode;
    Node EndNode;
    float lowestFCost = 0;
    int _NodeIndexSaver = 0;
    int saverCount = 0;

    public List<Node> StartRunning(Node start, Node end) {
        AStarPath = new List<Node>();
        StartNode = start;
        EndNode = end;

        for (int i = 0; i < openListIndex; i++) {//The Used Nodes Is In These Two Lists. So I Only Need To Reset The Nodes Used
            _OpenList[i].NodeSearchedThrough = false;
        }

        for (int i = 0; i < ClosedListIndex; i++) {
            _ClosedList[i].NodeSearchedThrough = false;
        }

        openListIndex = 0;
        ClosedListIndex = 0;

        StartNode.SetHCost(StartNode);//Giving The StartNode its Costs
        StartNode.StartNode(StartNode, EndNode);

        _OpenList[openListIndex++] = StartNode;//Giving The Search Through List Its First Node




        #region A* Algorythm

        while (ClosedListIndex < AStarMaxLength) {//If The ClosedListAtIndex Is Equalt To Or Greater The Total Amount Of Nodes Then This Is False And The Search Is Stopped
            lowestFCost = 10000000;

            for (int i = 0; i < openListIndex; i++) {//Iterating Through The List With Unused Nodes To Find The Node With The Lowerst FCost

                if (_OpenList[i].fCost < lowestFCost) {
                    _CurrentNode = _OpenList[i];
                    _NodeIndexSaver = i;
                    lowestFCost = _CurrentNode.fCost;
                }
            }

            //Debug.Log("---------------- LOWEST " + _CurrentNode.name);
            _ClosedList[ClosedListIndex++] = _CurrentNode;//The Node That Was Chosen From Openlist Is Put In Here
            _OpenList[_NodeIndexSaver] = _OpenList[--openListIndex];//The Node That Was Added To ClosedList Is Being Removed Here And Replaced With The Last Node In The Openlist


            if (_CurrentNode == EndNode) {//If True Then A Path Was Found And The Search Is Complete
                RemakePath();
                return AStarPath;
            }

            saverCount = _CurrentNode.Neighbour.Count;//This Is An Improvement Rather Then Getting The Length Each i++ (Not Much But Some)

            for (int i = 0; i < saverCount; i++) {

                _NodeSaver = _CurrentNode.Neighbour[i];
                if (_NodeSaver.NodeSearchedThrough == false) {//If false Then The Node Havent Been Searched Through And Info Need To Be Set
                //Debug.Log(_NodeSaver.name + " adding");

                    _NodeSaver.SetFirstAdding(_CurrentNode, end);
                    _OpenList[openListIndex++] = _NodeSaver;
                } else if (_CurrentNode.gCost < _NodeSaver.Parent.gCost) {//If Current.Gcost Is Less Then Nodeholder.parent.Gcost Then A New ParentNode Is Set  ...... If Errors Occur Use (_NodeHolder.GCost > _CurrentNode.GCost + (PathfindingNodeID[CollisionID[_NodeHolder.PosX, _NodeHolder.PosY]] * 1.4f))
                    _NodeSaver.SetNewParent(_CurrentNode);
                }
            }

        }

        #endregion

        Debug.LogWarning("No Path Detected, Initiating Self Destruct Algorythms.... 3..... 2..... 1..... .");



        return AStarPath;

    }

    void RemakePath() {//Backtracking From The EndNode. When The Backtracking Reaches The StartNode, Then The Path Is Set

        saverCount = 0;//Just A Reused Variable For Holding The Index Number For The Next Node To Enter In The Path

        _CurrentNode = EndNode;
        AStarPath.Add(_CurrentNode);

        if (EndNode == StartNode) {
            return;
        } 

        while (true) {//Going Backwards Parent To Parent To Parent.....
            if (_CurrentNode.Parent != StartNode) {
                AStarPath.Add(_CurrentNode.Parent);
                _CurrentNode = _CurrentNode.Parent;

            } else {
                AStarPath.Add(_CurrentNode.Parent);
                AStarPath.Reverse(); 
                return;
            }
        }

    }
}
