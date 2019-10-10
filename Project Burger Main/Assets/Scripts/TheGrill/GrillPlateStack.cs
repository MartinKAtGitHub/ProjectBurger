using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrillPlateStack : DropArea {

    [SerializeField]
    private GameObject _Ingredience = null;
    private BurgerDrag _SpawnedIngredients;

    private RectTransform _thisRectTransform;

    [SerializeField]
    private bool _PresetSpawn = false;
    public int IngredienceSpawnAmout;

    int StackCount = -1;
    BurgerDrag BurgerCheck;

    private void Awake()
    {
        _thisRectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        if (_PresetSpawn) {
            for (int i = 0; i < IngredienceSpawnAmout; i++) {
                SpawnIngredience();
            }
        }
    }

    private void SpawnIngredience() {
        _SpawnedIngredients = Instantiate(_Ingredience, transform).GetComponent<BurgerDrag>();

        _SpawnedIngredients.ResetPositionParent = _thisRectTransform;
        _SpawnedIngredients.GetComponent<RectTransform>().position = transform.position + (Vector3.up * ++StackCount * 8) + (Vector3.up * 28.5f);
    }


    public override void DropAreaOnBeginDrag() {
        StackCount -= 1;

        for(int i = 0; i <= StackCount; i++) {
            transform.GetChild(i).transform.position = transform.position + (Vector3.up * i * 8) + (Vector3.up * 28.5f);
        }

        if (StackCount < 0) {
            SpawnIngredience();

        }
    }

    public override void OnDrop(PointerEventData eventData) {
        
        BurgerCheck = eventData.pointerDrag.GetComponent<BurgerDrag>() ;

        if(BurgerCheck != null) {

            if(BurgerCheck.TheBurgerInfos.MyVariablesUp._BurgerHeat == 0 && BurgerCheck.TheBurgerInfos.MyVariablesDown._BurgerHeat == 0) {
                StackCount += 1;
                BurgerCheck.ResetPositionParent = _thisRectTransform;
                BurgerCheck.GetComponent<RectTransform>().position = transform.position + (Vector3.up * StackCount * 8) + (Vector3.up * 28.5f);
            }
        } 
    }

}
