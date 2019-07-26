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

    private Button_Check[] buttonDownCheck;
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
    }

    // Update is called once per frame
    void Update()
    {

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

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, speed);
        }

        buttonArr = GameObject.FindGameObjectsWithTag("Button");

        buttonDownCheck = new Button_Check[buttonArr.Length];

        for (int i = 0; i < buttonArr.Length; i++)
        {
            buttonDownCheck[i] = buttonArr[i].GetComponent<Button_Check>();
        }

        buttonCheck = new bool[buttonDownCheck.Length];

        for (int i = 0; i < buttonDownCheck.Length; i++)
        {
            buttonCheck[i] = buttonDownCheck[i].buttonDown;
            if (buttonDownCheck[i].buttonDown == true)
            {
                //numberOfTrueBooleans++;
            }
        }
    }

    void AddToButtonBool()
    {


    }
}
