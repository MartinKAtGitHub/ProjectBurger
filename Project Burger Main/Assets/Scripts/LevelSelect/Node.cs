using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField]
    public NodeBehaviour ThisNodesBehaviours;




    [HideInInspector] public bool NodeSearchedThrough = false;
    [HideInInspector] public float PosX = 0;
    [HideInInspector] public float PosY = 0;

    public List<Node> Neighbour = new List<Node>();
    [HideInInspector] public Node Parent;

    [HideInInspector] public float hCost = 0;//HCost is how far away the end node is from this node
    [HideInInspector] public float gCost = 0;//GCost is the cost that have been used to get to this node
    [HideInInspector] public float fCost = 0;//Gcost+Hcost (Distance to travel to + Dist to end)

    private void Start() {
        PosX = transform.position.x;
        PosY = transform.position.y;
    }

    public void SetHCost(Node theEnd) {
        hCost = Mathf.Abs((PosX - theEnd.PosX) + (theEnd.PosY - PosY));
    }

    public void StartNode(Node theParent, Node theEnd) {//setting parent gcost and hcost
        NodeSearchedThrough = true;
        Parent = theParent;

        hCost = Mathf.Abs((PosX - theEnd.PosX) + (theEnd.PosY - PosY));
        gCost = 0;
        fCost = hCost;

    }

    public void SetFirstAdding(Node theParent, Node theEnd) {//setting parent gcost and hcost
        NodeSearchedThrough = true;
        Parent = theParent;

        hCost = Mathf.Abs((PosX - theEnd.PosX) + (theEnd.PosY - PosY));

        if (Parent.PosX != PosX && Parent.PosY != PosY) {//If Corner Node Or Node Not In A Straight Line
            gCost = (1 * 1.4f) + Parent.gCost;
        } else {
            gCost = Parent.gCost;
        }
        gCost += Mathf.Abs((PosX - Parent.PosX) + (Parent.PosY - PosY));

        fCost = hCost + gCost;
    }

    public void SetNewParent(Node theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost
        Parent = theParent;

        if (Parent.PosX != PosX && Parent.PosY != PosY) {//If Corner Node Or Node Not In A Straight Line
            gCost = (1 * 1.4f) + Parent.gCost;
        } else {
            gCost = Parent.gCost;
        }
        gCost += Mathf.Abs((PosX - Parent.PosX) + (Parent.PosY - PosY));

        fCost = hCost + gCost;

    }

}