using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatFive : MonoBehaviour
{
    Tutorial_Text displayBeat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
            displayBeat = obj.GetComponent<Tutorial_Text>();
            displayBeat.TutorialBeatFive();
            StartCoroutine(EndTutorial());
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
