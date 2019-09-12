using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Panel : MonoBehaviour
{
    public int numSpheres = 3;
    public GameObject[] sphereArr;
    public GameObject Sphere;

    public Breaker_Sphere[] breakerCheckArr;

    [SerializeField]
    public int numberOfTrueBooleans = 0;
    public int maxTrueBooleansNeeded;

    public float speed;
    float speedTime = 0.5f;

    public GameObject fenceMesh;
    public Transform startMarker;
    public Transform endMarker;


    // Start is called before the first frame update
    void Start()
    {
        breakerCheckArr = new Breaker_Sphere[sphereArr.Length];

        for (int i = 0; i < sphereArr.Length; i++)
        {
            breakerCheckArr[i] = sphereArr[i].GetComponentInChildren<Breaker_Sphere>();
        }

        maxTrueBooleansNeeded = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfTrueBooleans == maxTrueBooleansNeeded)
        {
            // Set our position as a fraction of the distance between the markers.
            speed = (speed) + speedTime * Time.deltaTime;
            fenceMesh.transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, speed);

        }
    }




public void AddToBreakerBool()
    {
        numberOfTrueBooleans++;
    }
}
