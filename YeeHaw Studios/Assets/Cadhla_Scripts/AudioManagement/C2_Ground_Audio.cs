using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2_Ground_Audio : MonoBehaviour
{
    public List<AudioSource> _C2_GroundSentry;
    public List<AudioSource> _C2_UpstairsSentry;

    public List<AudioSource> _C2_UpstairsFence;


    //transition to ground
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < _C2_GroundSentry.Count; i++)
            {
                _C2_GroundSentry[i].volume = 0.5f;
            }


            for (int i = 0; i < _C2_UpstairsSentry.Count; i++)
            {
                _C2_UpstairsSentry[i].volume = 0.1f;
            }

            for (int i = 0; i < _C2_UpstairsFence.Count; i++)
            {
                _C2_UpstairsFence[i].volume = 0.1f;
            }
        }
    }
}
