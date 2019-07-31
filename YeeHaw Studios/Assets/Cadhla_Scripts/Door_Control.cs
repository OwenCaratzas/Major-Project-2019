using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Control : MonoBehaviour
{

    public float doorTimer;

    private bool buttonOn = false;

    public Transform startMarker;
    public Transform endMarker;

    public float speed = 1.0f;

    public int numButtons = 2;
    public GameObject[] buttonArr;
    public GameObject button;

    public Transform[] buttonPositions;
    public GameObject[] buttonPositionTransform;

    private Button_Check[] buttonDownCheck;
    public bool[] buttonCheck;

    public int numberOfTrueBooleans = 0;
    public int numberOfTrueBooleansNeeded;


    // Start is called before the first frame update
    void Start()
    {
        buttonPositionTransform = GameObject.FindGameObjectsWithTag("ButtonPosition");
        buttonPositions = new Transform[numButtons];

        buttonArr = new GameObject[numButtons];

        for (int i = 0; i < numButtons; i++)
        {
            buttonPositions[i] = buttonPositionTransform[i].transform;
            GameObject go = Instantiate(button, buttonPositions[i].transform.position, buttonPositions[i].transform.rotation) as GameObject;
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            buttonArr[i] = go;
        }

        numberOfTrueBooleansNeeded = numButtons;

        doorTimer = 20.0f;

        buttonArr = GameObject.FindGameObjectsWithTag("Button");

        //find individual scripts
        buttonDownCheck = new Button_Check[buttonArr.Length];

        for (int i = 0; i < buttonArr.Length; i++)
        {
            buttonDownCheck[i] = buttonArr[i].GetComponent<Button_Check>();
        }

        //set up bool array
        buttonCheck = new bool[buttonDownCheck.Length];
    }

    // Update is called once per frame
    void Update()
    {

        //check when to start timer
        if (numberOfTrueBooleans == 1)
        {
            doorTimer -= (1.0f * Time.deltaTime);
        }

        //check if timer has ended
        if (doorTimer <= 0)
        {
            doorTimer = 20;
            numberOfTrueBooleans = 0;
            for (int i = 0; i < buttonDownCheck.Length; i++)
            {
                buttonCheck[i] = buttonDownCheck[i].buttonDown = false;

            }
        }

        //check when to move door
        if (numberOfTrueBooleans == numberOfTrueBooleansNeeded)
        {

           // Set our position as a fraction of the distance between the markers.
           transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, speed);
        }

    }

        void AddToButtonBool()
        {
            for (int i = 0; i < buttonDownCheck.Length; i++)
            {
                buttonCheck[i] = buttonDownCheck[i].buttonDown;
                if (buttonDownCheck[i].buttonDown == true)
                {
                    numberOfTrueBooleans++;
                }
        }
    }
}
