using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{

    Text text;
    public static int moneyCollected;

    // Start is called before the first frame update
    void Start()
    {
        moneyCollected = 0;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "$" + moneyCollected.ToString();
    }
}
