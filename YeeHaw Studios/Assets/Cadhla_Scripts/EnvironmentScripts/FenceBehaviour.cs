using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehaviour : MonoBehaviour
{
    public GameObject[] Fences;
    public GameObject Lever;

    public bool leverPulled = false;

    public AudioClip leverClip;
    public AudioSource leverAudio;

    private bool firstContact = false;


    void Update()
    {
        if (leverPulled == true)
        {
            for (int i = 0; i < Fences.Length; i++ )
            {
                Fences[i].SetActive(false);
            }
        }
    }

    public void PullTheLever()
    {
        Debug.Log("Pull The Lever");

        if (leverPulled == false)
        {
            Lever.transform.Rotate(0, 0, -70.0f, Space.Self);

            leverAudio.Play();
            leverAudio.clip = leverClip;

            leverPulled = true;

            if (firstContact == false)
            {
                firstContact = true;
            }

        }
        
    }
}
