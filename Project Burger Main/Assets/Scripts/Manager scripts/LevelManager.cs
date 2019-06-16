using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LevelManager : MonoBehaviour
{
    public QueueManager QueueManager;
    public CustomerSelect CustomerSelect;
    public FoodTrayDropArea FoodTrayDropArea;
    public CustomerSpawner CustomerSpawner;
    public SalesManager SalesManager;

    [SerializeField] private float _preparationTime;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Found another LevelManager in the same Scene, make sure only 1 LevelManager exist per scene");
            Destroy(gameObject); // Destroy myself is Instance already has a ref
        }
    }

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
