using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatFour : MonoBehaviour
{
    Tutorial_Text displayBeat;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
            displayBeat = obj.GetComponent<Tutorial_Text>();

            Time.timeScale = 0.0f;
            player.TurnOffMouse();

            displayBeat.TutorialBeatFour();
            StartCoroutine(EndTutorial());
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(6);

        Time.timeScale = 1f;
        player.TurnOnMouse();
        //displayBeat.EndBeat();

        Destroy(gameObject);
    }
}
