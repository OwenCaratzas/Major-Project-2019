using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text : MonoBehaviour
{
    //public Image stillDisplay;
    public Image playDisplay;

    public Sprite BeatOne;
    public Sprite BeatTwo;
    public Sprite BeatThree;
    public Sprite BeatFour;
    public Sprite BeatFive;
    public Sprite BeatSix;
    public Sprite BeatSeven;
    public Sprite BeatEight;
    public Sprite BeatNine;

    private bool T_2_B;


    public GameObject GameUI;

    private float WaitTime;
    private float FadeInTime;
    private float FadeOutTime;

    //private float FadeInStillTime;
    //private float FadeOutStillTime;


    private Coroutine currentCoroutine = null;

    //private bool FadeInStill;
    //private bool FadeOutStill;

    private bool FadeInPlay;
    private bool FadeOutPlay;

    private bool startTimer;

    public Player player;


    //public GameObject prompt;

    //private GameObject promptBeatOne;


    // Start is called before the first frame update
    void Start()
    {
        //stillDisplay.color = new Color(stillDisplay.color.r, stillDisplay.color.g, stillDisplay.color.b, 0f);
        playDisplay.color = new Color(playDisplay.color.r, playDisplay.color.g, playDisplay.color.b, 0f);

        FadeInPlay = false;
        FadeOutPlay = false;

        //FadeInStill = false;
        //FadeOutStill = false;

        startTimer = false;
        WaitTime = 4f;

    }

    public void Update()
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


        //if (FadeInStill == true)
        //{
        //    if (FadeInStillTime <= 1)
        //    {
        //        FadeInStillTime = FadeInStillTime + (1f * Time.unscaledDeltaTime);
        //    }

        //    stillDisplay.color = new Color(stillDisplay.color.r, stillDisplay.color.g, stillDisplay.color.b, FadeInStillTime);
        //}


        //if (FadeOutStill == true)
        //{
        //    if (FadeOutStillTime >= 0)
        //    {
        //        FadeOutStillTime = FadeOutStillTime - (1f * Time.unscaledDeltaTime);
        //    }
        //    stillDisplay.color = new Color(stillDisplay.color.r, stillDisplay.color.g, stillDisplay.color.b, FadeOutStillTime);
        //}

        if (startTimer == true)
        {
            WaitTime -= 1 * Time.unscaledDeltaTime;
            if (WaitTime <= 0)
            {
                EndBeat();
            }
        }

    }

    public void TutorialBeatOne()
    {
        FadeInTime = 0f;

        playDisplay.sprite = BeatOne;
        //stillDisplay.sprite = null;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;
    }

    public void TutorialBeatTwo()
    {
        //FadeInStillTime = 0f;

        //FadeInStill = true;
        //FadeOutStill = false;

        FadeInTime = 0f;
        FadeInPlay = true;
        FadeOutPlay = false;

        //GameUI.SetActive(false);

        //stillDisplay.sprite = BeatTwo;
        playDisplay.sprite = BeatTwo;

        startTimer = true;
    }
    public void TutorialBeatThree()
    {
        FadeInTime = 0f;

        playDisplay.sprite = BeatThree;
        //stillDisplay.sprite = null;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;
    }
    public void TutorialBeatFour()
    {
        //FadeInStillTime = 0f;

        //stillDisplay.sprite = BeatFour;
        playDisplay.sprite = BeatFour;

        //FadeInStill = true;
        //FadeOutStill = false;

        FadeInTime = 0f;
        FadeInPlay = true;
        FadeOutPlay = false;

        //GameUI.SetActive(false);

        startTimer = true;
    }
    public void TutorialBeatFive()
    {
        FadeInTime = 0f;

        playDisplay.sprite = BeatFive;
        //stillDisplay.sprite = null;

        startTimer = true;

        FadeInPlay = true;
        FadeOutPlay = false;
    }
    public void TutorialBeatSix()
    {
        FadeInTime = 0f;

        playDisplay.sprite = BeatSix;
        //stillDisplay.sprite = null;

        startTimer = true;

        FadeInPlay = true;
        FadeOutPlay = false;
    }
    public void TutorialBeatSeven()
    {
        FadeInTime = 0f;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;

        playDisplay.sprite = BeatSeven;
        //stillDisplay.sprite = null;
    }

    public void TutorialBeatEight()
    {
        // FadeInStillTime = 0f;

        // FadeInStill = true;
        // FadeOutStill = false;

        FadeInTime = 0f;
        FadeInPlay = true;
        FadeOutPlay = false;

        //GameUI.SetActive(false);

        startTimer = true;

        //stillDisplay.sprite = BeatEight;
        playDisplay.sprite = null;
    }

    public void TutorialBeatNine()
    {
        FadeInTime = 0f;

        FadeInPlay = true;
        FadeOutPlay = false;

        startTimer = true;

        playDisplay.sprite = BeatNine;
       // stillDisplay.sprite = null;
    }

    public void EndBeat()
    {
        player.TurnOffMouse();
        player.walkSpeed = 0.05f;

        FadeOutTime = FadeInTime;
       // FadeOutStillTime = FadeInStillTime;

        GameUI.SetActive(true);

        if (FadeInPlay == true)
        {
            FadeInPlay = false;
            FadeOutPlay = true;
        }

        //if (FadeInStill == true)
        //{
        //    FadeInStill = false;
        //    FadeOutStill = true;
        //}

        startTimer = false;
        WaitTime = 4;

        Time.timeScale = 1f;
    }
}
