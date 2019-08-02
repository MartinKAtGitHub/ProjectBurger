using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerlayertest : MonoBehaviour {

    public burgeringredientlayertest[] ingredients;

    public float nextPos = 0;
    public float NextIngredientPos = 1;
    public float pixelVal = 6.25f;

    private void Start() {

        for (int i = 0; i < ingredients.Length; i++) {

            ingredients[i].transform.localPosition = Vector3.up * ((nextPos - (pixelVal * ingredients[i].NormalizingPositionValue) + (pixelVal * ingredients[i].IngredientOffset)));
            nextPos += (pixelVal * (NextIngredientPos + ingredients[i].NextIngredientOffset));

        }

    }

}
