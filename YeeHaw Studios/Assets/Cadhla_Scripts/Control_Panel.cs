using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Panel : MonoBehaviour
{
    public int numSpheres = 3;
    public GameObject[] sphereArr;
    public GameObject Sphere;

    public Transform[] breakerPositions;
    public GameObject[] Breakers;

    // Start is called before the first frame update
    void Start()
    {

        Breakers = GameObject.FindGameObjectsWithTag("Breaker");
        breakerPositions = new Transform[numSpheres];

        sphereArr = new GameObject[numSpheres];

        for (int i = 0; i < numSpheres; i++)
        {
            breakerPositions[i] = Breakers[i].transform;
            GameObject go = Instantiate(Sphere, breakerPositions[i].transform.position, breakerPositions[i].transform.rotation) as GameObject;
            sphereArr[i] = go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
