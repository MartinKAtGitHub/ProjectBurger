using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerMeatSpawner : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    public GameObject burger;

    PointerEventData data;
    GameObject test = null;



    public GameObject other;
    float time = 0;




    public void OnDrag(PointerEventData eventData) {

    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("HER2");

        /* Debug.Log("HER");
           test = Instantiate(burger, transform.position + Vector3.up * 110, Quaternion.identity, transform.parent);


         ExecuteEvents.Execute<IEndDragHandler>(gameObject, data, ExecuteEvents.endDragHandler);
         data.pointerDrag = test;
         ExecuteEvents.Execute<IBeginDragHandler>(test, data, ExecuteEvents.beginDragHandler);*/

    }


    public void OnPointerDown(PointerEventData eventData) {

        Debug.Log("HER");
        other = Instantiate(burger, transform.position + Vector3.up * 110, Quaternion.identity, transform.parent) as GameObject;
        other.GetComponent<BurgersMeat>().SetEventData(eventData);

                ExecuteEvents.Execute<IEndDragHandler>(gameObject, eventData, ExecuteEvents.endDragHandler);
                eventData.pointerDrag = other;
                ExecuteEvents.Execute<IBeginDragHandler>(other, eventData, ExecuteEvents.beginDragHandler);


        //   data.pointerDrag = test;
        //   ExecuteEvents.Execute<IEndDragHandler>(gameObject, data, ExecuteEvents.endDragHandler);
        //  data = eventData;
        //  ExecuteEvents.Execute(test, data, ExecuteEvents.initializePotentialDrag);


        //  GameObject test = Instantiate(burger, transform.position + Vector3.up * 110, Quaternion.identity, transform.parent);
        //    test.AddComponent<Burgers>();
        //  test.GetComponent<Burgers>().OnBeginDrag(eventData);

        //     test.GetComponent<Burgers>().CurrentDropArea = ;
        //        test.GetComponent<Burgers>().ResetDropZone = transform;
        //   test.GetComponent<Burgers>().test(eventData);

        //      ExecuteEvents.Execute(test, eventData, ExecuteEvents.initializePotentialDrag);

        /*    GameObject test =  Instantiate(burger, transform.position + Vector3.up * 110, Quaternion.identity, transform.parent);
              test.GetComponent<Burgers>().OnBeginDrag(eventData);
              test.GetComponent<Burgers>().OnDrag(eventData);
          */
    }

    public void OnPointerUp(PointerEventData eventData) {
        
    }
}
