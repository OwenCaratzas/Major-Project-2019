using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Use : MonoBehaviour
{
    public Text text;
    public GameObject image;
    //public static float equipmentLimit;

    public Player player;

    private bool itemInactive = true;
    public bool itemReady = false;

    public GameObject lightningBolt;
    public Transform boltSpawn;

    public PowerBar powerBar;
    private float power = 1f;

    private bool powerDrain = false;

    public GameObject equipmentImage;

    public Image back1;
    public Image back2;
    public Image back3;

    public float spinCog;
    //public Sprite equipmentOff;
    //public Sprite equipmentOn;

    public AudioClip noChargeClip;
    public AudioClip EMP_activate;
    public AudioSource noChargeAudio;

    void Start()
    {
        //equipmentLimit = EconomyManager.rangeExtendCount;
        //text = GameObject.FindGameObjectWithTag("Equipment").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (power > 0)
            {
                if (itemInactive == true && power >= 1f)
                {
                    equipmentImage.SetActive(false);

                    itemReady = false;
                    itemInactive = false;
                    //equipmentImage.sprite = equipmentOff;
                }

                else if (itemReady == false && power >= 1f)
                {
                    equipmentImage.SetActive(true);

                    itemInactive = true;
                    itemReady = true;
                    //equipmentImage.sprite = equipmentOn;
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
            back1.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            back2.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            back3.rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

            equipmentImage.SetActive(false);

            //equipmentImage.sprite = equipmentOff;
        }

        if (itemReady == true)
        {
            spinCog += Time.deltaTime;

            back1.rectTransform.rotation = Quaternion.Euler(0f, 0f, spinCog*16);
            back2.rectTransform.rotation = Quaternion.Euler(0f, 0f, -spinCog*20);
            back3.rectTransform.rotation = Quaternion.Euler(0f, 0f, spinCog*20);

            equipmentImage.SetActive(true);

            //equipmentImage.sprite = equipmentOn;
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

    }

    public void Lightning()
    {
        if (itemReady == true)
        {
            boltSpawn = player.particleTarget;
            itemInactive = true;
            itemReady = false;
            GameObject go = Instantiate(lightningBolt, boltSpawn.transform.position, boltSpawn.transform.rotation);
            powerDrain = true;
            //equipmentLimit--;
        }
    }

}
