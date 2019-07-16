using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedQueueDotIndicators : MonoBehaviour
{

    [SerializeField] private GameObject _queueDotContainerPrefab;
    [SerializeField] private RectTransform _queueDotParent;
    //[SerializeField] private List<GameObject> _queuePositions = new List<GameObject>();

    private GameObject[] _queuePositions;
    private QueueManager _queueManager;
    private GameObject _dotInFocus;

    void Awake()
    {
        _queueManager = GetComponent<QueueManager>();
        GenerateQueueDotContainers();
    }

    private void Start()
    {
        
    }
    
    private void GenerateQueueDotContainers()
    {
        var queueLimit = _queueManager.ActiveQueueLimit;
        _queuePositions = new GameObject[queueLimit];

        for (int i = 0; i < queueLimit; i++)
        {
            var clone = Instantiate(_queueDotContainerPrefab, _queueDotParent);
            //_queuePositions.Add(clone);
            _queuePositions[i] = clone;
        }
    }

    public void SetDotFocus(int index)
    {
        if (_dotInFocus != null)
        {
            _dotInFocus.transform.GetChild(0).GetChild(2).gameObject.SetActive(false); // Old Dot
            _dotInFocus = _queuePositions[index];
            _dotInFocus.transform.GetChild(0).GetChild(2).gameObject.SetActive(true); // Updated new Dot

        }
        else
        {
            _dotInFocus = _queuePositions[index];
            _dotInFocus.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
    }
}
