using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TheGrill : DropArea {

    


    public override void OnDrop(PointerEventData eventData) {

        if(eventData.pointerDrag.GetComponent<Ingredient>().IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {
            base.OnDrop(eventData);
            StartTimers();
        }

    //    AddIngredientsToFoodStack(eventData);
    }

    public override void DropAreaOnBeginDrag() {
    //    RemoveIngredientFromFoodStack();
    }





    void StartTimers() {




    }









}
