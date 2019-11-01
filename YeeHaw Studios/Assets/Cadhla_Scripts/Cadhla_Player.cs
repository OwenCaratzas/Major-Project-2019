using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadhla_Player : MonoBehaviour
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

    // controls what radius the audio collider will be on either walking or crouching
    public float walkAudioRadius;
    public float crouchAudioRadius;

    // keep track of whether the player is moving or not
    public bool isMoving;

    // the rate in which the player is becoming suspicious
    public float suspicionRate;

    // suspicionRate will change between these depending on if the player is walking or crouching
    public float walkSuspicionRate;
    public float crouchSuspicionRate;

    // Item UI reference
    public Item_Use rechargeUp;

    // battery gameobject reference
    public GameObject Battery;

    #endregion

    #region Private Variables

    // check for equipment active to send back to equipment script
    private bool interactRangeUp;

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
    public SphereCollider _col;

    // keep track of what time of movement the player is doing
    private string _movementType;


    private bool _wasCrouching;

    private bool _crouching;
    #endregion

    //private Shader _objectShader;

    private void Start()
    {
        // lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        // make the cursor invisible
        Cursor.visible = false;

        // set default values / references
        //_col = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _speed = walkSpeed;

        //reference to Item Use script
        rechargeUp = GetComponent<Item_Use>();
    }


    private void FixedUpdate()
    {
        // update the inputs and multiply by the speed modifier
        _translationX = Input.GetAxis("Horizontal") * _speed;
        _translationZ = Input.GetAxis("Vertical") * _speed;

        // apply the inputs to the character and move them
        _rb.MovePosition(transform.position + (transform.right * _translationX) + (transform.forward * _translationZ));

        // push the rigidbody directly up the y axis by multiplying it by the jumpForce
        if (Input.GetKeyDown("space"))
            _rb.AddForce(transform.up * jumpForce);

        // are we crouching or not?

        _crouching = Input.GetKey(KeyCode.LeftShift);
        if (_wasCrouching)
        {
            if (!_crouching)
            {
                //GetComponent<CapsuleCollider>().height = 2;

                Vector3 scale = GetComponent<Collider>().transform.localScale;
                scale.y *= 2.0f;
                GetComponent<Collider>().transform.localScale = scale;

                _movementType = "Walk";
                Debug.Log("Let go of shift");
                // normal movement
                _speed = walkSpeed;
            }
        }
        else
        {
            if (_crouching)
            {
                //GetComponent<CapsuleCollider>().height = 1;

                Vector3 scale = GetComponent<Collider>().transform.localScale;
                scale.y *= 0.5f;
                GetComponent<Collider>().transform.localScale = scale;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, 3.0f))
                {
                    scale = hit.point;
                    //scale = transform.position;
                    scale.x = transform.position.x;
                    scale.y += 0.1f;
                    scale.z = transform.position.z;
                    transform.position = scale;
                }


                //gameObject.transform.localScale -= new Vector3(0, 0.1f, 0);
                _movementType = "Crouch";
                // crouch movement
                _speed = crouchSpeed;
            }
        }
        _wasCrouching = _crouching;



        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    GetComponent<CapsuleCollider>().height = 1;
        //    //gameObject.transform.localScale -= new Vector3(0, 0.1f, 0);
        //    _movementType = "Crouch";
        //    // crouch movement
        //    _speed = crouchSpeed;
        //}

        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    GetComponent<CapsuleCollider>().height = 2;
        //    _movementType = "Walk";
        //    Debug.Log("Let go of shift");
        //    // normal movement
        //    _speed = walkSpeed;
        //}


        // apply rotations
        Rotation();
    }

    // Update is called once per frame
    void Update()
    {
        // Is the player currently moving?
        if (_translationX != 0 || _translationZ != 0)
        {
            isMoving = true;
            //      Turn on the SphereCollider
            //_col.enabled = true;
            //      Do a switch statement for the collider and suspicion rates to change between walking and crouching states
            //      This switch can also change the speed here, and the crouch logic can maybe also go here
            switch (_movementType)
            {
                case "Walk":
                    suspicionRate = walkSuspicionRate;
                    _col.radius = walkAudioRadius;
                    break;

                case "Crouch":
                    suspicionRate = crouchSuspicionRate;
                    _col.radius = crouchAudioRadius;
                    break;

                default:
                    suspicionRate = walkSuspicionRate;
                    _col.radius = walkAudioRadius;
                    break;
            }
        }
        else
        {
            isMoving = false;
            //_col.enabled = false;
        }

        RaycastHit outlineHit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out outlineHit, interactRange))
        {
            //objectShader = outlineHit.transform.gameObject.GetComponent<Shader>
            //if(outlineHit.transform.)
        }

        if (gameObject.GetComponent<Item_Use>().itemReady)
        {
            interactRange = 10;
            interactRangeUp = true;
        }
        else
            interactRange = 5;
        interactRangeUp = false;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
            {
                //if it was a button, activate it's script
                if (hit.transform.tag == "Button")
                {
                    //Debug.Log("Button clicked");
                    hit.transform.gameObject.SendMessage("ClickedOn");

                    if (gameObject.GetComponent<Item_Use>().itemReady)
                    {
                        gameObject.GetComponent<Item_Use>().Lightning();
                    }
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

                else if (hit.transform.tag == "BreakerSphere")
                {
                    hit.transform.gameObject.SendMessage("CompleteCircuit");
                }

                else if (hit.transform.tag == "Lever")
                {
                    hit.transform.gameObject.SendMessage("PullTheLever");

                    if (gameObject.GetComponent<Item_Use>().itemReady)
                    {
                        gameObject.GetComponent<Item_Use>().Lightning();
                    }
                }

                else if (hit.transform.tag == "Escape")
                {
                    hit.transform.gameObject.SendMessage("EscapeNow");
                }

                else if (hit.transform.tag == "Safe")
                {
                    hit.transform.gameObject.SendMessage("Search");
                }

                //else if (hit.transform.tag == "Battery")
                //{
                //    Battery = hit.transform.gameObject;
                //    if (Battery.GetComponent<Recharge_Station>().chargeAvailable)
                //    {
                //        rechargeUp.RechargeNow();
                //        hit.transform.gameObject.SendMessage("TakeCharge");
                //    }
                //}

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
