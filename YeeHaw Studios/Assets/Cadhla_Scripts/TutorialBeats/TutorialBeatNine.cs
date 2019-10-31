using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatNine : MonoBehaviour
{
    Tutorial_Text displayBeat;
    public Player player;

    private bool tutTrigger = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tutTrigger == false)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
                displayBeat = obj.GetComponent<Tutorial_Text>();
                displayBeat.TutorialBeatNine();

                tutTrigger = true;

                StartCoroutine(EndTutorial());
            }
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }
}
