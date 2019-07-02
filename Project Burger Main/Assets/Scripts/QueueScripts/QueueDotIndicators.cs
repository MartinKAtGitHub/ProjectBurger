using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueDotIndicators : MonoBehaviour
{
    [SerializeField] private GameObject _queueDotPositionPrefab;
    [SerializeField] private RectTransform _queueDotParent;
    [SerializeField] private List<GameObject> _queuePositions = new List<GameObject>();

    private QueueManager _queueManager;
    private GameObject _dotInFocus;


    private void Start()
    {
        _queueManager = LevelManager.Instance.QueueManager;
       // DebugWarning();
    }

    public void TimeOutDots()
    {

    }

    public GameObject SpawnDot()
    {
        var clone = Instantiate(_queueDotPositionPrefab, _queueDotParent);
        _queuePositions.Add(clone);
        // Play anim Fade in
        return clone;
    }

    public void RemoveDots(GameObject dot)
    {
        _queuePositions.Remove(dot);
        Destroy(dot); // PERFORMANCE QueueDot.cs | this can cause lags, might need to pool our characters
        // play fade out anim
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


    //private void DebugWarning()
    //{
    //    if (_dotInFocus.transform.childCount > 1 || _dotInFocus.transform.GetChild(0).childCount < 1)
    //    {
    //        Debug.LogError("QueueDotIndector.cs |  QueuePositionPrefb Child count has been Changed, Code uses child index Make sure not to remove/change pos GFX");
    //    }
    //    if (_dotInFocus.transform.GetChild(0).childCount > 3 || _dotInFocus.transform.GetChild(0).childCount < 3)
    //    {
    //        Debug.LogError("QueueDotIndector.cs |  QueuePositionPrefb Child count has been Changed, Code uses child index ");
    //    }
    //}
}
