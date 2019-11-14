using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Audio_Slider : MonoBehaviour
{
    [SerializeField]
    public static Slider _volValue;

    // Update is called once per frame
    void FixedUpdate()
    {
        //AudioListener.volume = _volValue.value*2;
    }
}
