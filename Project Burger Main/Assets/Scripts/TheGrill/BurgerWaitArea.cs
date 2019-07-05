using UnityEngine.EventSystems;
using UnityEngine;

//In The Future, Make The Burger Lose Its Freshness If On The Wait Area For To Long?
public class BurgerWaitArea : DropArea {

    public Ingredient.IngredientTypes WhatCanDropOnHere;
    public bool ConstantHeat = true;
    BurgerDrag _DropOnObject;
    bool _IsSomethingHere = false;


    public override void OnDrop(PointerEventData eventData) {

        if (_IsSomethingHere == false) {

            _DropOnObject = eventData.pointerDrag.GetComponent<BurgerDrag>();

            if (_DropOnObject != null) {//The Problem Here Is If I Drag Something And Drop It On Here, I Will Get An Error, So I Need A Check Of Some Sort. eventData.pointerDrag != null, will never happen but just for safety.
                if (_DropOnObject.TheIngredientGameObject.ingredient.IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {

                    _DropOnObject.ResetPositionParent = transform;
                    _DropOnObject.transform.position = transform.position;

                    _IsSomethingHere = true;

                }
            }

        }
    }


    public override void DropAreaOnBeginDrag() {//Calculate Burger Freshness :D
        if (ConstantHeat == false) {//TODO Do This / Burger Getting Old/Cold
            //TODO Make Burger Lose Its Freshness;
        }
        _IsSomethingHere = false;

    }
}
