using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleCreateBody : MonoBehaviour {

    public GameObject CustomerPrefab;

    public FlexibleCustomCompleteCustomer TheCustomerMale;
    public FlexibleCustomCompleteCustomer TheCustomerFemale;

    public bool Remake = false;

    GameObject one;

    public Transform WalkToPosition;

    private void Update() {

        if (Remake == true) {
            Remake = false;
            Destroy(one);

            if (Random.Range(0, 2) == 0) {
                MakeCustomer(TheCustomerMale, Vector3.zero);
            } else {
                MakeCustomer(TheCustomerFemale, Vector3.zero);
            }

        }



    }

    private void Start() {

        if (Random.Range(0, 2) == 0) {
            MakeCustomer(TheCustomerMale, Vector3.zero);
        } else {
            MakeCustomer(TheCustomerFemale, Vector3.zero);
        }

    }

    GameObject savedTorso;
    GameObject savedHead;

    void MakeCustomer(FlexibleCustomCompleteCustomer cus, Vector3 pos) {

        savedTorso = cus.TheTorsos[Random.Range(0, cus.TheTorsos.Length)];
        savedHead = cus.TheHeads[Random.Range(0, cus.TheHeads.Length)];


        one = (Instantiate(CustomerPrefab, transform.position + pos, Quaternion.identity, WalkToPosition.transform) as GameObject);

        one.transform.GetChild(0).GetComponent<SetFlexibleTorso>().SetImages(savedTorso);
        one.transform.GetChild(1).transform.localPosition = savedTorso.GetComponent<FlexibleCustomTorso>().HeadConnectPosition - savedHead.GetComponent<FlexibleCustomHead>().HeadConnectPosition;
        one.transform.GetChild(1).GetComponent<SetFlexibleHead>().SetImages(savedHead);

     //   one.GetComponent<CustomerState>().SetWalkTargets(transform, WalkToPosition);WALKINGOOF

    }



}