using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    [SerializeField]
    public static bool skipTutorial;

    // Start is called before the first frame update
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            skipTutorial = false;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("Tutorial") == 0)
            {
                skipTutorial = true;
            }
            else
            {
                skipTutorial = false;
            }
        }
    }

    public void Update()
    {
        if (Escape.firstTimeFinish == true)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            skipTutorial = true;
        }
    }

    public void changeTutState()
    {
        if (skipTutorial == true)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            skipTutorial = false;
        }

        else if (skipTutorial == false)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            skipTutorial = true;
        }
    }
}

