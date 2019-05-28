using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureMeasurer : MonoBehaviour {



    public Gradient MeatTimers;//This is what tells the user what stage in the cooking prosess the burger is in.
    public float MeatCookingTime = 1;//How long does the burger take to cook.

    public int KeyCounter = 0;//a counter that counts how many gradient keyframes that it has passed.
    public float KeyTimer = 0;//timer to store when the next gradient keyframe is.

    Image TempImage;


    public float timer;//The time holder.
    public bool StartTimer = false;

    // Start is called before the first frame update
    void Start() {
        TempImage = GetComponent<Image>();
        timer = 0.125f;
        KeyTimer = MeatTimers.colorKeys[0].time;
        KeyCounter = 0;
    }

    // Update is called once per frame
    void Update() {

        if (StartTimer == true) {

            if (timer < 0.905f) {
                timer += (1 / MeatCookingTime) * Time.deltaTime;

                if (timer > 0.905f) {
                    StartTimer = false;
                    timer = 0.905f;
                }

                if (timer >= KeyTimer) {
                    if (KeyCounter < MeatTimers.colorKeys.Length)
                        KeyTimer = MeatTimers.colorKeys[++KeyCounter].time;
                }

                TempImage.fillAmount = timer;
                TempImage.color = MeatTimers.Evaluate(timer);

            }
        }
    }
}
