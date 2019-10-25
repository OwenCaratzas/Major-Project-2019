using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textFour;

    private float oneFadeIn;
    private float twoFadeIn;
    private float threeFadeIn;
    private float fourFadeIn;

    public GameObject Main;

    private float FadeOut;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        textOne.color = new Color(textOne.color.r, textOne.color.g, textOne.color.b, 0f);
        textTwo.color = new Color(textTwo.color.r, textTwo.color.g, textTwo.color.b, 0f);
        textThree.color = new Color(textThree.color.r, textThree.color.g, textThree.color.b, 0f);
        textFour.color = new Color(textFour.color.r, textFour.color.g, textFour.color.b, 0f);

        Main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        timer = timer + 1 * Time.deltaTime;

        if (timer > 0 && timer < 3)
        {
            oneFadeIn = oneFadeIn + 1 * Time.deltaTime;
            textOne.color = new Color(textOne.color.r, textOne.color.g, textOne.color.b, oneFadeIn);
        }

        if (timer > 3 && timer < 6)
        {
            twoFadeIn = twoFadeIn + 1 * Time.deltaTime;
            textTwo.color = new Color(textTwo.color.r, textTwo.color.g, textTwo.color.b, twoFadeIn);
        }

        if (timer > 6 && timer < 9)
        {
            threeFadeIn = threeFadeIn + 1 * Time.deltaTime;
            textThree.color = new Color(textThree.color.r, textThree.color.g, textThree.color.b, threeFadeIn);
        }

        if (timer > 9)
        {
            oneFadeIn = oneFadeIn - 2 * Time.deltaTime;
            twoFadeIn = twoFadeIn - 2 * Time.deltaTime;
            threeFadeIn = threeFadeIn - 2 * Time.deltaTime;

            textOne.color = new Color(textOne.color.r, textOne.color.g, textOne.color.b, oneFadeIn);
            textTwo.color = new Color(textTwo.color.r, textTwo.color.g, textTwo.color.b, twoFadeIn);
            textThree.color = new Color(textThree.color.r, textThree.color.g, textThree.color.b, threeFadeIn);
  
        }

        if (timer > 11)
        {
            fourFadeIn = fourFadeIn + 1 * Time.deltaTime;
            textFour.color = new Color(textFour.color.r, textFour.color.g, textFour.color.b, fourFadeIn);
        }

        if (timer > 14)
        {
            SceneManager.LoadScene("LevelOne");
        }
    }
}
