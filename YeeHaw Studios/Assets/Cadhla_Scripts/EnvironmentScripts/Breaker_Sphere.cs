using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker_Sphere : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;

    public Transform markerA;
    public Transform markerB;

    public float speed;
    float breakerLength = 1.0f;

    public bool Move = true;

    public bool breakerReady;

    // Start is called before the first frame update
    void Start()
    {
        A.Set(markerA.position.x, markerA.position.y, markerA.position.z);
        B.Set(markerB.position.x, markerB.position.y, markerB.position.z);


        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Move == true)
        {
            transform.position = Vector3.Lerp(A, B, Mathf.PingPong(Time.time * speed, breakerLength));
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Breaker")
        {
            Debug.Log("Hit");
            breakerReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Breaker")
        {
            breakerReady = false;
        }
    }

    public void CompleteCircuit()
    {
        if (breakerReady == true)
        {
            Move = false;
            breakerLength = 0f;
        }
    }
}
