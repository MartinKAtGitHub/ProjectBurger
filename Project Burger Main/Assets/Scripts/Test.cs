
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    public  Vector2 offset; // How much away you want it to be from the hole
    Vector2 newRespawnPosition; // the New position away from the hole
    Vector2 playerPosition; // The saved position you get when fall.
    GameObject player; // character object
    public void Resapwn()
    {
        newRespawnPosition = playerPosition + offset;
        Instantiate(player, newRespawnPosition, Quaternion.identity); // I dont know how you spawn your player but use the new pos
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT" + name);

    }
}
