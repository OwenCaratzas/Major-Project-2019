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

    void EndIntro()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);

        TutorialOne.SetActive(true);
        gameUI.SetActive(true);
    }
}
