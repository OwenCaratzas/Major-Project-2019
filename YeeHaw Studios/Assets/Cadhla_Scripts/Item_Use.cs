using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Use : MonoBehaviour
{
    public Text text;
    public GameObject image;
    public static int equipmentLimit;

    public bool itemActive = false;
    public bool itemReady = false;

    public GameObject lightningBolt;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        equipmentLimit = 10;
        //text = GameObject.FindGameObjectWithTag("Equipment").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        text.text = equipmentLimit.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (equipmentLimit > 0)
            {
                if (itemActive == false)
                {
                    itemActive = true;
                    itemReady = true;
                    image.GetComponent<Image>().color = new Color32(1, 250, 255, 250);
                }

                else if (itemActive == true)
                {
                    itemActive = false;
                    itemReady = false;
                    image.GetComponent<Image>().color = new Color32(255, 255, 255, 250);
                }
            }
        }

        if (itemActive == false)
        {
            image.GetComponent<Image>().color = new Color32(255, 255, 255, 250);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //if (itemReady == true)
            //{
            //    itemActive = false;
            //    itemReady = false;
            //    GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
            //    equipmentLimit --;
            //}
            Lightning();
        }
    }

    public void Lightning()
    {
        if (itemReady == true)
        {
            itemActive = false;
            itemReady = false;
            GameObject go = Instantiate(lightningBolt, Player.transform.position, Player.transform.rotation);
            equipmentLimit--;
        }
    }
}
