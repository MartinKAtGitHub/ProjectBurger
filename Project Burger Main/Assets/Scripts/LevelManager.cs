using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public QueueManager QueueManager;
    public CustomerSelect CustomerSelect;
    public FoodTrayDropArea FoodTrayDropArea;
    public CustomerSpawner CustomerSpawner;

    void Start()
    {
        // StartCorutine(StartLevel)
        CustomerSpawner.SpawnCustomer();
        CustomerSelect.SelectInitialCustomer();

    }


    void Update()
    {

    }

}
