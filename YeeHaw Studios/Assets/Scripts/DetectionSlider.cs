using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DetectionSlider : MonoBehaviour
{
    public Slider slider;
    public Sentry guard;

    private void Update()
    {
        slider.value = (guard.DetectionAmount / guard.MaxDetectionAmount);
        Debug.Log(slider.value);
    }
}
