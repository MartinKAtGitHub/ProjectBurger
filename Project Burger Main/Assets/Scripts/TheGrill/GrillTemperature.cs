using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Was Thinking About Having A Display Setting The Heat Values Of The Grill, Currently It's In Its Early Stage But It's Set Up So That It Can Be Expaneded At A later Point
public class GrillTemperature : MonoBehaviour {

    public bool HeatOnOff = true;
    public float GrillTopTemperatureValue = 60;

    public float GrillHeatingRate = 0.5f;
    public float GrillCoolingRate = 1.5f;//The Cooling Happends Faster Then The Heating, Cuz Future Ideas (TODO Laters, But Completed For Now)

}