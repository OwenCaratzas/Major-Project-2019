using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFootstep : MonoBehaviour
{
    public AudioClip guardFootstep;
    public AudioSource guard;

    void PlayStep()
    {
        guard.clip = guardFootstep;
        guard.Play();
    }
}
