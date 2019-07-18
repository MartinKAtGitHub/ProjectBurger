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
    public ScoreManager ScoreManager;
    public WinLooseManager WinLooseManager;

    [SerializeField] private float _preparationTime;
    [SerializeField] private Vector2 _customerSpawnTimerMinMax;

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

        StartCoroutine(CustomerSpawnSystemInit());
        Debug.Log("WAIT FOR FIRST TO SAPWN");
       
    }

    /// <summary>
    /// Starts the customer spawn system, we need a special case for the first customer that spawns which only happens once.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CustomerSpawnSystemInit()
    {
        yield return new WaitForSeconds(_preparationTime);

        var customer = ShuffleBag.Next();
        QueueManager.AddCustomerToQueue(customer);
        CustomerSelect.SetInitialCustomer();


        yield return new WaitForSeconds(3);
        StartCoroutine(CustomerSpawnSystemLoop());
    }
    /// <summary>
    /// This is th main customer spawn loop. At the end of the list this method will be called again.
    /// </summary>
    private IEnumerator CustomerSpawnSystemLoop()
    {
        for (int i = 0; i < ShuffleBag.CustomerPool.Count; i++)
        {
            yield return new WaitUntil(() =>
            {
                Debug.Log("Waiting for Active Queue to be smaller then limit before spawning more customers");
                return QueueManager.ActiveQueueLimit.Count < QueueManager.MaxActiveCustomerAmount;

            }); // PERFORMANCE Levelmanager.cs | StartLevel() The bool is checked every frame, until it turns true

            yield return new WaitForSeconds(Random.Range(_customerSpawnTimerMinMax.x, _customerSpawnTimerMinMax.y));
      
            var customer = ShuffleBag.Next();
            QueueManager.AddCustomerToQueue(customer);

            yield return null;
        }
        // Signal end of loop, maybe have an event and fire this metod again
        // OR StartCoroutine(CustomerSpawnSystemLoop()); call it again, might crash 
    }
}
