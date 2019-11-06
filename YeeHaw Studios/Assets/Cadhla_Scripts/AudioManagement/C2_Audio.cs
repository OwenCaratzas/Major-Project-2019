using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2_Audio : MonoBehaviour
{
    public List<AudioSource> _C1Sentry;
    public List<AudioSource> _C2Sentry;

    public List<AudioSource> _C1Fence;
    public List<AudioSource> _C2Fence;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < _C1Sentry.Count; i++)
            {
                _C1Sentry[i].volume = 0.1f;
            }

            for (int i = 0; i < _C1Fence.Count; i++)
            {
                _C1Fence[i].volume = 0.1f;
            }


            for (int i = 0; i < _C2Sentry.Count; i++)
            {
                _C2Sentry[i].volume = 0.5f;
            }

            for (int i = 0; i < _C2Fence.Count; i++)
            {
                _C2Fence[i].volume = 0.4f;
            }
        }
    }
}
