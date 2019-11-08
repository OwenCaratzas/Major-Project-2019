using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Audio_Slider : MonoBehaviour
{
    [SerializeField]
    public static Slider _volValue;


    // Start is called before the first frame update
    void Awake()
    {
        //_volValue.value = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
       AudioListener.volume = _volValue.value*2;
    }
}
