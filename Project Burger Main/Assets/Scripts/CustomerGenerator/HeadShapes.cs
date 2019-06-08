using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomerGender/BodyParts/HeadShape", fileName = "HeadShape")]
public class HeadShapes : ScriptableObject {

    public Sprite Head;
    public Sprite[] HairHats;
    public Sprite[] Eyes;
    public Sprite[] Nose;
    public Sprite[] Mouth;

    [Space(10)]
    public Vector3 HairHatPosition;
    public Vector3 EyePosition;
    public Vector3 NosePosition;
    public Vector3 MouthPosition;
    public Vector3 HeadBodyConnectPosition;
}

