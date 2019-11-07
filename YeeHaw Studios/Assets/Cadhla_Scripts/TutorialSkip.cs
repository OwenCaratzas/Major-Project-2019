using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    public static bool skipTutorial;

    // Start is called before the first frame update
    void Start()
    {
        if (Escape.firstTimeFinish == true)
        {
            skipTutorial = true;
        }

        else skipTutorial = false;
    }

    public void Update()
    {
        Debug.Log(skipTutorial.ToString());
    }

    public void changeTutState()
    {
        if (skipTutorial == true)
        {
            skipTutorial = false;
        }

        else if (skipTutorial == false)
        {
            skipTutorial = true;
        }
    }
}
