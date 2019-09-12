using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatNine : MonoBehaviour
{
    Tutorial_Text displayBeat;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collide");
            GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
            displayBeat = obj.GetComponent<Tutorial_Text>();
            displayBeat.TutorialBeatNine();
            StartCoroutine(EndTutorial());
        }
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
