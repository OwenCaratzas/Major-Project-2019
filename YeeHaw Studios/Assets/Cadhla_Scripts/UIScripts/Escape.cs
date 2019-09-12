using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject safe;

    public void EscapeNow()
    {
        if (gameObject.tag == "Escape")
        {
            if (safe.GetComponent<Safe_Script>().objectiveCompleted)
            {
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }
}
