using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    public Player player;

    public Sentry alertGuard;
    GameObject pressurePosition;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player>();

            if (player.Speed == player.walkSpeed)
            {
                if (alertGuard.PlayerTarget == null)
                    alertGuard.PlayerTarget = other.gameObject;

                alertGuard.NewTarget(player.transform.position);
            }
        }
    }
}
