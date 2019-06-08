using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeadShape : MonoBehaviour {

    public Image Head;
    public Image Mouth;
    public Image Eyes;
    public Image Nose;
    public Image HairHat;

    public void SetImages(HeadShapes headShape) { 

        Head.sprite = headShape.Head;
        Mouth.sprite = headShape.Mouth[Random.Range(0, headShape.Mouth.Length)];
        Eyes.sprite = headShape.Eyes[Random.Range(0, headShape.Eyes.Length)];
        Nose.sprite = headShape.Nose[Random.Range(0, headShape.Nose.Length)];
        HairHat.sprite = headShape.HairHats[Random.Range(0, headShape.HairHats.Length)];

        Mouth.transform.localPosition = headShape.MouthPosition;
        Eyes.transform.localPosition = headShape.EyePosition;
        Nose.transform.localPosition = headShape.NosePosition;
        HairHat.transform.localPosition = headShape.HairHatPosition;

        /*   Head.sprite = head;
           Mouth.sprite = mouth;
           Eyes.sprite = eyes;
           Nose.sprite = nose;
           HairHat.sprite = hairHat;*/
    }

}
