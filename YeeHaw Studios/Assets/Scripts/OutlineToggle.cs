using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineToggle : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private Renderer thisRend;
    //private Shader 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        thisRend = GetComponent<Renderer>(); // grab the renderer component
    }

    //Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= playerScript.InteractRange)
        {
            thisRend.material.SetFloat("_Outline", 0.075f);
            Debug.Log(thisRend.material.GetFloat("_Outline"));
            //outLineShader.id
        }
        else
        {
            thisRend.material.SetFloat("_Outline", 0.002f);
            Debug.Log(thisRend.material.GetFloat("_Outline"));
        }
    }
}
