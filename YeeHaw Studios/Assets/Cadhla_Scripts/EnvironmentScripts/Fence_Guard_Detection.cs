using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_Guard_Detection : MonoBehaviour
{

    public GameObject electricFence;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FenceDetection")
        {
            Debug.Log("Guard");
            electricFence.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FenceDetection")
        {
            Debug.Log("Guard");
            electricFence.SetActive(true);
        }
    }


}
