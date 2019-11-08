using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFade : MonoBehaviour
{
    public Image blackout;
    private float blackOut;
    private float blackIn;

    private bool blackOutGo = false;
    private bool blackInGo = true;

    public GameObject cam1;
    public GameObject cam2;

    public GameObject gameUI;
    public GameObject TutorialOne;

    public GameObject _audioManage1;

    public GameObject skipPrompt;

    public Text BeatOne;

    public TypeWriterEffect stringRef;

    private float skipTimer;
    private float skipFinish = 2f;

    public Transform slider;

    public static bool introPlaying;


    // Start is called before the first frame update
    void Start()
    {
        blackIn = 1;
        introPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (blackOutGo == true)
        {
            blackOut = blackOut + (2.0f * Time.deltaTime);
            blackout.color = new Color(blackout.color.r, blackout.color.g, blackout.color.b, blackOut);
        }

        if (blackInGo == true)
        {
            blackIn = blackIn - (2.0f * Time.deltaTime);
            blackout.color = new Color(blackout.color.r, blackout.color.g, blackout.color.b, blackIn);
        }

        if (Input.GetKey(KeyCode.E))
        {
            skipTimer += Time.deltaTime;
            slider.localScale = new Vector3((skipTimer/2), 1f);
        }

        else if (Input.GetKeyUp(KeyCode.E))
        {
            skipTimer = 0;
            slider.localScale = new Vector3(skipTimer, 1f);
        }

        if (skipTimer >= skipFinish)
        {
            EndIntro();
        }

    }


    void FadeOut()
    {
        blackOut = 0f;
        blackOutGo = true;
        blackInGo = false;
    }

    void FadeIn()
    {
        blackIn = blackOut;
        blackInGo = true;
        blackOutGo = false;
    }

    void FirstBeat()
    {
        BeatOne.color = new Color(BeatOne.color.r, BeatOne.color.g, BeatOne.color.b, 255f);
    }

    void SecondBeat()
    {
        stringRef.currentText = "".ToString();
        stringRef.fullText = "Avoid the Guards and Camera's ".ToString();
        stringRef.SendMessage("ShowText");
    }

    void ThirdBeat()
    {
        stringRef.currentText = "".ToString();
        stringRef.fullText = "Reach the Escape and Don't get caught ".ToString();
        stringRef.SendMessage("ShowText");
    }

    void EndIntro()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);

        skipPrompt.SetActive(false);
        slider.localScale = new Vector3(0f, 0f);

        blackout.color = new Color(blackout.color.r, blackout.color.g, blackout.color.b, 0f);

        TutorialOne.SetActive(true);
        gameUI.SetActive(true);

        _audioManage1.SetActive(true);

        if (Escape.objectiveComplete == true)
        {
            Escape.objectiveComplete = false;
        }

        introPlaying = false;
        FadeIn();
        BeatOne.color = new Color(BeatOne.color.r, BeatOne.color.g, BeatOne.color.b, 0f);
    }
}
