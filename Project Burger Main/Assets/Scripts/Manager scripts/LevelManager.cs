using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public QueueManager QueueManager;
    public CustomerSelect CustomerSelect;
    public FoodTrayDropArea FoodTrayDropArea;
    public CustomerSpawner CustomerSpawner;
    public SalesManager SalesManager;
    public ShuffleBag ShuffleBag;


    [SerializeField] private float _preparationTime;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
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
        CustomerSelect.Initialize();
        FoodTrayDropArea.Initialize();
        QueueManager.Initialize();

        StartCoroutine(CustomerSpawnSystem());
    }

    private IEnumerator CustomerSpawnSystem()
    {
        /*
         * Go though list and return customers at Random time. 
         * At the end of the list restart the list.
         *  
         */

        for (int i = 0; i < ShuffleBag.CustomerPool.Count; i++)
        {
            yield return new WaitUntil(() =>
            {
                Debug.Log("Waiting for Active Queue to be smaller then limit before spawning more customers");
                return QueueManager.ActiveQueueLimit.Count < QueueManager.MaxActiveCustomerAmount;

            }); // PERFORMANCE Levelmanager.cs | StartLevel() The bool is checked every frame, until it turns true

            yield return new WaitForSeconds(Random.Range(1, 2));
           // Debug.Log("SPAWINIG NEW DUDE");

            var customer = ShuffleBag.Next();
            QueueManager.AddCustomerToQueue(customer);

            yield return new WaitForEndOfFrame();

        }

    }
}
