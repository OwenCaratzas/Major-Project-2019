using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_Guard_Detection : MonoBehaviour
{

    public GameObject electricFence;

    public FenceBehaviour leverCheck;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FenceDetection" && leverCheck.leverPulled == false)
        {
            electricFence.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FenceDetection" && leverCheck.leverPulled == false)
        {
            electricFence.SetActive(true);
        }
    }


}
