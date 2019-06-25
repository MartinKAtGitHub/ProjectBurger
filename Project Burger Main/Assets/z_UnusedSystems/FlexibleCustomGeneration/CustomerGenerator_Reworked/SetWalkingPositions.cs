using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TransformArray {
    public TransformArray2[] GroupInGroups;

}

[System.Serializable]
public class TransformArray2 {
    public TransformArray3[] PathInGroup;

}

[System.Serializable]
public class TransformArray3 {
    public Transform[] PositionsInPath;
    public bool Talking = false;
    public float Patience = 1;

}


//OOF..... This Allows Me To Make A Set Of Sets Of Walking Positions Not Pretty But It Does The Job...
public class SetWalkingPositions : MonoBehaviour {

    [SerializeField]
    public TransformArray[] WalkingPossibilities;

             
}
