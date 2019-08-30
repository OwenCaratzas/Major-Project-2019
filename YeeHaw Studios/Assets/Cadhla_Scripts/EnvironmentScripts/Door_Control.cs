using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Control : MonoBehaviour
{

    public float doorTimer;

    private bool buttonOn = false;

    public GameObject doorMesh;
    public Transform startMarker;
    public Transform endMarker;

    public float speed;
    float speedTime = 0.5f;

    public int numButtons = 2;
    public GameObject[] buttonArr;
    public GameObject button;


    [SerializeField]
    private Button_Check[] buttonDownCheck;

    [SerializeField]
    public int numberOfTrueBooleans = 0;
    public int maxTrueBooleansNeeded;



    // Start is called before the first frame update
    void Start()
    {
        maxTrueBooleansNeeded = numButtons;

        doorTimer = 20.0f;

        //find individual scripts
        buttonDownCheck = new Button_Check[buttonArr.Length];

        for (int i = 0; i < buttonArr.Length; i++)
        {
            buttonDownCheck[i] = buttonArr[i].GetComponentInChildren<Button_Check>();
        }

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
                buttonDownCheck[i].buttonDown = false;

            }
        }

        //check when to move door
        if (numberOfTrueBooleans == maxTrueBooleansNeeded)
        {
            // Set our position as a fraction of the distance between the markers.
           speed = (speed) + speedTime * Time.deltaTime;
           doorMesh.transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, speed);
        }

    }

        public void AddToButtonBool()
        {
            numberOfTrueBooleans++;
        }
}
