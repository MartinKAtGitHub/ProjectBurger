using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{

    public Action OnSale;

    private void Awake()
    {
        LevelManager.Instance.SalesManager = this;
    }

    /// <summary>
    /// Triggers the onSale event;
    /// </summary>
    public void OnSell()
    {
        // Call this on a button
        // event.Fire();

        OnSale?.Invoke();
    }
}
