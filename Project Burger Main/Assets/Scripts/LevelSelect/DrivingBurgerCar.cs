using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingBurgerCar : MonoBehaviour {

   public Node StartNode;
   public Node StartNode2;
   public Node EndNode;

    List<Node> myPath;

    bool Walking = false;
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {

        if(Walking == true) {
           transform.position = Vector2.MoveTowards(transform.position, myPath[0].transform.position, 1 * speed);
            if(Vector2.Distance(transform.position, myPath[0].transform.position) < 0.1f) {

                StartNode = myPath[0];
                myPath.RemoveAt(0);

                if (myPath.Count >= 1) {
                    StartNode2 = myPath[0];
                } else {
                    StartNode2 = null;
                    StartNode = EndNode;
                    Walking = false;
                    LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(true);
                //    StartNode.GetComponent<OnClickWalk>().GoToLevel();
                }
            }
            
        }

    }

    public void Clicked(Node End) {
        EndNode = End;
        Walking = true;

        if (StartNode2 != null) {
            if (Vector2.Distance(transform.position, StartNode.transform.position) <= Vector2.Distance(transform.position, StartNode2.transform.position)) {
                myPath = LevelSelectManager.Instance.AStarSearch.StartRunning(StartNode, EndNode);

                if(myPath.Count > 1) {
                    if(myPath[0] == StartNode && myPath[1] == StartNode2) {//Going Same Direction So Just Continue Driving
                        myPath.RemoveAt(0);//Ignoring The First Node
                    }
                }

            } else {
                myPath = LevelSelectManager.Instance.AStarSearch.StartRunning(StartNode2, EndNode);

                if (myPath.Count > 1) {
                    if (myPath[0] == StartNode2 && myPath[1] == StartNode) {//Going Same Direction So Just Continue Driving
                        myPath.RemoveAt(0);//Ignoring The First Node
                    }
                }
            }

        } else {
            myPath = LevelSelectManager.Instance.AStarSearch.StartRunning(StartNode, EndNode);
        }







    }


}
