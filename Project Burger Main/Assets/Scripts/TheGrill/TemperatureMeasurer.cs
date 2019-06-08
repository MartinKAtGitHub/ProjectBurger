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

            if(_GrillHeat.BurgerTemperature < 0.125f) {
                _TempImage.fillAmount = 0.125f;
                _TempImage.color = _GrillHeat._BurgerIngredience.MeatTimers.Evaluate(0.125f);
            } else {
                _TempImage.fillAmount = _GrillHeat.BurgerTemperature;
                _TempImage.color = _GrillHeat._BurgerIngredience.MeatTimers.Evaluate(_GrillHeat.BurgerTemperature);
            }

    }
    
}
