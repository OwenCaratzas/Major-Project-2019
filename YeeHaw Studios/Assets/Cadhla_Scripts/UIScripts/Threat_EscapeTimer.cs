using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Threat_EscapeTimer : MonoBehaviour
{
    public static float escapeTimer = 60;
    private bool timerThreatActive = false;

    Text text;

    public EndLevel failState;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerThreatActive == true)
        {
            text.text = "Lockdown Initiating... Time to Escape " + Mathf.Round(escapeTimer) + " secs".ToString();
            escapeTimer -= Time.deltaTime;
        }

        if (escapeTimer <= 0)
        {
            failState.LoadMenuScene();
        }
    }

    public void callCountDown()
    {
        timerThreatActive = true;
    }

}
