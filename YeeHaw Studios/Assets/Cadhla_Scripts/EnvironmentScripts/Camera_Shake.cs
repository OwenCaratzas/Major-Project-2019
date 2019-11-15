using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public Transform camTransform;

    private Vector3 A;
    private Vector3 B;

    public float changeFactor = 0.001f;

    public float shakeDuration = 0f;

    public float shakeAmount = 1f;
    public float decreaseFactor = 1.0f;

    public float shakeTimer = 10f;

    public PauseMenu pauseCheck;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        SetFactor();
    }

    void SetFactor()
    {
        originalPos = camTransform.localPosition;

            A.Set(camTransform.localPosition.x, camTransform.localPosition.y + changeFactor, camTransform.localPosition.x);
            B.Set(camTransform.localPosition.x, camTransform.localPosition.y - changeFactor, camTransform.localPosition.x);

    }

    // Update is called once per frame
    void Update()
    {
        shakeTimer -= Time.deltaTime;
        if (pauseCheck.GameIsPaused == false)
        {
            camTransform.localPosition = Vector3.Lerp(B, A, Mathf.PingPong(Time.time * 1, shakeAmount));
        }

        if (shakeTimer <= 0)
        {
            shakeTimer = Random.Range(18,30);
            shakeDuration = 2;
        }

        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime * decreaseFactor;
            changeFactor = Random.Range(0.004f, 0.006f);
            SetFactor();
        }

        if (shakeDuration <= 0)
        {
            changeFactor = Random.Range (0.0005f, 0.003f);
            SetFactor();
        }
    }
}
