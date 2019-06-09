using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWalk : MonoBehaviour {




    TestManager testin;

    public void setTestManager(TestManager te) {
        testin = te;
    }

     Transform Door;
     Transform Cashier;
    public float MoveSpeed = 1;

    bool ReachedEnd = false;
    bool GoIn = true;
    bool onceorder = true;
    public float TimeToWait = 5f;
    float WaitTime = 0;
    OrderGenerator test;

    private void Start() {
        test = GetComponent<OrderGenerator>();
        
    }


    public void SetWalkTargets(Transform start, Transform end) {
        Door = start;
        Cashier = end;
    }

    private void Update() {

        if(ReachedEnd == false) {
            transform.position = Vector3.MoveTowards(transform.position, Cashier.position, MoveSpeed * Time.deltaTime);
            if (transform.position == Cashier.position) {
                ReachedEnd = true;
                WaitTime = TimeToWait + Time.time;
            }
        } else {

            if(GoIn == true) {//StartDialog And/Or Order Generator
                              //TODO connect To Order Generator And Wait For Response;

                if(onceorder == true) {
                    Debug.Log("AT THE GOAL");
                    test.RequestOrder();
                    testin.SetOrder(this);

                    onceorder = false;
                }

                if (Time.time >= WaitTime) {
                    Cashier = Door;
                    GoIn = false;
                    ReachedEnd = false;
                }

            } else {
                GameObject.Find("Manager").GetComponent<GeneratingCursomers>().Remake = true;
                Destroy(gameObject);
            }
        }

    }


}
