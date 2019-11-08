using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Menu_Toggle : MonoBehaviour
{
    public Text toggleButton;
    private bool tutActive;

    public void Awake()
    {
        if(!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            tutActive = true;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt ("Tutorial")  == 0)
            {
                tutActive = false;
            }
            else
            {
                tutActive = true;
            }
        }


    }

    public void Update()
    {
        if (TutorialSkip.skipTutorial == true)
        {
            tutActive = false;
        }

        else if(TutorialSkip.skipTutorial == false)
        {
            tutActive = true;
        }

        if (tutActive == true)
        {
            toggleButton.text = "On".ToString();
        }

        if (tutActive == false)
        {
            toggleButton.text = "Off".ToString();
        }
    }
}
