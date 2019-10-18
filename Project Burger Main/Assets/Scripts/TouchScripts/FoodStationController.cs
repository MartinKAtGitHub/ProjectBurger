using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches the Food station on button click
/// </summary>
public class FoodStationController : MonoBehaviour
{

    [Tooltip("The RectTransform which will be moved on Left Right btn press")]
    private RectTransform _stationContainer;

    void Awake()
    {
        _stationContainer = GetComponent<RectTransform>();

        if (_stationContainer == null)
        {
            Debug.LogError($"Cant find RectTransform make sure {name} is the swipe object");
        }
    }

    IEnumerator SmoothTransition(Vector2 startPos, Vector2 endPos, float sec)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            _stationContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        yield return null;
    }


    public void NextStation()
    {
        var newPos = _stationContainer.anchoredPosition;
        newPos += new Vector2(-1 * (1920), 0);

        StartCoroutine(SmoothTransition(_stationContainer.anchoredPosition, newPos, 5f));
    }

    public void PreviousStation()
    {
        var newPos = _stationContainer.anchoredPosition;
        newPos += new Vector2( (1920), 0);

        StartCoroutine(SmoothTransition(_stationContainer.anchoredPosition, newPos, 5f));
    }
}
