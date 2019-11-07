using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionMeter : MonoBehaviour
{
    public Transform bar;

    public Sentry[] Guards;

    private float currentDetectionValue = 0f;
    private float maxDetectionValue = 0f;


    // Start is called before the first frame update

    private void Start()
    {

    }

    void Update()
    {
        currentDetectionValue = maxDetectionValue;

        for (int i = 0; i < Guards.Length; i++)
        {
            if (Guards[i]._detectionAmount > 0f)
            {
                if (maxDetectionValue >= currentDetectionValue)
                {
                    maxDetectionValue = Guards[i]._detectionAmount;
                }
                else if (currentDetectionValue <= maxDetectionValue)
                {

                }
            }
        }

        bar.localScale = new Vector3((currentDetectionValue/100),1f);

    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetReverseSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
