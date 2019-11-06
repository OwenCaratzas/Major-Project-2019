using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject success;

    public void FixedUpdate()
    {
        if (Escape.objectiveComplete == true)
        {
            success.SetActive(true);
        }

        if (Escape.objectiveComplete == false)
        {
            success.SetActive(false);
        }
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}

