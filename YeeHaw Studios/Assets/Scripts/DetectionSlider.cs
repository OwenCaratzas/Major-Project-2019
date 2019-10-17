using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DetectionSlider : MonoBehaviour
{
    //public Slider slider;
    public Image needle;

    public Player player;
    private float audioValue;


    private void Update()
    {
        if (player.isMoving)
        {
            audioValue = 43 - (0.86f * (player.suspicionRate * 200));
            //slider.value = player.suspicionRate;
            needle.rectTransform.rotation = Quaternion.Euler(0f, 0f, audioValue);
        }
        else
        {
            //slider.value *= 0.9f;
            audioValue = 43;
            needle.rectTransform.rotation = Quaternion.Euler(0f, 0f, audioValue);
        }
        //slider.value = (guard.DetectionAmount / guard.MaxDetectionAmount);
        //Debug.Log(slider.value);
    }
}
