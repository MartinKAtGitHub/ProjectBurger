using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCustomSpawner : MonoBehaviour {

    public int MaxActiveCustomers = 5;

    public float MinimumTimeOfEmptyQueue = 5f;
    float TimeWaited = 0f;
    public float spawntime = 30;

    QueueManager _TheQueue;
    bool SpawnCustomer = false;

    public bool MakeArtificialQueue = false;//This Allows Good Player Performance To Spawn More Customer More Often.
    public float ArtificialQueueFallOff = 1f;
    int ArtificialQueueLength = 0;


    float _ArtificialTimeWaited = 0;
    CustomerWardrobe _CustomerCreation;
    ThemeDaySetup _ThemeDayCustomers;


    // Start is called before the first frame update
    void Start()
    {
        _CustomerCreation = GetComponent<CustomerWardrobe>();
        _ThemeDayCustomers = GetComponent<ThemeDaySetup>();

        _TheQueue = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<QueueManager>();
    

        for (int i = 0; i < 5; i++) {
            _CustomerCreation.SettingUpCustomer(_ThemeDayCustomers.CusomterSpawnRandom());
        }

     Debug.Log(   LevelManager.Instance.gameObject.name);

    }
    // Update is called once per frame
    void Update()
    {









        if (ArtificialQueueLength > 0 && _ArtificialTimeWaited < Time.time) {
            if (_TheQueue.ActiveQueueLimit.Count < _TheQueue.MaxActiveCustomerAmount) {
                //Spawn
                ArtificialQueueLength--;
            } else {
                _ArtificialTimeWaited = Time.time + (spawntime * 0.2f);//If False Wait Another x Sec This Will Add Some Rng To The Spawn Time.
                TimeWaited = Time.time + (spawntime * 0.8f);
            }
        } else {

            if (TimeWaited + spawntime < Time.time) {//When Its Time To Spawn A Customer This Is True 
                TimeWaited = Time.time;

                if (_TheQueue.ActiveQueueLimit.Count < _TheQueue.MaxActiveCustomerAmount) {
                    //Spawn
                } else {

                    if (MakeArtificialQueue == true) {//When Added To Artificial Queue, Make The Timer 20% Of Normal SpawnTime.
                        _ArtificialTimeWaited = Time.time + (spawntime * 0.2f);
                        ArtificialQueueLength++;
                    }
                }

            }

        }




        if (SpawnCustomer == false) {

            if (_TheQueue.ActiveQueueLimit.Count == 0) {
                TimeWaited = Time.time;
                SpawnCustomer = true;
            }

        }else {






        }


        if(TimeWaited + MinimumTimeOfEmptyQueue < Time.time) {





        } else {




        }


    }
}
