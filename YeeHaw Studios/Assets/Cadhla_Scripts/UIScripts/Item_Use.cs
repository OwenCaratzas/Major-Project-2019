using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Use : MonoBehaviour
{
    public Text text;
    public GameObject image;
    //public static float equipmentLimit;

    public bool itemInactive = true;
    public bool itemReady = false;

    public GameObject lightningBolt;
    public GameObject Player;

    public PowerBar powerBar;
    public float power = 1f;

    public bool powerDrain = false;

    public bool powerRecharging = false;

    public Image equipmentImage;
    public Sprite equipmentOff;
    public Sprite equipmentOn;

    void Start()
    {
        //equipmentLimit = EconomyManager.rangeExtendCount;
        //text = GameObject.FindGameObjectWithTag("Equipment").GetComponent<Text>();

        equipmentImage = GameObject.FindGameObjectWithTag("Display").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
            }
        }
        if (power <= 0)
        {
            powerDrain = false;
        }

        //power bar recharge
        if (powerRecharging == true)
        {
            if (power <= 1f)
            {
                power += 0.1f;
                powerBar.SetReverseSize(power);
            }
        }
        if (power >= 1)
        {
            powerRecharging = false;
        }




        //testing mechanic
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemReady == true && power == 1f)
            {
                itemInactive = true;
                itemReady = false;
                GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
                powerDrain = true;
                Lightning();
                //equipmentLimit--;
            }
        }
    }

    public void Lightning()
    {
        if (itemReady == true)
        {
            Debug.Log("Shocking!");
            itemInactive = true;
            itemReady = false;
            GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
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
