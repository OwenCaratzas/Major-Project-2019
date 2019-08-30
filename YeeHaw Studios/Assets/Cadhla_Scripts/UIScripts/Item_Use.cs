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

    [SerializeField] private PowerBar powerBar;
    float power = 1f;

    private bool powerDrain = false;

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
                if (itemInactive == true && power == 1f)
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

        if (itemInactive == true)
        {
            equipmentImage.sprite = equipmentOff;
        }

        if (itemReady == true)
        {
            equipmentImage.sprite = equipmentOn;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemReady == true && power == 1f)
            {
                itemInactive = true;
                itemReady = false;
                GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
                powerDrain = true;
                //equipmentLimit--;
            }
        }
        if (powerDrain == true)
        {
            if (power > 0f)
            {
                power -= .01f;
                powerBar.SetSize(power);
            }
        }
        if (power == 0)
        {
            powerDrain = false;
        }
    }

    public void Lightning()
    {
        if (itemReady == true)
        {
            itemInactive = false;
            itemReady = false;
            GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
            //equipmentLimit--;
        }
    }
}
