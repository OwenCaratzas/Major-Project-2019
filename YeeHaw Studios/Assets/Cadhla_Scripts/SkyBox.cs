using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    public GameObject sky;

    public Transform startMarker;
    public Transform endMarker;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sky.transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, Time.deltaTime/2);
    }
}
