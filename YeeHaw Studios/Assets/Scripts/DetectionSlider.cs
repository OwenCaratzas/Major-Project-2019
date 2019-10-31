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
            needle.rectTransform.rotation = Quaternion.Euler(0f, 0f, audioValue);
        }
        else
        {
            audioValue = 43;
            needle.rectTransform.rotation = Quaternion.Euler(0f, 0f, audioValue);
        }
    }

}
