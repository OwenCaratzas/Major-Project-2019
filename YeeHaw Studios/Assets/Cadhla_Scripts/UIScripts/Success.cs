using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    public Image successImage;
    public float timer;

    public float delay;
    public string fullText;
    public string currentText;

    void Start()
    {
        timer = 18f;
        delay = 0.1f;
        StartCoroutine(ShowText());
    }

    public void FixedUpdate()
    {
        if (Escape.objectiveComplete == true)
        {
            timer -= Time.deltaTime;
            successImage.color = new Color(successImage.color.r, successImage.color.g, successImage.color.b, 255f);
            fullText = "This is where our tale ends.\n\n\nThrough thick and thin,\n\n\nOur hero stands successful.\n\n\nAnd that was,\n\n\nThe Great Train Heist. ".ToString();
        }

        if (Escape.objectiveComplete == false)
        {
            timer = 18f;
            successImage.color = new Color(successImage.color.r, successImage.color.g, successImage.color.b, 0f);
            this.GetComponent<Text>().text = "".ToString();
        }

        if (timer <= 0)
        {
            Escape.objectiveComplete = false;
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
