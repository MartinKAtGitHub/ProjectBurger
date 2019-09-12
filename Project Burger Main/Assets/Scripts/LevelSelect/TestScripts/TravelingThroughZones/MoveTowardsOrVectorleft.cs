using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsOrVectorleft : MonoBehaviour
{

    public bool Run = false;
    public bool AorB = true;
    public float speed = 10;

    public Transform EndPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Run == true) {
            if(AorB == true) {
                transform.position += Vector3.right * Time.deltaTime * speed;
            } else {
                transform.position = Vector2.MoveTowards(transform.position, EndPoint.position, 1 * Time.deltaTime * speed);
            }
        }
    }
}
