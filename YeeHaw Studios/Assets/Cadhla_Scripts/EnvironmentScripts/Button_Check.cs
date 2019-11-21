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

    public AudioClip buttonClip;
    public AudioSource buttonAudio;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ClickedOn();
        //}

        if (buttonDown == true)
        {
            transform.position = Vector3.MoveTowards(buttonLocation.position, endMarker.position, speed);

        }

        if (buttonDown == false)
        {
            transform.position = Vector3.MoveTowards(buttonLocation.position, startMarker.position, speed);
        }
    }

    public void ClickedOn()
    {
        buttonAudio.Play();
        buttonAudio.clip = buttonClip;

        buttonDown = true;
        buttonBool.AddToButtonBool();
    }
}
