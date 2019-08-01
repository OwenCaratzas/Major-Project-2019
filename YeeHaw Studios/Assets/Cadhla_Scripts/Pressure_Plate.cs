using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    public Player playerSpeed;

    public Sentry alertGuard;
    GameObject pressurePosition;

    // Start is called before the first frame update


    public void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.tag == "Player")
        {
            if (playerSpeed.speed > 5)
            {

                alertGuard.Chase();
            }
        }
    }
}
