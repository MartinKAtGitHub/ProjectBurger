using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSender : MonoBehaviour {

    public Vector2 HorizontalPoint;


    public bool StartSpawn = false;

    public GameObject prefab;
    public List<Transform> spawnedObjects = new List<Transform>();
    public List<Vector3> spawnedObjectsVector = new List<Vector3>();

    public float time = 5;
    public float currenttime = 0;
    public float spawnAmount;
    public float speed = 10;
    public float MinDistance = 0.05f;
    public float Pow = 5;

    public Vector2 playerHorizonVector = Vector2.zero;
     Vector2 MoveVector = Vector2.zero;


    private void Start() {
        for (int i = 0; i < spawnedObjects.Count; i++) {
            spawnedObjectsVector.Add(spawnedObjects[i].position);
        }
    }


    public void Starting() {
        currenttime = Time.time;
        StartSpawn = true;



    }

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(LevelSelectManager.Instance.CameraFollow.transform.position.x, transform.position.y, 0);

        playerHorizonVector = ((Vector2)LevelSelectManager.Instance.CameraFollow.transform.position - HorizontalPoint);
        playerHorizonVector.x = playerHorizonVector.x * -1;

        for (int i = 0; i < spawnedObjects.Count; i++) {
            MoveVector.y = spawnedObjectsVector[i].y;
            MoveVector.x = ((Vector2)transform.position + (playerHorizonVector * (MinDistance + (speed * (Mathf.Pow(1 + i, Pow)))))).x;

            Debug.Log(Mathf.Pow(1 + i, Pow));

            spawnedObjects[i].transform.position = MoveVector;

        }





    }
}
