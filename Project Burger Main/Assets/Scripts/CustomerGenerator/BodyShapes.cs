using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomerGender/BodyParts/BodyShape", fileName = "BodyShape")]
public class BodyShapes : ScriptableObject {

    public Sprite Body;
    //    public Sprite[] Ties;
    //    public Sprite[] Shirts;

    [Space(10)]
    //    public Vector3 TiesPosition;
    //    public Vector3 ShirtPosition;
    public Vector3 HeadBodyConnectPosition;

}

