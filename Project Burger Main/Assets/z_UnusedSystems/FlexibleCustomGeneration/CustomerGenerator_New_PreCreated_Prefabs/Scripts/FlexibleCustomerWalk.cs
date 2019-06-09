using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleCustomerWalk : MonoBehaviour
{
  

    Transform Door;
    Transform Cashier;
    public float MoveSpeed = 1;

    bool ReachedEnd = false;
    bool GoIn = true;
    public float TimeToWait = 5f;
    float WaitTime = 0;

    private void Start() {

    }


    public void SetWalkTargets(Transform start, Transform end) {
        Door = start;
        Cashier = end;
    }

    private void Update() {

        if (ReachedEnd == false) {
            transform.position = Vector3.MoveTowards(transform.position, Cashier.position, MoveSpeed * Time.deltaTime);
            if (transform.position == Cashier.position) {
                ReachedEnd = true;
                WaitTime = TimeToWait + Time.time;
            }
        } else {

            if (GoIn == true) {//StartDialog And/Or Order Generator
                               //TODO connect To Order Generator And Wait For Response;

                

                if (Time.time >= WaitTime) {
                    Cashier = Door;
                    GoIn = false;
                    ReachedEnd = false;
                }

            } else {
                GameObject.Find("CustomGenerator").GetComponent<FlexibleCreateBody>().Remake = true;
                Destroy(gameObject);
            }
        }

    }


}
