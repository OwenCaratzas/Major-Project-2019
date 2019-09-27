using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{

    public GameObject steamOne;
    public GameObject steamTwo;

    public Animator m_robotAnimController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_robotAnimController.GetBool("Detected") == true && m_robotAnimController.GetBool("Patrol") == false)
        {
            steamOne.SetActive(true);
            steamTwo.SetActive(true);
        }
        else
        {
            steamOne.SetActive(false);
            steamTwo.SetActive(false);
        }

    }
}
