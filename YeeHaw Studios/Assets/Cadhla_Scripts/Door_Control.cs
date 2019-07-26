using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Control : MonoBehaviour
{

    public float doorTimer;

    private bool buttonOn = false;

    public Transform startMarker;
    public Transform endMarker;

    public float speed = 0.005f;

    private float startTime;
    public float journeyLength;

    public int numButtons = 2;
    public GameObject[] buttonArr;
    public GameObject button;
    public Button_Check[] buttonDownCheck;
    public bool[] buttonCheck;

    public int numberOfTrueBooleans = 0;
    public int numberOfTrueBooleansNeeded;


    // Start is called before the first frame update
    void Start()
    {
        buttonArr = new GameObject[numButtons];
        for (int i = 0; i < numButtons; i++)
        {
            GameObject go = Instantiate(button, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
            go.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            buttonArr[i] = go;
        }

        numberOfTrueBooleansNeeded = numButtons;

        doorTimer = 20.0f;

        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        buttonArr = GameObject.FindGameObjectsWithTag("Button");

        buttonDownCheck = new Button_Check[buttonArr.Length];

        for (int i = 0; i < buttonArr.Length; i++)
        {
            buttonDownCheck[i] = buttonArr[i].GetComponent<Button_Check>();
        }

        buttonCheck = new bool[buttonDownCheck.Length];
        
        for (int i = 0; i<buttonDownCheck.Length; i++)
        {
            buttonCheck[i] = buttonDownCheck[i].buttonDown;
        }


        if (numberOfTrueBooleans == 1)
        {
            doorTimer -= (1.0f*Time.deltaTime);
        }

        if (doorTimer == 0)
        {
            doorTimer = 20;

        }

        if (numberOfTrueBooleans == numberOfTrueBooleansNeeded)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        }
    }
}
