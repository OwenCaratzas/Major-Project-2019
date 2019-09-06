using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DetectionSlider : MonoBehaviour
{
    public Slider slider;
    public Sentry guard;
    public Player player;

    private void Update()
    {
        if (player.isMoving)
        {
            slider.value = player.suspicionRate;
        }
        else
            slider.value *= 0.9f;
        //slider.value = (guard.DetectionAmount / guard.MaxDetectionAmount);
        //Debug.Log(slider.value);
    }
}
