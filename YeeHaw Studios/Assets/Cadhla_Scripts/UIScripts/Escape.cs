using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject safe;
    public Player mouseControl;

    public static bool objectiveComplete = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (safe.GetComponent<Safe_Script>().objectiveCompleted)
            {
                objectiveComplete = true;
                mouseControl.TurnOnMouse();
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
