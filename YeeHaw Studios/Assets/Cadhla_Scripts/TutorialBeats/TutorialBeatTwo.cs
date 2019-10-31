using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialBeatTwo : MonoBehaviour
{
    Tutorial_Text displayBeat;

    public GameObject GuardOne;
    public Player player;

    private bool tutTrigger = false;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tutTrigger == false)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
                displayBeat = obj.GetComponent<Tutorial_Text>();
                //displayBeat.TutorialBeatTwo();

                tutTrigger = true;

                StartCoroutine(EndTutorial());

            }
        }
    }


    IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(6);

        Time.timeScale = 1f;

        //GuardOne.GetComponent<NavMeshAgent>().acceleration = 4.0f;
        //GuardOne.GetComponent<Sentry>().DetectionAmount = 0;
        GuardOne.GetComponent<Sentry>().beatTwoOver = true;

        Destroy(gameObject);
    }
}
