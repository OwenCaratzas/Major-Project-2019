using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker_Sphere : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;

    public Transform markerA;
    public Transform markerB;

    public float directionChance;

    public float speed;

    public bool positiveMove = false;
    public bool negativeMove = false;

    // Start is called before the first frame update
    void Start()
    {
        A.Set(markerA.position.x, markerA.position.y, markerA.position.z);
        B.Set(markerB.position.x, markerB.position.y, markerB.position.z);

        directionChance = Random.Range(0, 2.1f);

        if (directionChance <=1)
        {
            positiveMove = true;
        }

        if (directionChance >1)
        {
            negativeMove = true;
        }

        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (positiveMove == true)
        {
            transform.position = Vector3.Lerp(A, B, Mathf.PingPong(Time.time * speed, 1.0f));
        }

        if (negativeMove == true)
        {
            transform.position = Vector3.Lerp(B, A, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }
}
