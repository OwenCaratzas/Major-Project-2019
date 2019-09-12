using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialVisualPrompts : MonoBehaviour
{
    public Transform tutorialPrompt;

    public GameObject text;

    private Vector3 A;
    private Vector3 B;
    private float Length;
    private float speed;


    private void Start()
    {
        text.transform.position = tutorialPrompt.transform.position;
    }

    // Start is called before the first frame update
    public void SetPosition()
    {
        A.Set(text.transform.position.x, text.transform.position.y, text.transform.position.z);
        B.Set(text.transform.position.x, text.transform.position.y + 1f, text.transform.position.z);

        speed = 0.1f;
        Length = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(A, B, Mathf.PingPong(Time.time * speed, Length));
    }
}
