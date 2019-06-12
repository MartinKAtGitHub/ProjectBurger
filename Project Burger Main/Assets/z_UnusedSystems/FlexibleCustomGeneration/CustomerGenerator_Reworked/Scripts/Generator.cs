using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    public GameObject TheCustomer;

    public GameObject[] DifferentCustomers;

    GameObject saver;
    int customindex = 0;
    // Start is called before the first frame update
    public bool Remake = false;

    public Transform WalkToPosition;

    private void Update() {

        if (Remake == true) {
            Remake = false;
            Destroy(saver);

            if (Random.Range(0, 2) == 0) {
                MakeCustomer();
            } else {
                MakeCustomer();
            }

        }



    }

    void Start() {

        if (Random.Range(0, 2) == 0) {
            MakeCustomer();
        } else {
            MakeCustomer();
        }

    }

    void MakeCustomer() {//Currently There Is 1 Ways To Make This Better, Make A Object Refrence Script That Hold Refrences To Image And RectTransform. But More Garbage On Destroy() And On Init();

        saver = Instantiate(TheCustomer, transform.position , Quaternion.identity, WalkToPosition.transform) as GameObject;

        customindex = Random.Range(0, DifferentCustomers.Length);

        saver.transform.GetChild(0).transform.localPosition = DifferentCustomers[customindex].transform.GetChild(0).transform.localPosition;//PositionOfTheTorso
        saver.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = DifferentCustomers[customindex].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        saver.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = DifferentCustomers[customindex].transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta;

        saver.transform.GetChild(1).transform.localPosition = DifferentCustomers[customindex].transform.GetChild(1).transform.localPosition;//PositionOfTheHead
        saver.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = DifferentCustomers[customindex].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite;
        saver.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = DifferentCustomers[customindex].transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta;

        saver.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = DifferentCustomers[customindex].transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite;
        saver.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta = DifferentCustomers[customindex].transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().sizeDelta;

        saver.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = DifferentCustomers[customindex].transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite;
        saver.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().sizeDelta = DifferentCustomers[customindex].transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().sizeDelta;

        saver.transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = DifferentCustomers[customindex].transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite;
        saver.transform.GetChild(1).GetChild(3).GetComponent<RectTransform>().sizeDelta = DifferentCustomers[customindex].transform.GetChild(1).GetChild(3).GetComponent<RectTransform>().sizeDelta;

        saver.GetComponent<FlexibleCustomerWalk>().SetWalkTargets(transform, WalkToPosition);
    //    saver.GetComponent<FlexibleCustomerWalk>().setTestManager(GetComponent<TestManager>());

    }




}
