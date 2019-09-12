using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    public Player player;

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
            player = collision.gameObject.GetComponent<Player>();

            if (player.Speed == player.walkSpeed)
            {
                if(alertGuard.PlayerTarget == null)
                    alertGuard.PlayerTarget = collision.gameObject;

                alertGuard.NewTarget(player.transform.position);
            }
        }
    }
}
