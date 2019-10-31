using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatSix : MonoBehaviour
{
    Tutorial_Text displayBeat;

    private bool tutTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tutTrigger == false)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
                displayBeat = obj.GetComponent<Tutorial_Text>();
                displayBeat.TutorialBeatSix();

                tutTrigger = true;

                StartCoroutine(EndTutorial());
            }
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
