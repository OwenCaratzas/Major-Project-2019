﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float startPoint = -1.0f;
    public float endPoint = 1.0f;

    public float interpolationMultiplier = 0.25f;

    private float _interpolationValue = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Animate the rotation between start to end
        transform.Rotate(0, Mathf.Lerp(startPoint, endPoint, _interpolationValue), 0);

        // Increase the interpolation value
        _interpolationValue += interpolationMultiplier * Time.deltaTime;

        // If the interpolator value reaches it's current target, 
        // the points are swapped so that it should move to the opposite direction
        if (_interpolationValue > 1.0f)
        {
            float temp = endPoint;
            endPoint = startPoint;
            startPoint = temp;
            _interpolationValue = 0.0f;
        }
    }
}
