using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Variables

    // the camera mounted onto the player
    public Camera playerCamera;

    // normal movement speed
    public float walkSpeed;

    // when crouching, movement speed is dropped lower
    public float crouchSpeed;

    // force applied on the y axis to push the player up to simulate a jump. 50.0f is pretty good for an ok jump
    public float jumpForce = 50.0f;
    
    #endregion

    #region Private Variables

    // speed modifier for movement
    private float _speed;

    // how fast the character rotates
    private float _rotationSpeed = 10.0f;

    // horizontal and vertical mouse inputs
    private float _mouseX;
    private float _mouseY;

    // movement inputs after the speed modifier is applied
    private float _translationX;
    private float _translationZ;

    // mouse speed modifier
    private float _mouseSensitivity = 5.0f/*0.5f*/;

    // how far the player can interact with interactable objects. It get's serialized so we can see the value change without having that public
    [SerializeField]
    private float interactRange = 3;

    // rigidbody attached to the player's gameobject
    private Rigidbody _rb;

    // player's sphere collider, for audio range
    private SphereCollider _col;

    #endregion
    
    //private Shader _objectShader;

    private void Start()
    {
        // lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        // make the cursor invisible
        Cursor.visible = false;

        // set default values / references
        _col = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _speed = walkSpeed;
    }


    private void FixedUpdate()
    {
        // update the inputs and multiply by the speed modifier
        _translationX = Input.GetAxis("Horizontal") * _speed;
        _translationZ = Input.GetAxis("Vertical") * _speed;

        // apply the inputs to the character and move them
        _rb.MovePosition(transform.position  + (transform.right * _translationX) + (transform.forward * _translationZ));

        // push the rigidbody directly up the y axis by multiplying it by the jumpForce
        if (Input.GetKeyDown("space"))
            _rb.AddForce(transform.up * jumpForce);

        // are we crouching or not?

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // crouch movement
            _speed = crouchSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Let go of shift");
            // normal movement
            _speed = walkSpeed;
        }
        // apply rotations
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

        if (gameObject.GetComponent<Item_Use>().itemActive)
            interactRange = 10;
        else
            interactRange = 5;


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
                else if (hit.transform.tag == "TestObject")
                {
                    hit.rigidbody.AddForce(transform.forward * 1000);
                }

                gameObject.GetComponent<Item_Use>().Lightning();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void Rotation()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        _mouseY -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _mouseY = Mathf.Clamp(_mouseY, -80, 80);

        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(new Vector3(0, _mouseX, 0)));
        
        playerCamera.transform.localRotation = Quaternion.Euler(_mouseY, 0, 0);
    }

    public float Speed
    {
        get { return _speed; }
    }
}