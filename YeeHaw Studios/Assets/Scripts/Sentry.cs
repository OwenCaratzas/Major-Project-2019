using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Sentry : MonoBehaviour
{
    #region Public Variables
    public float suspicionRate;
    /// <summary>
    /// List of transforms which will be the series of waypoints
    /// </summary>
    [Tooltip("Waypoint list, patrol will iterate through these in a cycle")]
    public Transform[] targetList;

    /// <summary>
    /// Any object that makes a sound within range of the AI, only the last sound will be investigated 
    /// </summary>
    [Tooltip("Whatever the AI has heard(object collision) *probably going to be removed and the collision sound will use a function*")]
    public Transform audioTarget;

    // search behaviour start and end points for the vision sweep, -1 interpolated to 1 is a nice horizontal sweep

    /// <summary>
    /// Interpolation start value
    /// </summary>
    [Tooltip("Interpolation start value")]
    public float startPoint = -1.0f;

    /// <summary>
    /// set animator controller for access
    /// </summary>
    [Tooltip("Animator Controller reference")]
    public Animator m_robotAnimController;

    /// <summary>
    /// Interpolation end value
    /// </summary>
    [Tooltip("Interpolation end value")]
    public float endPoint = 1.0f;

    /// <summary>
    /// The higher the value the faster it will reach the end point, so the vision sweep will be a smaller angle
    /// </summary>
    [Tooltip("The higher the multiplier, the shorter/faster the vision sweep")]
    public float interpolationMultiplier = 0.25f;

    [Header("Band-aid fix, tick if it isn't the first guard")]
    public bool beatTwoOver = false;

    public bool search = false;
    public bool skipDetection = false;

    [Space]
    public GameObject captureScreen;

    /// <summary>
    /// Current awareness of the AI
    /// </summary>
    [SerializeField]
    [Tooltip("How aware the AI is")]
    public float _detectionAmount = 0.0f;


    [Header("Serialized Private variables")]
    #endregion

    #region Private Variables

    ///// <summary>
    ///// Current awareness of the AI
    ///// </summary>
    //[SerializeField]
    //[Tooltip("How aware the AI is")]
    //private float _detectionAmount = 0.0f;

    /// <summary>
    /// Whatever the AI is currently navigating towards
    /// </summary>
    private Transform _currentTarget;

    /// <summary>
    /// The last player location that the AI was aware of
    /// </summary>
    /// needs to be set as private again after testing
    public Vector3 _lastKnownPlayerPos;

    /// <summary>
    /// Where the AI should be navigating
    /// </summary>
    private Vector3 _destination;

    private int _lastTarget;

    private bool _foundPlayer = false;

    private NavMeshAgent _agent;

    private BuildMesh _meshScript;

    private SphereCollider _col;

    private GameObject _player;

    private bool increaseDetection;

    private bool _playerOutOFBounds = false;

    [SerializeField]
    private bool _startSearch;

    // the different behaviour states
    private enum _BEHAVIOURS { Patrol, Search, Detected, Chase };

    // current behaviour
    [SerializeField]
    private _BEHAVIOURS _curBehaviour;
    private float _interpolationValue = 0.0f;

    private Light _spotlight;

    bool _playerNotFound;
    #endregion


    #region Getters & Setters

    public float MaxDetectionAmount { get; } = 100.0f;

    public GameObject PlayerTarget
    {
        get { return _player; }
        set { _player = value; }
    }

    public float DetectionAmount
    {
        get { return _detectionAmount; }
        set { _detectionAmount = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _spotlight = GetComponentInChildren<Light>();
        _meshScript = GetComponentInChildren<BuildMesh>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
        _destination = _agent.destination;

        _currentTarget = targetList[0];
        _lastTarget = 0;
        _col = GetComponent<SphereCollider>();
        _agent.updateRotation = true;
        //radius = 45.0f;
        _player = null;
    }

    private void Update()
    {
        Debug.Log(_agent.pathStatus);
        /*
        chase->search->patrol
        chase will always follow this progression

        search->chase/patrol
        search can lead to both chase and patrol

        patrol->chase/search
        patrol can lead to both chase and search
        */
        if (beatTwoOver)
        {
            // Swtich statement to control what behaviour is being used. Only one can be used at a time.
            switch (_curBehaviour)
            {
                // Make sure the AI resumes patrolling between the points and slows down to normal
                case _BEHAVIOURS.Patrol:
                    PatrolBehaviour();
                    break;
                // if the AI has just been chasing the player and can no longer see them, look before resuming patrol
                case _BEHAVIOURS.Search:
                    SearchBehaviour();
                    break;
                case _BEHAVIOURS.Detected:
                    DetectedBehaviour();
                    break;
                // Chase code AND set values like AI speed and stuff correctly
                case _BEHAVIOURS.Chase:
                    //DetectedBehaviour();
                    ChaseBehaviour();
                    break;
                default:
                    break;
            }

            // decrease detection if the player is not being detected
            if (!increaseDetection) //&& _curBehaviour != _BEHAVIOURS.Chase)
            {
                _detectionAmount -= MaxDetectionAmount * 0.001f;
                if (_detectionAmount < 0)
                    _detectionAmount = 0;
            }

            //Debug.Log("detection amount: " + _detectionAmount);
            if (_detectionAmount >= MaxDetectionAmount)
            {
                _foundPlayer = true;
                //_lastKnownPlayerPos = _player.transform.position;
            }
            else if (_detectionAmount <= 0)
                _foundPlayer = false; //_curBehaviour = _BEHAVIOURS.Patrol;

            if (_foundPlayer)
            {
                if(skipDetection)
                    _curBehaviour = _BEHAVIOURS.Chase;
                else if(!skipDetection)
                    _curBehaviour = _BEHAVIOURS.Detected;
                //Chase(_player.transform);

            }
            else if ((!_foundPlayer && _startSearch) || _startSearch)
            {
                //Patrol();
                //_curBehaviour = _BEHAVIOURS.Patrol;
                _curBehaviour = _BEHAVIOURS.Search;
                //_agent.speed = 1.0f;
            }
        }
    }



    void Patrol()
    {
        // set patrol anim
        m_robotAnimController.SetBool("Patrol", true);
        m_robotAnimController.SetBool("Searching", false);

        if (Vector3.Distance(_destination, _currentTarget.position) > 1.0f)
        {
            //destination = target.position;
            _destination = _currentTarget.position;
            _agent.destination = _destination;
        }
        else if (!_agent.pathPending && _agent.remainingDistance < 1)
        {
            if (_currentTarget != targetList[targetList.Length - 1])
            {
                _lastTarget++;
                _currentTarget = targetList[_lastTarget];
            }
            else
            {
                _lastTarget = 0;
                _currentTarget = targetList[_lastTarget];
            }
        }
    }

    void SeePlayer(List<RaycastHit> hitList)
    {

        if (hitList != null)
        {
            _playerNotFound = true;

            for (int i = 0; i < hitList.Count; i++)
            {
                if (hitList[i].collider.tag == "Player")
                {
                    _playerNotFound = false;
                    increaseDetection = true;
                    _player = hitList[i].collider.gameObject;
                    _detectionAmount = MaxDetectionAmount;
                    _lastKnownPlayerPos = _player.transform.position;
                }
                else if (_playerNotFound)
                    increaseDetection = false;
            }
        }
        else
            increaseDetection = false;

        //if (hit.collider.tag == "Player")
        //{
        //    Debug.Log("IT SEES");
        //    increaseDetection = true;
        //    _player = hit.collider.gameObject;
        //    _detectionAmount = MaxDetectionAmount;
        //    _lastKnownPlayerPos = _player.transform.position;
        //}
        //else
        //{
        //    increaseDetection = false;
        //}
    }

    void HeardSound()
    {
        //Debug.Log("The guard heard that");
        NewTarget(audioTarget.position);
        //_curBehaviour = _BEHAVIOURS.Chase;
    }

    void PatrolBehaviour()
    {
        //Debug.Log("Patrol Behaviour");
        //"Main" for everything patrol related
        _spotlight.color = Color.yellow;
        _agent.speed = 1.5f;
        Patrol();
    }


    void SearchBehaviour()
    {
        skipDetection = false;

        //Debug.Log("Search Behaviour");
        m_robotAnimController.SetBool("Searching", true);
        m_robotAnimController.SetBool("Chase", false);

        //"Main" for everything search related
        //_spotlight.color = new Color(1, 0.64f, 0, 1);
        _spotlight.color = Color.magenta;
        _agent.speed = 0.0f;
        // rotate for a few seconds then go back to patrol
        StartCoroutine(SearchRotation());
        _playerOutOFBounds = false;
        _curBehaviour = _BEHAVIOURS.Patrol;
    }


    void DetectedBehaviour()
    {

        //Debug.Log("Detected Behaviour");
        // set the detection animation and freeze the guard in place for a short period of time
        _spotlight.color = Color.red;
        _agent.speed = 0.0f;
        m_robotAnimController.SetBool("Detected", true);
        m_robotAnimController.SetBool("Patrol", false);

        Invoke("ChaseBehaviour", 2);

        
        //_curBehaviour = _BEHAVIOURS.Chase;
        //StartCoroutine(DetectionPlayThrough());
    }

    IEnumerator DetectionPlayThrough()
    {
        _spotlight.color = Color.red;
        //search = true;
        m_robotAnimController.SetBool("Detected", true);
        m_robotAnimController.SetBool("Patrol", false);
        yield return new WaitForSeconds(2);
        //search = false;
        //ChaseBehaviour();
        _curBehaviour = _BEHAVIOURS.Chase;
    }

    void ChaseBehaviour()
    {
        //if(Vector3.Distance(_player.transform.position, _agent.pathEndPosition) > 1.0f)
        skipDetection = true;
        //Debug.Log("Chase Behaviour");
        //"Main" for everything chase related
        _spotlight.color = Color.red;
        m_robotAnimController.SetBool("Detected", false);
        m_robotAnimController.SetBool("Chase", true);
        _agent.speed = 3.5f;
        //_lastKnownPlayerPos = _player.transform.position;
        //Chase(_player.transform);
        //_startSearch = true;
        
        _curBehaviour = _BEHAVIOURS.Chase;
        NewTarget(_lastKnownPlayerPos);
        //Debug.Log("Chase: " + Vector3.Distance(transform.position, _lastKnownPlayerPos));
        //if (_agent.transform.position == _lastKnownPlayerPos)
        //    _curBehaviour = _BEHAVIOURS.Search;
    }

    public void NewTarget(Vector3 targetPosition)
    {
        //_lastKnownPlayerPos = targetPosition;
        _agent.destination = targetPosition;
        StartSearch();
    }

    private void OnTriggerStay(Collider other)
    {
        ///*

        // if the trigger collider belonging to the player enters and stays within the AI's trigger collider
        if (other.transform.parent == null)
        {
            return;
        }
        else
        {
            if (other.transform.parent.tag == "Player" && beatTwoOver)
            {
                //Debug.Log(other.transform.parent.tag);
                // the parent of the collider is the player, so set _player to that gameobject
                GameObject player = other.transform.parent.gameObject;
                _player = player;

                // get the playerScript from the player
                Player playerScript = _player.GetComponent<Player>();
                float distance = Vector3.Distance(other.transform.position, transform.position);
                // if the player is currently moving
                if (playerScript.isMoving)
                {
                    // set this bool to true so that the detection doesn't yet decrease
                    increaseDetection = true;
                    suspicionRate = playerScript.suspicionRate;
                    //suspicionRate = (distance * 0.1f);
                    suspicionRate = suspicionRate * 1 / distance;
                    suspicionRate *= 2;
                    //start increasing by the modifier dependant on what pose the player is in
                    _detectionAmount += suspicionRate;


                    //here we set up parameters dependant on behaviour state


                    // make sure the detection amount can't be higher than the max
                    if (_detectionAmount > MaxDetectionAmount)
                        _detectionAmount = MaxDetectionAmount;

                    if (_detectionAmount >= MaxDetectionAmount)
                        _lastKnownPlayerPos = _player.transform.position;
                }
                // otherwise if the player ISN'T moving
                else if (!playerScript.isMoving)
                {
                    // set this to true so that the AI knows to start decreasing awareness
                    increaseDetection = false;
                }
            }
        }

        //*/

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == null)
        {
            return;
        }
        else
        {
            // if the trigger collider belonging to the player leaves the AI's trigger collider
            if (other.transform.parent.tag == "Player")
            {
                // the AI should no longer be able to hear the player, start decreasing the detection amount
                increaseDetection = false;
            }
        }
    }

    IEnumerator SearchRotation()
    {
        // Animate the rotation between start to end
        transform.Rotate(0, Mathf.Lerp(startPoint, endPoint, _interpolationValue), 0);

        // Increase the interpolation value
        _interpolationValue += interpolationMultiplier * Time.deltaTime;

        // If the interpolator value reaches it's current target, 
        // the points are swapped so that it should move to the opposite direction
        if (_interpolationValue > 1.0f)
        {
            float temp = endPoint;
            endPoint = startPoint;
            startPoint = temp;
            _interpolationValue = 0.0f;
        }
        //print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        //print(Time.time);
        _startSearch = false;
    }

    void StartSearch()
    {
        NavMeshHit navHit;
        NavMeshPath boundsPath = new NavMeshPath();
        if (NavMesh.SamplePosition(_player.transform.position, out navHit, 1f, NavMesh.AllAreas))
        {
            if(Vector3.Distance(_player.transform.position, navHit.position) > 1f)
                _playerOutOFBounds = true;
        }

        if (NavMesh.CalculatePath(_agent.transform.position, _lastKnownPlayerPos, NavMesh.AllAreas, boundsPath))
        {
            //boundsPath.
            Debug.Log(boundsPath.status);
            if (boundsPath.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("Path Not Complete");
            }
            //NavMesh.FindClosestEdge
                
        }

        if ((Vector3.Distance(transform.position, _lastKnownPlayerPos) < 1f) || (boundsPath.status == NavMeshPathStatus.PathPartial)
            //|| NavMesh.CalculatePath(_agent.transform.position, _lastKnownPlayerPos, NavMesh.AllAreas, _agent.path)
            /*(NavMesh.SamplePosition(_lastKnownPlayerPos, out navHit, 5f,NavMesh.AllAreas)) == false*/
            /*_playerOutOFBounds*/
            /*|| (Vector3.Distance(_player.transform.position, _agent.pathEndPosition) > 5f)*/)
        {
            //Debug.Log("AI has reached the last known player position");
            _startSearch = true;
            _foundPlayer = false;
            //Debug.Log("Distance from AI to the last known player position is: " + Vector2.Distance(_currentTarget.position, _lastKnownPlayerPos) + "AI needs to start searching");
            _curBehaviour = _BEHAVIOURS.Search;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (_curBehaviour == _BEHAVIOURS.Chase)
                    StartCoroutine(LoseScreen());
                else if (_curBehaviour == _BEHAVIOURS.Patrol || _curBehaviour == _BEHAVIOURS.Search)
                    DetectionAmount = 100;
            }
        }
    }

    IEnumerator LoseScreen()
    {
        captureScreen.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
        //Debug.Log("Didn't work");
    }
}
