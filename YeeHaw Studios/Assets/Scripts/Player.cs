using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// Place the camera mounted on the player here
    /// </summary>
    [Tooltip("Place the main camera here")]
    public Camera playerCamera;

    // normal movement speed
    /// <summary>
    /// How fast the player walks
    /// </summary>
    [Tooltip("Walk speed multiplier")]
    public float walkSpeed;

    // sprinting speed
    /// <summary>
    /// How fast the player sprints
    /// </summary>
    [Tooltip("Sprint speed multiplier")]
    public float sprintSpeed;

    // when crouching, movement speed is dropped lower
    /// <summary>
    /// How fast the player crouches
    /// </summary>
    [Tooltip("Crouch speed multiplier")]
    public float crouchSpeed;

    // force applied on the y axis to push the player up to simulate a jump. 50.0f is pretty good for an ok jump
    /// <summary>
    /// The amount of upward force applied to the player upong jumping
    /// </summary>
    [Tooltip("The amount of upward force applied to the player")]
    public float jumpForce = 50.0f;

    // controls what radius the audio collider will be on either walking, sprinting or crouching

    /// <summary>
    /// Audio radius for walking
    /// </summary>
    [Tooltip("The radius in which the AI will hear the player moving while walking")]
    public float walkAudioRadius;

    /// <summary>
    /// Audio radius for sprinting
    /// </summary>
    [Tooltip("The radius in which the AI will hear the player moving while sprinting")]
    public float sprintAudioRadius;

    /// <summary>
    /// Audio radius for crouching
    /// </summary>
    [Tooltip("The radius in which the AI will hear the player moving while crouching")]
    public float crouchAudioRadius;

    // keep track of whether the player is moving or not
    public bool isMoving;

    // the rate in which the player is becoming suspicious
    public float suspicionRate;

    // suspicionRate will change between these depending on if the player is walking, sprinting or crouching

    /// <summary>
    /// Detection rate for walking
    /// </summary>
    [Tooltip("The rate in which the AI will detect the player moving while walking")]
    public float walkSuspicionRate;

    /// <summary>
    /// Detection rate for sprinting
    /// </summary>
    [Tooltip("The rate in which the AI will detect the player moving while sprinting")]
    public float sprintSuspicionRate;

    /// <summary>
    /// Detection rate for crouching
    /// </summary>
    [Tooltip("The rate in which the AI will detect the player moving while crouching")]
    public float crouchSuspicionRate;

    // the amount of noise the player is making

    // Is the player grounded?
    public bool isGrounded;

    // Item UI reference
    public Item_Use rechargeUp;

    // battery gameobject reference
    public GameObject Battery;

    //setting player footstep audio
    public AudioClip WalkingFootstepClip;
    public AudioClip CrouchingFootstepClip;
    public AudioClip RunningFootstepClip;
    public AudioSource playerFootstepAudio;

    /// <summary>
    /// set animator controller for access
    /// </summary>
    [Tooltip("Animator Controller reference")]
    public Animator HandsAnim;

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
    private float _interactRange = 3;

    // rigidbody attached to the player's gameobject
    private Rigidbody _rb;

    // player's sphere collider, for audio range
    public SphereCollider _col;

    // keep track of what time of movement the player is doing
    [SerializeField]
    private string _movementType;

    // crouch state of the previous frame
    [SerializeField]
    private bool _wasCrouching;
    [SerializeField]
    private bool _crouching;
    [SerializeField]
    private bool _sprinting;

    /// <summary>
    /// Should we crouch after sprinting
    /// </summary>
    private bool _sprintCrouching;

    
    #endregion

    public float InteractRange
    {
        get { return _interactRange; }
        set { _interactRange = value; }
    }

    public string MovementType
    {
        get { return _movementType; }
        set { _movementType = value; }
    }
    //private Shader _objectShader;

    private void Start()
    {
        //Time.timeScale = 1f;
        // lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        // make the cursor invisible
        Cursor.visible = false;

        // set default values / references
        //_col = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
        _speed = walkSpeed;
        _movementType = "Walk";

        HandsAnim.SetBool("Idle", true);
        HandsAnim.SetBool("Crouching", false);
        HandsAnim.SetBool("Walking", false);
        HandsAnim.SetBool("Running", false);

        TurnOnMouse();


    }


    private void FixedUpdate()
    {
        // update the inputs and multiply by the speed modifier
        _translationX = Input.GetAxis("Horizontal")/* * _speed*/;
        _translationZ = Input.GetAxis("Vertical") /** _speed*/;

        // apply the inputs to the character and move them
        //_rb.MovePosition(transform.position + (transform.right * _translationX) + (transform.forward * _translationZ));
        _rb.MovePosition(
            transform.position + Vector3.ClampMagnitude(
                (transform.right * _translationX) + (transform.forward * _translationZ), 1f) * _speed);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, _interactRange))
            {
                //Debug.Log(hit.collider.name);

                //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.green, _interactRange);

                //if it was a button, activate it's script
                if (hit.transform.tag == "Button")
                {
                    //Debug.Log("Button clicked");
                    //hit.transform.gameObject.GetComponent<Button_Check>().ClickedOn();
                    hit.transform.gameObject.SendMessage("ClickedOn");

                    if (gameObject.GetComponent<Item_Use>().itemReady)
                    {
                        gameObject.GetComponent<Item_Use>().Lightning();
                    }
                }
                else if (hit.transform.tag == "Chest")
                {
                    //Debug.Log("Chest clicked");
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
                    //Debug.Log("HitLever");
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

                else if (hit.transform.tag == "Battery")
                {
                    Battery = hit.transform.gameObject;
                    if (Battery.GetComponent<Recharge_Station>().chargeAvailable)
                    {
                        rechargeUp.RechargeNow();
                        hit.transform.gameObject.SendMessage("TakeCharge");
                    }
                }

                //gameObject.GetComponent<Item_Use>().Lightning();
            }
        }

        crouchSuspicionRate = Random.Range(0.04f, 0.06f);
        walkSuspicionRate = Random.Range(0.18f, 0.22f);
        sprintSuspicionRate = Random.Range(0.37f, 0.43f);

        if (_sprinting)
        {
            _movementType = "Sprint";
        }

        // if the player WAS crouching, but no longer is crouching, make the player stand up and move to walking
        if (_wasCrouching || !_sprinting)
        {
            if (!_crouching)
            {
                Vector3 scale = GetComponent<Collider>().transform.localScale;
                //scale.y = 1.0f;
                //GetComponent<Collider>().transform.localScale = scale;
                GetComponent<CapsuleCollider>().height = 2;
                //if (_sprinting)
                //{
                //    _movementType = "Sprint";
                //}
                //else
                //{
                    _movementType = "Walk";
                    // normal movement
                   
                //}

            }
        }
        if (_crouching)
        {
            GetComponent<CapsuleCollider>().height = 0.5f;



            //may need this

            //Vector3 scale = GetComponent<Collider>().transform.localScale;
            //scale.y = 0.5f;
            //GetComponent<Collider>().transform.localScale = scale;



            //THIS EVENTUALLY NEEDS TO WORK PROPERLY
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, -transform.up, out hit, 3.0f))
            //{
            //    scale = hit.point;
            //    //scale = transform.position;
            //    scale.x = transform.position.x;
            //    //if(scale.y )
            //    scale.y += 0.1f;
            //    scale.z = transform.position.z;
            //    transform.position = scale;
            //}


            //gameObject.transform.localScale -= new Vector3(0, 0.1f, 0);
            _movementType = "Crouch";
            // crouch movement
            
        }
        _wasCrouching = _crouching;

        // apply rotations
        Rotation();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            if (_crouching)
                _crouching = false;
            else
                _crouching = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_crouching)
                _sprintCrouching = true;
            _sprinting = true;
            _crouching = false;
        }
        else
        {
            //_crouching = !_crouching;
            if (_sprintCrouching)
            {
                _sprintCrouching = false;
                _crouching = true;
            }

            _sprinting = false;
        }

        // Is the player currently moving?
        if (_translationX != 0 || _translationZ != 0)
        {
            isMoving = true;
            switch (_movementType)
            {
                case "Sprint":
                    suspicionRate = sprintSuspicionRate;
                    _col.radius = sprintAudioRadius;
                    _speed = sprintSpeed;
                    HandsAnim.SetBool("Running", true);
                    HandsAnim.SetBool("Walking", false);
                    HandsAnim.SetBool("Crouching", false);
                    HandsAnim.SetBool("Idle", false);

                    //setting audio to play
                    if (playerFootstepAudio.clip != RunningFootstepClip)
                    {
                        playerFootstepAudio.volume = 0.3f;
                        playerFootstepAudio.clip = RunningFootstepClip;
                        playerFootstepAudio.Play();
                    }
                    break;

                case "Walk":
                    suspicionRate = walkSuspicionRate;
                    _col.radius = walkAudioRadius;
                    _speed = walkSpeed;
                    HandsAnim.SetBool("Walking", true);
                    HandsAnim.SetBool("Running", false);
                    HandsAnim.SetBool("Crouching", false);
                    HandsAnim.SetBool("Idle", false);

                    //setting audio to play
                    if (playerFootstepAudio.clip != WalkingFootstepClip)
                    {
                        playerFootstepAudio.volume = 0.2f;
                        playerFootstepAudio.clip = WalkingFootstepClip;
                        playerFootstepAudio.Play();
                    }
                    break;

                case "Crouch":
                    suspicionRate = crouchSuspicionRate;
                    _col.radius = crouchAudioRadius;
                    _speed = crouchSpeed;

                    HandsAnim.SetBool("Crouching", true);
                    HandsAnim.SetBool("Running", false);
                    HandsAnim.SetBool("Walking", false);
                    HandsAnim.SetBool("Idle", false);

                    //setting audio to play
                    if (playerFootstepAudio.clip != CrouchingFootstepClip)
                    {
                        playerFootstepAudio.volume = 0.1f;
                        playerFootstepAudio.clip = CrouchingFootstepClip;
                        playerFootstepAudio.Play();
                    }
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

            suspicionRate = 0f;
            playerFootstepAudio.clip = null;
            playerFootstepAudio.Pause();

            HandsAnim.SetBool("Idle", true);
            HandsAnim.SetBool("Running", false);
            HandsAnim.SetBool("Walking", false);
            HandsAnim.SetBool("Crouching", false);
        }

        //RaycastHit outlineHit;
        //if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out outlineHit, _interactRange))
        //{
        //    //objectShader = outlineHit.transform.gameObject.GetComponent<Shader>
        //    //if(outlineHit.transform.)
        //}

        if (gameObject.GetComponent<Item_Use>().itemReady)
            _interactRange = 10;
        else
            _interactRange = 5;


        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, _interactRange))
        //    {
        //        Debug.Log(hit.collider.name);

        //        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.green, _interactRange);

        //        //if it was a button, activate it's script
        //        if (hit.transform.tag == "Button")
        //        {
        //            //Debug.Log("Button clicked");
        //            //hit.transform.gameObject.GetComponent<Button_Check>().ClickedOn();
        //            hit.transform.gameObject.SendMessage("ClickedOn");

        //            if (gameObject.GetComponent<Item_Use>().itemReady)
        //            {
        //                gameObject.GetComponent<Item_Use>().Lightning();
        //            }
        //        }
        //        else if (hit.transform.tag == "Chest")
        //        {
        //            //Debug.Log("Chest clicked");
        //            hit.transform.gameObject.SendMessage("CollectMoney");
        //        }
        //        else if (hit.transform.tag == "TestObject")
        //        {
        //            hit.rigidbody.AddForce(transform.forward * 1000);
        //        }

        //        else if (hit.transform.tag == "BreakerSphere")
        //        {
        //            hit.transform.gameObject.SendMessage("CompleteCircuit");
        //        }

        //        else if (hit.transform.tag == "Lever")
        //        {
        //            //Debug.Log("HitLever");
        //            hit.transform.gameObject.SendMessage("PullTheLever");

        //            if (gameObject.GetComponent<Item_Use>().itemReady)
        //            {
        //                gameObject.GetComponent<Item_Use>().Lightning();
        //            }
        //        }

        //        else if (hit.transform.tag == "Escape")
        //        {
        //            hit.transform.gameObject.SendMessage("EscapeNow");
        //        }

        //        else if (hit.transform.tag == "Safe")
        //        {
        //            hit.transform.gameObject.SendMessage("Search");
        //        }

        //        else if (hit.transform.tag == "Battery")
        //        {
        //            Battery = hit.transform.gameObject;
        //            if (Battery.GetComponent<Recharge_Station>().chargeAvailable)
        //            {
        //                rechargeUp.RechargeNow();
        //                hit.transform.gameObject.SendMessage("TakeCharge");
        //            }
        //        }

        //        //gameObject.GetComponent<Item_Use>().Lightning();
        //    }
        //}

        //crouchSuspicionRate = Random.Range(0.04f, 0.06f);
        //walkSuspicionRate = Random.Range(0.18f, 0.22f);
        //sprintSuspicionRate = Random.Range(0.37f, 0.43f);


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
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

    public void TurnOffMouse()
    {
        // lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        // make the cursor invisible
        Cursor.visible = false;
    }

    public void TurnOnMouse()
    {
        // lock cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Confined;
        // make the cursor visible
        Cursor.visible = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }
}