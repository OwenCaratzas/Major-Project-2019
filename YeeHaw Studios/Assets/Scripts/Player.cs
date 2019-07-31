using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera playerCamera;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float horizontal;
    public float vertical;

    private float mouseSensitivity = 5.0f/*0.5f*/;

    float playerTranslation;

    GameObject player;

    private void Start()
    {
        player = gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float translationZ = Input.GetAxis("Vertical") * speed;
        float translationX = Input.GetAxis("Horizontal") * speed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(translationX, 0, translationZ);

        // Rotate around our y-axis
        //transform.Rotate(0, rotation, 0);
        Rotation();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //RaycastHit hit;

            //if(Physics.Raycast())
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
