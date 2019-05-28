using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TheGrill : DropArea {

    public GameObject TimerObject;
    public float TemperatureSpeed = 2;


    Text Temperature;
    bool StartBurger = false;
    float BurgerHeat = 10;
    float BurgerOffTime = 0;

    bool BlinkOnceASec = false;
    bool Once = false;

    private void Start() {
        Temperature = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }


    public override void OnDrop(PointerEventData eventData) {

        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Ingredient>() != null) {//The Problem Here Is If I Drag Something And Drop It On Here, I Will Get An Error, So I Need A Check Of Some Sort. eventData.pointerDrag != null, will never happen but just for safety.

            if (eventData.pointerDrag.GetComponent<Ingredient>().IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {
                base.OnDrop(eventData);
                StartTimers();
            }

        }
        //    AddIngredientsToFoodStack(eventData);
    }

    public override void DropAreaOnBeginDrag() {
        //    RemoveIngredientFromFoodStack();

        StartBurger = false;


    }





    void StartTimers() {

        StartBurger = true;

        //  GameObject.Find("Temp").GetComponent<Animator>().speed = 1f / TemperatureSpeed;
        //   anim["name of animation"].speed = animSpeed;

    }


    private void Update() {
        


        if(StartBurger == true) {
            BurgerHeat += Time.deltaTime;
            if(BurgerHeat >= 200) {
                StartBurger = false;
            }

    //        Temperature.text = Mathf.Floor(BurgerHeat) + "*";
        } else {

            if(BlinkOnceASec == true) {

      //          if()

                





            }



        }



    }






}
