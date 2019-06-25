using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureMeasurer : MonoBehaviour {

    Image _TempImage;
    TheGrill _GrillHeat;

    void Start() {
        _TempImage = GetComponent<Image>();
        _GrillHeat = transform.parent.GetComponent<TheGrill>();
    }

    void Update() {

        if (_GrillHeat.DropAreaOccupied == true) {

            if (_GrillHeat.HeatOfBurger >= _GrillHeat.EndHeat) {
                _TempImage.fillAmount = _GrillHeat.EndHeat;
                _TempImage.color = _GrillHeat.BurgerIngredience.MeatTimers.Evaluate(_GrillHeat.EndHeat);
            } else {
                _TempImage.fillAmount = _GrillHeat.HeatOfBurger;
                _TempImage.color = _GrillHeat.BurgerIngredience.MeatTimers.Evaluate(_GrillHeat.HeatOfBurger);
            }
        }
    }
    
}
