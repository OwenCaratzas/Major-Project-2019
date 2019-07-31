using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon_Control : MonoBehaviour
{
    public float pylonCharge;

    private bool pylonDeactivated = false;

    // Start is called before the first frame update
    void Start()
    {
        //chargeCount = new Chest_Open[.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (pylonCharge == 0)
        {
            pylonDeactivated = true;
        }
    }
}
