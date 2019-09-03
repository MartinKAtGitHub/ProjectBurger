using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zGiefScore : MonoBehaviour
{
    public int levelID = 100;

    public void EnterLevel() {
        LevelManager.Instance.ScoreManager.AddScore(100);
    }
}
