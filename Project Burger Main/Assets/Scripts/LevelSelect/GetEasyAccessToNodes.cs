using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEasyAccessToNodes : MonoBehaviour {

    public Node[] nodes;

    void Awake()
    {
        for (int i = 0; i < nodes.Length; i++)
            nodes[i].NodeIndexInArray = i;
    }
   
}
