using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMapPositions : MonoBehaviour {

    [SerializeField]
    private Node _startNodes;

    [SerializeField]
    private AreaPoints _points;


    public Node StartNode { get => _startNodes; }
    public AreaPoints MapPoints { get => _points; }




}
