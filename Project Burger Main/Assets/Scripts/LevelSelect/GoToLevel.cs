using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour {

    [HideInInspector] public int levelID = 0;
    
    public void EnterLevel() {
        SceneManager.LoadScene(levelID);
    }

}
