using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDisplay : MonoBehaviour {
   
    public void CloseTheDisplay() {
        transform.parent.gameObject.SetActive(false);
    }

}
