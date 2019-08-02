﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera playerCamera;

    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;

    public float horizontal;
    public float vertical;

    private float mouseSensitivity = 5.0f/*0.5f*/;

    float playerTranslation;

    [SerializeField]
    private float interactRange = 3;

    GameObject player;
    public Rigidbody rb;

    private SphereCollider col;

    private Shader objectShader;

    private void Start()
    {
        player = gameObject;
        Cursor.lockState = CursorLockMode.Locked;

        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //if(Input.GetKey(KeyCode.W))
        //    rb.AddForce(transform.forward * 50);

        float translationX = Input.GetAxis("Horizontal") * speed;
        float translationZ = Input.GetAxis("Vertical") * speed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(translationX, 0, translationZ);

        //rb.MovePosition(new Vector3(translationX, 0, translationZ));

        // Rotate around our y-axis
        //transform.Rotate(0, rotation, 0);
        Rotation();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit outlineHit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out outlineHit, interactRange))
        {
            //objectShader = outlineHit.transform.gameObject.GetComponent<Shader>
            //if(outlineHit.transform.)
        }

        //=============================================================================

        //float translationX = Input.GetAxis("Horizontal") * speed;
        //float translationZ = Input.GetAxis("Vertical") * speed;

        //// Make it move 10 meters per second instead of 10 meters per frame...
        //translationX *= Time.deltaTime;
        //translationZ *= Time.deltaTime;

        //// Move translation along the object's z-axis
        //transform.Translate(translationX, 0, translationZ);

        ////rb.MovePosition(new Vector3(translationX, 0, translationZ));

        //// Rotate around our y-axis
        ////transform.Rotate(0, rotation, 0);
        //Rotation();


        //==============================================================================

        if (gameObject.GetComponent<Item_Use>().itemActive)
            interactRange = 10;
        else
            interactRange = 5;



        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        //    speed = 1.5f;
        //}
        //else
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
        //    speed = 3.0f;
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
            {
                //if it was a button, activate it's script
                if (hit.transform.tag == "Button")
                {
                    Debug.Log("Button clicked");
                    //hit.transform.gameObject.GetComponent<Button_Check>().ClickedOn();
                    hit.transform.gameObject.SendMessage("ClickedOn");
                }
                else if (hit.transform.tag == "Chest")
                {
                    Debug.Log("Chest clicked");
                    hit.transform.gameObject.SendMessage("CollectMoney");
                }

                gameObject.GetComponent<Item_Use>().Lightning();
            }
        }
    }

    void Rotation()
    {
        horizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        vertical -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vertical = Mathf.Clamp(vertical, -80, 80);

        transform.Rotate(0, horizontal, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(vertical, 0, 0);
    }
}
