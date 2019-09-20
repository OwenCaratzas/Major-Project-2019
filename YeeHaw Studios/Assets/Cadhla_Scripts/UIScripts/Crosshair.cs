using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{

    public Image crosshair;

    public Sprite standing;
    public Sprite crouching;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.MovementType == "Walk" || player.MovementType == "Sprint") //standing check here
        {
            crosshair.sprite = standing;
            crosshair.SetNativeSize();

        }
        else if (player.MovementType == "Crouch") //crouching check here
        {
            crosshair.sprite = crouching;
            crosshair.SetNativeSize();

        }
    }
}
