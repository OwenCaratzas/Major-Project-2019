using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Open : MonoBehaviour
{
    public GameObject[] chestPylons;
    private GameObject[] Pylon;
    public int numPylons = 4;

    private Pylon_Control[] chargeBuild;
    public bool[] chargeCheck;

    public bool chestFull = true;
   
    // Start is called before the first frame update
    void Start()
    {
        Pylon = GameObject.FindGameObjectsWithTag("Pylon");

        //chestPylons = new GameObject[numPylons];

        //chargeCheck = new bool[chargeBuild.Length];

        for (int i = 0; i < numPylons; i++)
        {
            //chestPylons[i] = Pylon[i].gameObject;
            //chargeBuild[i] = Pylon[i].GetComponent<Pylon_Control>();

            //if (chargeCheck[i].pylonDeactivated == true)
            //{

            //}
        }

        

    }

    // Update is called once per frame
    void CollectMoney()
    {
        if (chestFull == true)
        {
            chestFull = false;
            ScoreTextScript.coinAmount += 100;
        }
    }
}
