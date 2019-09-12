using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge_Station : MonoBehaviour
{
    public bool chargeAvailable;

    private void Start()
    {
        chargeAvailable = true;
    }

    void TakeCharge()
    {
        chargeAvailable = false;
    }
}
