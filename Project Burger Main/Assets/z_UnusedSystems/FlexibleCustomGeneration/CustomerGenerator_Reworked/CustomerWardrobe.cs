using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWardrobe : MonoBehaviour {
    //Special Theme Day, Lets Say Black Panther Day, Where The Majority Is Ppl Of Aftican Ethnicity. And So On... How To Separate So That Black Ppl Spawn More Often,
    //2 ways. Make Prefabs Of Different Groups Of Ppl, And Put That Into The Spawner. Or Make Sprite Groups And Let The Theme Say Which Sprite Groups To Spawn.

    //Make A Script That Hold The Refrence To All States Of Customer, Male Fat, Male Nerd, Male African, Male Indian, Male Norwegian And So On... 
    //Then There Is A Theme Script That Somehow Increases The Spawn Rate Of Specific Kind Of Customers.
    //Can Be A Struct That Hold CustomerType And An Int And The Manager Loops Through The Struct To Add The Values To The Spawner

    public GameObject CustomerPrefab;

    RectTransform RTTorso;
    RectTransform RTHead;

    Image SPTorso;
    Image SPHead;
    Image SPEye;
    Image SPNose;
    Image SPMouth;
    Image SPHair;

    OrderGenerator CustomerOrderG;

    private void Awake() {
        SetupConnections();
    }

    public void SetupConnections() {
        RTTorso = CustomerPrefab.transform.GetChild(0).GetComponent<RectTransform>();
        SPTorso = CustomerPrefab.transform.GetChild(0).GetChild(0).GetComponent<Image>(); //Torso

        RTHead = CustomerPrefab.transform.GetChild(1).GetComponent<RectTransform>();
        SPHead = CustomerPrefab.transform.GetChild(1).GetChild(0).GetComponent<Image>(); //Head
        SPEye = CustomerPrefab.transform.GetChild(1).GetChild(1).GetComponent<Image>(); //Eye
        SPNose = CustomerPrefab.transform.GetChild(1).GetChild(2).GetComponent<Image>(); //Nose
        SPMouth = CustomerPrefab.transform.GetChild(1).GetChild(3).GetComponent<Image>(); //Mouth
        SPHair = CustomerPrefab.transform.GetChild(1).GetChild(4).GetComponent<Image>(); //Hair

        CustomerOrderG = CustomerPrefab.GetComponent<OrderGenerator>();
    }







    public void CreatCustomer() {



    }

    public void SettingUpCustomer(CustomerRefrences a) {
       
        RTTorso.transform.localPosition = a.TorsoRectTransform.transform.localPosition;
        RTTorso.sizeDelta = a.TorsoRectTransform.sizeDelta;
        SPTorso.sprite = a.TorsoSprites.Torsos[Random.Range(0, a.TorsoSprites.Torsos.Length)];

        RTHead.transform.localPosition = a.HeadRectTransform.transform.localPosition;
        RTHead.sizeDelta = a.HeadRectTransform.sizeDelta;
        SPHead.sprite = a.HeadSprites.Heads[Random.Range(0, a.HeadSprites.Heads.Length)];
        SPEye.sprite = a.HeadSprites.Eyes[Random.Range(0, a.HeadSprites.Eyes.Length)];
        SPNose.sprite = a.HeadSprites.Noses[Random.Range(0, a.HeadSprites.Noses.Length)];
        SPMouth.sprite = a.HeadSprites.Mouths[Random.Range(0, a.HeadSprites.Mouths.Length)];
        SPHair.sprite = a.HeadSprites.Hair[Random.Range(0, a.HeadSprites.Hair.Length)];

       // CustomerOrderG.RequestOrderTrial(a.RecipeB, a.MultiorderAmount, a.MultiorderChance);
        Instantiate(CustomerPrefab, transform.position, Quaternion.identity, transform.parent.GetChild(0));

    }


    void RandomCustomerSetup() {

    }

    







}
