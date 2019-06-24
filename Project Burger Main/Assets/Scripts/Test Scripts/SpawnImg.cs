using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnImg : MonoBehaviour
{
    [SerializeField] private GameObject img;
    [SerializeField] private Transform imgParaent;

    private int num = 0;

    public void SpawnImgOnClick()
    {
        var clone = Instantiate(img, imgParaent);
        clone.name += num++;
        clone.transform.SetAsFirstSibling();
    }
}
