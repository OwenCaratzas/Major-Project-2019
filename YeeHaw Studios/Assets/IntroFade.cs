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

    public Text BeatOne;

    public TypeWriterEffect stringRef;


    // Start is called before the first frame update
    void Start()
    {
        blackIn = 1;
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

        TutorialOne.SetActive(true);
        gameUI.SetActive(true);

        BeatOne.color = new Color(BeatOne.color.r, BeatOne.color.g, BeatOne.color.b, 0f);
    }
}
