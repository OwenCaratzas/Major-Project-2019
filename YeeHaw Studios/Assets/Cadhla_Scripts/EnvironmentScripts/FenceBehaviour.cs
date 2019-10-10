using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehaviour : MonoBehaviour
{
    public GameObject[] Fences;
    public GameObject Lever;


    private bool fenceActive = true;

    public AudioClip leverClip;
    public AudioSource leverAudio;


    void Update()
    {
        if (fenceActive == false)
        {
            for (int i = 0; i < Fences.Length; i++ )
            {
                Fences[i].SetActive(false);
            }
        }
    }

    void PullTheLever()
    {
        Debug.Log("Pull The Lever");
        Lever.transform.Rotate(0, 0, -70.0f, Space.Self);

        leverAudio.Play();
        leverAudio.clip = leverClip;

        fenceActive = false;
    }
}
