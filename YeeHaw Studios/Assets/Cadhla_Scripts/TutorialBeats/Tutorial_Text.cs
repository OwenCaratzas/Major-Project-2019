using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text : MonoBehaviour
{
    public Image playDisplay;

    public Sprite BeatOne;

    public Sprite BeatThree;
    public Pressure_Plate plateOne;

    public Sprite BeatFour;
    public FenceBehaviour leverOne;

    public Sprite BeatFive;

    public Sprite BeatSix;
    public Door_Control doorOne;

    public Sprite BeatNine;

    private bool T_2_B;


    public GameObject GameUI;

    public float WaitTime;
    private float FadeInTime;
    private float FadeOutTime;

    private Coroutine currentCoroutine = null;

    private bool FadeInPlay;
    private bool FadeOutPlay;

    public bool startTimer;
    public bool tutTrigger = false;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        playDisplay.color = new Color(playDisplay.color.r, playDisplay.color.g, playDisplay.color.b, 0f);

        FadeInPlay = false;
        FadeOutPlay = false;

        startTimer = false;
        WaitTime = 6f;

    }

    public void FixedUpdate()
    {
       
        if (FadeInPlay == true)
        {
            if (FadeInTime <= 1)
            {
                FadeInTime = FadeInTime + (1f * Time.unscaledDeltaTime);
            }

           playDisplay.color = new Color(playDisplay.color.r, playDisplay.color.g, playDisplay.color.b, FadeInTime);
        }

        
        if (FadeOutPlay == true)
        {
            if (FadeOutTime >= 0)
            {
                FadeOutTime = FadeOutTime - (1f * Time.unscaledDeltaTime);
            }
            playDisplay.color = new Color(playDisplay.color.r, playDisplay.color.g, playDisplay.color.b, FadeOutTime);
        }


        if (startTimer == true)
        {
            WaitTime -= 1 * Time.unscaledDeltaTime;
            if (WaitTime <= 0)
            {
                EndBeat();
            }
        }

        if (plateOne.firstContact == true)
        {
            if (tutTrigger == false)
            {
                WaitTime = 3;
                startTimer = true;
                tutTrigger = true;
                plateOne.firstContact = false;
            }
        }

        if (leverOne.leverPulled == true)
        {
            if (tutTrigger == false)
            {
                WaitTime = 3;
                startTimer = true;
                tutTrigger = true;
            }
        }

        if (doorOne.firstContact == true)
        {
            if (tutTrigger == false)
            {
                WaitTime = 3;
                startTimer = true;
                tutTrigger = true;
               doorOne.firstContact = false;
            }
        }

    }

    public void TutorialBeatOne()
    {
        WaitTime = 6f;
        FadeInTime = 0f;

        playDisplay.sprite = BeatOne;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;
    }

    public void TutorialBeatThree()
    {
        startTimer = false;

        playDisplay.sprite = BeatThree;

        WaitTime = 6f;
        FadeInTime = 0f;

        FadeInPlay = true;
        FadeOutPlay = false;
    }
    public void TutorialBeatFour()
    {
        startTimer = false;
        tutTrigger = false;

        playDisplay.sprite = BeatFour;

        WaitTime = 6f;
        FadeInTime = 0f;

        FadeInPlay = true;
        FadeOutPlay = false;
    }
    public void TutorialBeatFive()
    {
        WaitTime = 6f;
        FadeInTime = 0f;

        playDisplay.sprite = BeatFive;

        startTimer = true;

        FadeInPlay = true;
        FadeOutPlay = false;
    }
    public void TutorialBeatSix()
    {
        startTimer = false;
        tutTrigger = false;

        WaitTime = 6f;
        FadeInTime = 0f;

        playDisplay.sprite = BeatSix;

        FadeInPlay = true;
        FadeOutPlay = false;
    }

    public void TutorialBeatNine()
    {
        WaitTime = 8f;
        FadeInTime = 0f;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;

        playDisplay.sprite = BeatNine;
    }

    public void EndBeat()
    {
        player.TurnOffMouse();
        player.walkSpeed = 0.05f;

        FadeOutTime = FadeInTime;

        GameUI.SetActive(true);

        if (FadeInPlay == true)
        {
            FadeInPlay = false;
            FadeOutPlay = true;
        }

        startTimer = false;
        WaitTime = 6;

        Time.timeScale = 1f;
    }
}
