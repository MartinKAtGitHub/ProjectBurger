using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollower : MonoBehaviour {

    public Vector3 startpoint;
    public bool startmoving = false;
    public float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(startmoving == true) {

        transform.position += Vector3.left * Time.deltaTime * Speed;
        }

    }

    public void MoveAfteRteleport() {
        startmoving = true;
        transform.parent = LevelSelectManager.Instance.Player.transform;
    }

    public void SetMovingObject(Transform theObject) {
        transform.SetParent(theObject);
        startmoving = true;
    }

    public void RemoveMovingObject() {
        transform.SetParent(transform.parent.parent);
        startmoving = false;
    }

}
