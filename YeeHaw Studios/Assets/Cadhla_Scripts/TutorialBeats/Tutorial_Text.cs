﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text : MonoBehaviour
{
    public Text text;
    public Text textTwo;
    public Text textThree;

    public Image textBox;
    public Image stillDisplay;

    public Image TutorialStillOne;
    public Image TutorialStillTwo;
    public GameObject TutorialStillThree;
    public GameObject TutorialStillFour;
    public GameObject TutorialStillFive;

    private bool T_2_B;


    public Image GameUI;

    public float WaitTime;
    public float FadeInTime;
    public float FadeOutTime;

    private Coroutine currentCoroutine = null;

    private bool FadeIn;
    private bool FadeOut;

    private bool startTimer;

    public Player player;


    //public GameObject prompt;

    //private GameObject promptBeatOne;


    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.FindGameObjectWithTag("TextBox").GetComponent<Image>();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0f);

        FadeIn = false;
        FadeOut = false;

        startTimer = false;
        WaitTime = 5.5f;

        //promptBeatOne = GameObject.FindGameObjectWithTag("PromptOne");

    }

    public void Update()
    {
       
        if (FadeIn == true)
        {
            if (FadeInTime <= 1)
            {
                FadeInTime = FadeInTime + (1f * Time.deltaTime);
            }

            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, FadeInTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, FadeInTime);

            if (T_2_B == true)
            {
                TutorialStillOne.color = new Color(text.color.r, text.color.g, text.color.b, FadeInTime);
                GameUI.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            }
        }

        
        if (FadeOut == true)
        {
            if (FadeOutTime >= 0)
            {
                FadeOutTime = FadeOutTime - (1f * Time.deltaTime);
            }
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, FadeOutTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, FadeOutTime);
        }


        if (startTimer == true)
        {
            WaitTime -= 1 * Time.deltaTime;
            if (WaitTime <= 0)
            {
                EndBeat();
            }
        }

    }

    public void TutorialBeatOne()
    {
        FadeInTime = 0f;

        text.text = "W,A,S,D to move\n\nMouse to look around\n\nCTRL to Crouch".ToString();

        FadeIn = true;
        FadeOut = false;
        startTimer = true;
    }

    public void TutorialBeatTwo()
    {
        FadeInTime = 0f;

        FadeIn = true;
        FadeOut = false;

        textTwo.text = "Stay out of the Guards Spotlights\n\n".ToString();
        textThree.text = "If spotted Guards will chase you down and attempt to capture you.".ToString();
    }
    public void TutorialBeatThree()
    {
        FadeInTime = 0f;

        text.text = "Pressure Plates make lots of sound so crouch to avoid activating".ToString();

        FadeIn = true;
        FadeOut = false;
        startTimer = true;
    }
    public void TutorialBeatFour()
    {
        FadeIn = false;
        FadeOut = true;

        textTwo.text = "Electric Fences will stun you".ToString();
        textThree.text = "Wait for a Guard to approach or find the Power Lever to deactivate\n\nLeft - Click to interactive with the Power Lever".ToString();

        //TutorialStillTwo.SetActive(true);
        //GameUI.SetActive(false);

    }
    public void TutorialBeatFive()
    {
        FadeInTime = 0f;

        text.text = "Cameras lack noise sensors but will alert Guards\n\nSHIFT to Sprint".ToString();

        startTimer = true;

        FadeIn = true;
        FadeOut = false;
    }
    public void TutorialBeatSix()
    {
        FadeInTime = 0f;

        text.text = "Find the two buttons in the carriage to open Security Doors, use Left - Click to activate them\n\nBe quick, the buttons reset after a short time".ToString();

        startTimer = true;

        FadeIn = true;
        FadeOut = false;
    }
    public void TutorialBeatSeven()
    {
        FadeIn = false;
        FadeOut = true;

        textTwo.text = "Trip the breakers on the Control Panel to retract Fences".ToString();
        textThree.text = "Left-Click the Breakers when the Power Surge passes through".ToString();

        TutorialStillFour.SetActive(true);
        //GameUI.SetActive(false);
    }

    public void TutorialBeatEight()
    {
        FadeIn = false;
        FadeOut = true;

        textTwo.text = "Find the safe and steal the goods to complete the heist".ToString();

        TutorialStillThree.SetActive(true);
       // GameUI.SetActive(false);
    }

    public void TutorialBeatNine()
    {
        FadeIn = false;
        FadeOut = true;

        textTwo.text = "The ElectroMagnet can be used to interact with Buttons and Levers at a distance".ToString();
        textThree.text = "Press Q to active and click when aiming at them".ToString();

        TutorialStillFive.SetActive(true);
        //GameUI.SetActive(false);
    }
       
    public void EndBeat()
    {
        
        text.text = "";
        textTwo.text = "";
        textThree.text = "";

        player.TurnOffMouse();
        player.walkSpeed = 0.05f;

        //TutorialStillOne.SetActive(false);
        //TutorialStillTwo.SetActive(false);
        TutorialStillThree.SetActive(false);
        TutorialStillFour.SetActive(false);
        TutorialStillFive.SetActive(false);

        FadeOutTime = FadeInTime;

        FadeIn = false;
        FadeOut = true;

        startTimer = false;
        WaitTime = 8;

        Time.timeScale = 1f;

       // GameUI.SetActive(true);

        
        //textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0f);
    }
}
