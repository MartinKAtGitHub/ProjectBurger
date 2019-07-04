using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrillPlateStack : OnDropAreaInfo {

    [SerializeField]
    private GameObject _Ingredience = null;
    private BurgerDrag _SpawnedIngredients;

    [SerializeField]
    private bool _PresetSpawn = false;
    public int IngredienceSpawnAmout;


    private void Start() {
        if (_PresetSpawn) {
            for (int i = 0; i < IngredienceSpawnAmout; i++) {
                SpawnIngredience();
            }
        }
    }

    private void SpawnIngredience() {
        _SpawnedIngredients = Instantiate(_Ingredience, transform).GetComponent<Draggable>() as BurgerDrag;

        _SpawnedIngredients.ResetPositionParent = transform;
        _SpawnedIngredients.SetStartParent(transform.position + (Vector3.up * transform.childCount * 8) + (Vector3.up * 28.5f), transform);

        _SpawnedIngredients.GetComponent<RectTransform>().position = transform.position + (Vector3.up * transform.childCount * 8) + (Vector3.up * 28.5f);
    }

    public override void DropAreaOnBeginDrag() {

        if(transform.childCount < 1) {

            SpawnIngredience();

        }
    }
    
}
