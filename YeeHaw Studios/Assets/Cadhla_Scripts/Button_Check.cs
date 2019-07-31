using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Check : MonoBehaviour
{
    public bool buttonDown = false;

    public Transform startMarker;
    public Transform endMarker;
    public Transform buttonLocation;

    public float speed = 0.1f;

    private float startTime;
    public float journeyLength;

    public Door_Control buttonBool;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickedOn();
        }

        if (buttonDown == true)
        {
            //// Distance moved = time * speed.
            //float distCovered = (Time.time - startTime) * speed;

            ////Fraction of journey completed = current distance divided by total distance.
            //float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.MoveTowards(buttonLocation.position, endMarker.position, speed);

        }

        if (buttonDown == false)
        {
            //// Distance moved = time * speed.
            //float distCovered = (Time.time - startTime) * speed;

            //// Fraction of journey completed = current distance divided by total distance.
            //float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.MoveTowards(buttonLocation.position, startMarker.position, speed);

        }


    }

    public void ClickedOn()
    {
        buttonDown = true;
        buttonBool.AddToButtonBool();
        Debug.Log("IT WORKED");
    }
}
