using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCustomSpawner : MonoBehaviour {

    //   public float MinimumTimeOfEmptyQueue = 5f;

    float TimeWaited = 0f;
    public float spawntime = 30;
 //   public float timetime = 0;

    GameObject spawned;

    public bool MakeArtificialQueue = false;//This Allows Good Player Performance To Spawn More Customer More Often.
 //   public float ArtificialQueueFallOff = 1f;

    int ArtificialQueueLength = 0;
    float _ArtificialTimeWaited = 0;


    bool StartSpawner = false;
    bool SpeedSpawn = true;

    [SerializeField]
    private CustomerWardrobe _CustomerCreation = null;
    [SerializeField]
    private ThemeDaySetup _ThemeDayCustomers = null; 

    void Start() {

        StartSpawning();

    }



    void Update() {

        if (StartSpawner == true) {


            if (ArtificialQueueLength > 0 && _ArtificialTimeWaited < Time.time) {//Made An Artificial Queue To Increase The Spawn Amount If We Have A Full Queue For The Next Customer.

                if (LevelManager.Instance.QueueManager.NumOfActiveCustomers < LevelManager.Instance.QueueManager.ActiveQueueLimit) {
                    SetAditionalSpawningInfo();
                    ArtificialQueueLength--;
                } else {
                    _ArtificialTimeWaited = Time.time + (spawntime * 0.2f);//If False Wait Another x Sec This Will Add Some Rng To The Spawn Time.
                    TimeWaited = Time.time + (spawntime * 0.8f);
                }
            } else {

                if (TimeWaited < Time.time) {//When Its Time To Spawn A Customer This Is True 
                    TimeWaited = Time.time + spawntime;

                    Debug.Log(LevelManager.Instance.QueueManager.NumOfActiveCustomers + " | " + LevelManager.Instance.QueueManager.ActiveQueueLimit);
                    if (LevelManager.Instance.QueueManager.NumOfActiveCustomers < LevelManager.Instance.QueueManager.ActiveQueueLimit) {//Spawning Customer In Here
                        SetAditionalSpawningInfo();
                    } else {//Adding To Artificial Queue
                        if (MakeArtificialQueue == true) {//When Added To Artificial Queue, Make The Timer 20% Of Normal SpawnTime.
                            _ArtificialTimeWaited = Time.time + (spawntime * 0.2f);
                            ArtificialQueueLength++;
                        }
                    }

                }

                if (SpeedSpawn == false && LevelManager.Instance.QueueManager.NumOfActiveCustomers == 0) {//When There Are NoOne In The Queue Spawn Customer At An Increased Rate
                    SpeedSpawn = true;

                    TimeWaited = Time.time + (spawntime * 0.8f); //Start Spawn Time

                }
            }
        }

    }

    void SetAditionalSpawningInfo() {
        spawned = _CustomerCreation.SettingUpCustomer(_ThemeDayCustomers.CusomterSpawnRandom(), this);//Getting Customer To Spawn
        spawned = Instantiate(spawned, transform.position, Quaternion.identity, gameObject.transform) as GameObject;

        spawned.gameObject.SetActive(false);

        LevelManager.Instance.QueueManager.AddCustomerToQueue(spawned.GetComponent<Customer>());
        SpeedSpawn = false;
    }



    public void StartSpawning() {
        StartSpawner = true;
        TimeWaited = Time.time + (spawntime * 0.8f); //Start Spawn Time
    }

}
