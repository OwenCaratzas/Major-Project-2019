using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    public Player player;

    public Sentry alertGuard;
    GameObject pressurePosition;

    public AudioClip impactClip;
    public AudioSource impactAudio;

    public bool firstContact = false;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            firstContact = true;
            player = other.gameObject.GetComponent<Player>();

            if (player.Speed >= player.walkSpeed)
            {
                if (impactAudio.clip != impactAudio)
                {
                    impactAudio.clip = impactClip;
                    impactAudio.Play();
                }
                if (alertGuard.PlayerTarget == null)
                    alertGuard.PlayerTarget = other.gameObject;
                    alertGuard.NewTarget(player.transform.position);
            }
        }
    }
}
