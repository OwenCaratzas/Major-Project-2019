using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialBeatTwo : MonoBehaviour
{
    Tutorial_Text displayBeat;

    public GameObject GuardOne;

    public float timer = 8f;
    private bool startTimer = false;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (startTimer == true)
        {
            timer -= 1 * Time.deltaTime;
        }

        if (timer <= 0)
        {
            GuardOne.GetComponent<NavMeshAgent>().acceleration = 8.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            startTimer = true;
            GameObject obj = GameObject.FindGameObjectWithTag("TutorialDisplay");
            displayBeat = obj.GetComponent<Tutorial_Text>();
            displayBeat.TutorialBeatTwo();
            StartCoroutine(EndTutorial());
        }
    }


    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(9);
        Destroy(gameObject);
    }
}
