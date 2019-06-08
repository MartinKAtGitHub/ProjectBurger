using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingCursomers : MonoBehaviour {

    public GameObject CustomerPrefab;

    public CustomerGender TheCustomer;
    public CustomerGender TheCustomerFemale;

    public bool Remake = false;

    GameObject one;
  

    public Transform WalkToPosition;
  
    private void Update() {

        if (Remake == true) {
            Remake = false;
            Destroy(one);

            if (Random.Range(0, 2) == 0) {
                MakeCustomer(TheCustomer, Vector3.zero);
            } else {
                MakeCustomer(TheCustomerFemale, Vector3.zero);
            }

        }



    }

    private void Start() {

        if (Random.Range(0, 2) == 0) {
            MakeCustomer(TheCustomer, Vector3.zero);
        } else {
            MakeCustomer(TheCustomerFemale, Vector3.zero);
        }

    }

    void MakeCustomer(CustomerGender cus, Vector3 pos) {
        one = Instantiate(CustomerPrefab, transform.position + pos, Quaternion.identity, WalkToPosition.transform) as GameObject;
        one.transform.GetChild(0).GetComponent<SetBodyShape>().SetImages(cus.BodyShape);
        one.transform.GetChild(1).transform.localPosition = cus.BodyShape.HeadBodyConnectPosition - cus.HeadShape.HeadBodyConnectPosition;
        one.transform.GetChild(1).GetComponent<SetHeadShape>().SetImages(cus.HeadShape);

        one.GetComponent<CustomerWalk>().SetWalkTargets(transform, WalkToPosition);

    }

    

}