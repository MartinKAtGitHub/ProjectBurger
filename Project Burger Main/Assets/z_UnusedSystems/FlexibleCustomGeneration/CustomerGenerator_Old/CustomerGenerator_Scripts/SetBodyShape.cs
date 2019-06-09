using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBodyShape : MonoBehaviour {

    public Image Body;
//    public Image Tie;
//    public Image Shirt;

    public void SetImages(BodyShapes bodyShape) {

        Body.sprite = bodyShape.Body;
        Body.rectTransform.sizeDelta = bodyShape.Body.rect.size;//Im Not Sure Why Sprite.rect.size Should Work, But Looks Like rect.size == Sprite Pixel Size

        //        Tie.sprite = bodyShape.Ties[Random.Range(0, bodyShape.Ties.Length)];
        //        Shirt.sprite = bodyShape.Shirts[Random.Range(0, bodyShape.Shirts.Length)];

        //        Tie.transform.localPosition = bodyShape.Ties;
        //        Shirt.transform.localPosition = bodyShape.Shirts;

    }

}
