using UnityEngine.EventSystems;

//In The Future, Make The Burger Lose Its Freshness If On The Wait Area For To Long?
public class BurgerWaitArea : DropArea {

    public Ingredient.IngredientTypes WhatCanDropOnHere;
    public bool ConstantHeat = true;

    public override void OnDrop(PointerEventData eventData) {

        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<IngredientGameObject>() != null) {//The Problem Here Is If I Drag Something And Drop It On Here, I Will Get An Error, So I Need A Check Of Some Sort. eventData.pointerDrag != null, will never happen but just for safety.

            if (eventData.pointerDrag.GetComponent<IngredientGameObject>().ingredient.IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {
                IsThisDropAreaOccupied = true;
                base.OnDrop(eventData);
            }
        }
    }

    
    public override void DropAreaOnBeginDrag() {//Calculate Burger Freshness :D
        if(ConstantHeat == true) {//TODO Do This / Burger Getting Old/Cold

        }
        IsThisDropAreaOccupied = false;

    }

}
