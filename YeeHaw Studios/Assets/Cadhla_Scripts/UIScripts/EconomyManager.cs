using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{

    public static float totalMoney = 50;
    public static float rangeExtendCount = 1;

    public Text rangeExtendorCountText;
    public Text totalMoneyText;

    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        rangeExtendorCountText.text = rangeExtendCount.ToString();
        totalMoneyText.text = "$ " + totalMoney.ToString();
    }

    public void purchaseRangeExtend()
    {
        if (totalMoney >= 50)
        {
            rangeExtendCount++;
            totalMoney -= 50;
        }
    }
}
