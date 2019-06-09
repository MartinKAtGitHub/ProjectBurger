using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFlexibleHead : MonoBehaviour {

    public Image Head;
    public Image Mouth;
    public Image Eyes;
    public Image Nose;
    public Image HairHat;

    public void SetImages(GameObject theHead) {
        FlexibleCustomHead head = theHead.GetComponent<FlexibleCustomHead>();

        Head.sprite = head.Head[Random.Range(0, head.Head.Length)];
        Mouth.sprite = head.Mouth[Random.Range(0, head.Mouth.Length)];
        Eyes.sprite = head.Eyes[Random.Range(0, head.Eyes.Length)];
        Nose.sprite = head.Nose[Random.Range(0, head.Nose.Length)];
        HairHat.sprite = head.HairHat[Random.Range(0, head.HairHat.Length)];

        Head.transform.localPosition = theHead.transform.GetChild(0).transform.localPosition;
        Mouth.transform.localPosition = theHead.transform.GetChild(3).transform.localPosition;
        Eyes.transform.localPosition = theHead.transform.GetChild(1).transform.localPosition;
        Nose.transform.localPosition = theHead.transform.GetChild(2).transform.localPosition;
        HairHat.transform.localPosition = theHead.transform.GetChild(4).transform.localPosition;

    }
}