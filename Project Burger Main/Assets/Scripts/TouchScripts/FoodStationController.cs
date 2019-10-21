using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches the Food station on button click
/// </summary>
public class FoodStationController : MonoBehaviour
{
    [Tooltip("The time it takes to transition to another station")]
    [SerializeField] private float _timeToArrive = 0.5f;
    [Tooltip("The RectTransform which will be moved on Left Right btn press")]
    private RectTransform _stationContainer;


    private int _stationCount;
    private int _stationIndex;
    private float _stationWidth;
    private bool _inTransition;


    void Awake()
    {
        _stationContainer = GetComponent<RectTransform>();
        _stationCount = transform.childCount - 1;
        _stationWidth = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        SetDeafultStation();
    }

    IEnumerator SmoothTransition(Vector2 startPos, Vector2 endPos, float sec)
    {
        _inTransition = true;
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _stationContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        _inTransition = false;
        yield return null;
    }


    public void NextStation()
    {
        if (!_inTransition)
        {
            if (_stationIndex < _stationCount)
            {
                _stationIndex++;

                var newPos = _stationContainer.anchoredPosition;
                newPos += new Vector2(-1 * (_stationWidth), 0);

                StartCoroutine(SmoothTransition(_stationContainer.anchoredPosition, newPos, _timeToArrive));
            }
        }
    }

    public void PreviousStation()
    {
        if (!_inTransition)
        {
            if (_stationIndex > 0)
            {
                _stationIndex--;

                var newPos = _stationContainer.anchoredPosition;
                newPos += new Vector2((_stationWidth), 0);
                StartCoroutine(SmoothTransition(_stationContainer.anchoredPosition, newPos, _timeToArrive));

            }
        }
    }


    private void SetDeafultStation()
    {
        _stationIndex = 0;
    }
}
