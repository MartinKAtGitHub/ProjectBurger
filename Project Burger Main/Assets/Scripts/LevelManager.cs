using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public QueueManager QueueManager;
    public CustomerSelect CustomerSelect;
    public FoodTrayDropArea FoodTrayDropArea;
    public CustomerSpawner CustomerSpawner;

    [SerializeField]
    private float _preparationTime;

    void Start()
    {
        // StartCorutine(StartLevel)
        //CustomerSpawner.SpawnCustomer();
       // CustomerSpawner.SpawnCustomer();
        CustomerSpawner.SpawnCustomer();
        CustomerSelect.SelectInitialCustomer();

    }

    private IEnumerator StartLevel()
    {
        // Wait for prep time
        // Enable/Start Customer spawner
        yield return null;
    }
}
