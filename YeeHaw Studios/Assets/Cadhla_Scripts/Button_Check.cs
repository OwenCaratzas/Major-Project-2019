using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Check : MonoBehaviour
{
    public bool buttonDown = false;

    public Transform startMarker;
    public Transform endMarker;

    public float speed = 0.005f;

    private float startTime;
    public float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown == true)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

        }

        if (buttonDown == false)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);

        }


    }

    void ClickedOn()
    {
        buttonDown = true;
    }
}
