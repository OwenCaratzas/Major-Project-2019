using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{

    public GameObject steamOne;
    public GameObject steamTwo;

    public Sentry _detectCheck;

    public AudioSource whistleAudio;
    public AudioClip whistle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_detectCheck.detectedCheck == true)
        {
            steamOne.SetActive(true);
            steamTwo.SetActive(true);

            Debug.Log("PlayWhistle");

            whistleAudio.Play();
        }
        else
        {
            steamOne.SetActive(false);
            steamTwo.SetActive(false);
        }

    }
}
