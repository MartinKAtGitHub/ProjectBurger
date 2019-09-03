using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgeringredientlayertest : MonoBehaviour {

    public float NormalizingPositionValue = 0;//Bottom Point Of The Ingredient. If You Want Some Parts To Be Hanging Out Like With The Lettuce, Then Dont Have The Position At The Bottom But Abit Up To Where You Want The Bottom Position To be.
    public float IngredientOffset = 0;//Normaly The Ingredient Is Increased By 1 Pixel At A Time, But If You Want To Offset That, To Spawn The Ingredient Abit Further Down Or Up.
    public float NextIngredientOffset = 0;//This Is For If We Add Something That We Want To Stick Out (like the tomatoe) And Force The Next Ingredient To Be Pushed Abit More Up Then Usual.

}
