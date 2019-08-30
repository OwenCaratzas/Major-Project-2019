using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text : MonoBehaviour
{
    public Text text;
    public Image textBox;

    private float WaitTime = 8;

    private Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        textBox = GameObject.FindGameObjectWithTag("TextBox").GetComponent<Image>();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0f);
        WaitTime = 8f;

    }

    public void TutorialBeatOne()
    {
        text.text = "W,A,S,D to move\n\nMouse to look around".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }

    public void TutorialBeatTwo()
    {
        text.text = "Stay out of the Spotlight\n\nBe mindful of how much Sound you make".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());

    }
    public void TutorialBeatThree()
    {
        text.text = "Pressure Plates make lots of sound so move slowly\n\nCTRL to crouch".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }
    public void TutorialBeatFour()
    {
        text.text = "Electric Fences will stun you\n\nWait for a Guard to approach or find the Power Lever to deactivate\n\nLeft - Click to interact with highlighted objects".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }
    public void TutorialBeatFive()
    {
        text.text = "Cameras lack noise sensors but will alert Guards\n\nSHIFT to sprint".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }
    public void TutorialBeatSix()
    {
        text.text = "Find the two valves in the carriage to open Security Doors\n\nBe quick, the valves lose pressure and reset after a short time".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }
    public void TutorialBeatSeven()
    {
        text.text = "Reactivate the Control Panel to deploy the retracted walkway\n\nActivate the Breakers when the Power Surge passes through".ToString();
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(EndBeat());
    }
    IEnumerator EndBeat()
    {
        yield return new WaitForSeconds(WaitTime);
        text.text = "";
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0f);
    }
}
