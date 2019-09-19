using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject safe;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (safe.GetComponent<Safe_Script>().objectiveCompleted)
            {
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }
}
