using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.tag = "HiddenPlayer";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HiddenPlayer")
        {
            other.gameObject.tag = "Player";
        }
    }
}
