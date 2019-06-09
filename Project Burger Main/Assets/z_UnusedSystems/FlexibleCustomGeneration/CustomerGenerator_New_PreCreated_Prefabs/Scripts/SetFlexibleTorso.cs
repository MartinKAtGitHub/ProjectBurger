using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFlexibleTorso : MonoBehaviour
{
    public Image Torso;
    //    public Image Tie;
    //    public Image Shirt;

    public void SetImages(GameObject theTorso) {
        FlexibleCustomTorso torsoShape = theTorso.GetComponent<FlexibleCustomTorso>();

        Torso.sprite = torsoShape.Torso[Random.Range(0, torsoShape.Torso.Length)];
        Torso.rectTransform.sizeDelta = Torso.sprite.rect.size;//Im Not Sure Why Sprite.rect.size Should Work, But Looks Like rect.size == Sprite Pixel Size

        //        Tie.sprite = bodyShape.Ties[Random.Range(0, bodyShape.Ties.Length)];
        //        Shirt.sprite = bodyShape.Shirts[Random.Range(0, bodyShape.Shirts.Length)];

        //        Tie.transform.localPosition = bodyShape.Ties;
        //        Shirt.transform.localPosition = bodyShape.Shirts;

    }
}
