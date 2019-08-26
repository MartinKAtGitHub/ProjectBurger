using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour {

    /// <summary>
    /// This Is Called If The Node Is The Final Node
    /// </summary>
    public virtual void SteppingOnEndNodeBehaviour() {
        Debug.Log("Calling this method when entering a node, seems to be empty (should not be called)");
    }

    /// <summary>
    /// This Is Called For Every Node That Is Walked Upon.
    /// </summary>
    public virtual void TransitionNodeBehaviour() {
        Debug.Log("Calling this method when entering a node, seems to be empty (should not be called)");
    }


    /// <summary>
    /// This Is Called At The Start When The Player Starts Moving
    /// </summary>
    public virtual void SteppingOffEndNodeBehaviour() {
        Debug.Log("Calling this method when entering a node, seems to be empty (should not be called)");
    }


}
