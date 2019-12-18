using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturesAssemble : MonoBehaviour {

    public Sprite[] hats;
    public Sprite[] Face;
    public Sprite[] Accessories;
    public Sprite[] Body;

    public SpriteRenderer hat;
    public SpriteRenderer face;
    public SpriteRenderer accessory;
    public SpriteRenderer body;

    public bool change = false;
    private void Start() {

        hat.sprite = hats[Random.Range(0, hats.Length)];
        face.sprite = Face[Random.Range(0, Face.Length)];
        accessory.sprite = Accessories[Random.Range(0, Accessories.Length)];
        body.sprite = Body[Random.Range(0, Body.Length)];


    }
    private void Update() {
        if(change == true) {
            change = false;
            hat.sprite = hats[Random.Range(0, hats.Length)];
            face.sprite = Face[Random.Range(0, Face.Length)];
            accessory.sprite = Accessories[Random.Range(0, Accessories.Length)];
            body.sprite = Body[Random.Range(0, Body.Length)];

        }
    }


}
