using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Use : MonoBehaviour
{
    public Text text;
    public GameObject image;
    //public static float equipmentLimit;

    private bool itemInactive = true;
    public bool itemReady = false;

    public GameObject lightningBolt;
    public GameObject boltSpawn;

    public PowerBar powerBar;
    private float power = 1f;

    private bool powerDrain = false;

    private bool powerRecharging = false;

    public Image equipmentImage;
    public Sprite equipmentOff;
    public Sprite equipmentOn;

    public AudioClip noChargeClip;
    public AudioClip EMP_activate;
    public AudioSource noChargeAudio;

    void Start()
    {
        //equipmentLimit = EconomyManager.rangeExtendCount;
        //text = GameObject.FindGameObjectWithTag("Equipment").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (power > 0)
            {
                if (itemInactive == true && power >= 1f)
                {
                    itemReady = true;
                    itemInactive = false;
                    equipmentImage.sprite = equipmentOff;
                }

                else if (itemReady == true)
                {
                    itemInactive = true;
                    itemReady = false;
                    equipmentImage.sprite = equipmentOn;
                }
            }

            else if(power <= 0)
            {
                noChargeAudio.Play();
                noChargeAudio.clip = noChargeClip;
            }
        }

        //sprite check
        if (itemInactive == true)
        {
            equipmentImage.sprite = equipmentOff;
        }

        if (itemReady == true)
        {
            equipmentImage.sprite = equipmentOn;
        }

        //power bar check for draining
        if (powerDrain == true)
        {
            if (power >= 0f)
            {
                power -= .01f;
                powerBar.SetSize(power);

                if (noChargeAudio.clip != EMP_activate)
                {
                    noChargeAudio.clip = EMP_activate;
                    noChargeAudio.Play();
                }
            }
        }
        if (power <= 0)
        {
            powerDrain = false;
        }

        //recharge over time

        if (power < 1)
        {
            power += 0.02f * Time.deltaTime;
            powerBar.SetReverseSize(power);
        }



        ////power bar recharge
        //if (powerRecharging == true)
        //{
        //    if (power <= 1f)
        //    {
        //        power += 0.1f;
        //        powerBar.SetReverseSize(power);
        //    }
        //}
        //if (power >= 1)
        //{
        //    powerRecharging = false;
        //}




        ////testing mechanic
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (itemReady == true && power == 1f)
        //    {
        //        itemInactive = true;
        //        itemReady = false;
        //        GameObject go = Instantiate(lightningBolt, boltSpawn.transform.position, boltSpawn.transform.rotation);
        //        powerDrain = true;
        //        Lightning();
        //        //equipmentLimit--;
        //    }
        //}
    }

    public void Lightning()
    {
        if (itemReady == true)
        {
            Debug.Log("Shocking!");
            itemInactive = true;
            itemReady = false;
            GameObject go = Instantiate(lightningBolt, boltSpawn.transform.position, boltSpawn.transform.rotation);
            powerDrain = true;
            //equipmentLimit--;
        }
    }


    public void RechargeNow()
    {
        if (power <= 0)
        {
            powerRecharging = true;
        }

    }

}
