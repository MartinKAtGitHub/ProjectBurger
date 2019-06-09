using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomerGender/CustomerGroup", fileName = "Customer")]

public class CustomerGender : ScriptableObject {

    public int Hungryness;
    public int PatienceTime;

    public HeadShapes HeadShape;
    public BodyShapes BodyShape;

}
