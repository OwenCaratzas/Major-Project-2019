using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour
{
    #region Public Variables
    public Transform target;
    public Transform[] targetList;
    public Transform currentTarget;
    public Transform audioTarget;

    public GameObject visionDetectionOrigin;
    public GameObject face;

    public float fovAngle = 110f;
    // might have to be vector3's
    public float radius;
    public float height;

    public Material passive;
    public Material alerted;

    public Vector3 radiusVert;
    public Vector3 distanceVert;
    #endregion

    #region Private Variables
    [SerializeField]
    private float _detectionAmount = 0.0f;
    private float _maxDetection = 100.0f;
    private float _maxRange = 150.0f;
    private float _maxAngle = 45.0f;

    private Vector3 _lastKnownPlayerPos;
    private Vector3 _noAngle;
    private Vector3 _destination;
    private Vector3[] _playerLocations;
    //Vector3 direction;

    private int _lastTarget;

    private bool _foundPlayer = false;

    private NavMeshAgent _agent;

    private BuildMesh _meshScript;

    private SphereCollider _col;

    private GameObject _player;

    private bool increaseDetection;

    [SerializeField]
    private bool _startSearch;

    // the different behaviour states
    private enum _BEHAVIOURS { Patrol, Search, Chase};

    // current behaviour
    [SerializeField]
    private _BEHAVIOURS _curBehaviour;
    #endregion

    public float startPoint = -1.0f;
    public float endPoint = 1.0f;

    public float interpolationMultiplier = 0.25f;

    private float _interpolationValue = 0.0f;

    private Light _spotlight;
    #region Getters & Setters
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

    public float MaxDetectionAmount
    {
        get { return _maxDetection; }
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

        currentTarget = targetList[0];
        _lastTarget = 0;
        _col = GetComponent<SphereCollider>();
        _agent.updateRotation = true;
        radius = 45.0f;
        _player = null;
    }

    private void Update()
    {

        /*
        chase->search->patrol
        chase will always follow this progression

        search->chase/patrol
        search can lead to both chase and patrol

        patrol->chase/search
        patrol can lead to both chase and search
        */

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
            // Chase code AND set values like AI speed and stuff correctly
            case _BEHAVIOURS.Chase:
                ChaseBehaviour();
                break;
            default:
                break;
        }

        // decrease detection if the player is not being detected
        if (!increaseDetection)
        {
            _detectionAmount -= _maxDetection * 0.0005f;
            if (_detectionAmount < 0)
                _detectionAmount = 0;
        }

        //Debug.Log("detection amount: " + _detectionAmount);
        if (_detectionAmount >= _maxDetection)
        {
            _foundPlayer = true;
            _lastKnownPlayerPos = _player.transform.position;
        }
        else if (_detectionAmount <= 0)
            _foundPlayer = false; //_curBehaviour = _BEHAVIOURS.Patrol;

        if (_foundPlayer)
        {
            _curBehaviour = _BEHAVIOURS.Chase;
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

    

    void Patrol()
    {
        if (Vector3.Distance(_destination, currentTarget.position) > 1.0f)
        {
            //destination = target.position;
            _destination = currentTarget.position;
            _agent.destination = _destination;
        }
        else if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            //if (Vector3.Distance(currentTarget.position, _lastKnownPlayerPos) < 5.0f)
            //{
            //    Debug.Log("Distance from AI to the last known player position is: " + Vector2.Distance(currentTarget.position, _lastKnownPlayerPos) + "AI needs to start searching");
            //    _curBehaviour = _BEHAVIOURS.Search;
            //    _lastKnownPlayerPos = new Vector3();
            //}
            //StartSearch();
            if (currentTarget != targetList[targetList.Length - 1])
            {
                _lastTarget++;
                currentTarget = targetList[_lastTarget];
            }
            else
            {
                _lastTarget = 0;
                currentTarget = targetList[_lastTarget];
            }
        }
    }

    void SeePlayer(RaycastHit hit)
    {
        if (hit.collider.tag == "Player")
        {
            increaseDetection = true;
            _player = hit.collider.gameObject;
            _lastKnownPlayerPos = _player.transform.position;
            _detectionAmount = _maxDetection;
        }
        else
        {
            increaseDetection = false;
        }
    }

    void HeardSound()
    {
        Debug.Log("The guard heard that");
        NewTarget(audioTarget.position);
        //_curBehaviour = _BEHAVIOURS.Chase;
    }

    void PatrolBehaviour()
    {
        //"Main" for everything patrol related
        _spotlight.color = Color.yellow;
        _agent.speed = 1.0f;
        Patrol();
    }

    void SearchBehaviour()
    {
        //"Main" for everything search related
        _spotlight.color = new Color(1, 0.64f, 0, 1);
        _agent.speed = 0.0f;
        // rotate for a few seconds then go back to patrol
        StartCoroutine("SearchRotation");
        _curBehaviour = _BEHAVIOURS.Patrol;
        
    }

    void ChaseBehaviour()
    {
        //"Main" for everything chase related
        _spotlight.color = Color.red;
        _agent.speed = 3.0f;
        //_lastKnownPlayerPos = _player.transform.position;
        //Chase(_player.transform);
        //_startSearch = true;
        NewTarget(_lastKnownPlayerPos);
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
        // if the trigger collider belonging to the player enters and stays within the AI's trigger collider
        if (other.transform.parent == null)
        {
            return;
        }
        else
        {
            if (other.transform.parent.tag == "Player")
            {
                //Debug.Log(other.transform.parent.tag);
                // the parent of the collider is the player, so set _player to that gameobject
                GameObject player = other.transform.parent.gameObject;
                _player = player;

                // get the playerScript from the player
                Player playerScript = _player.GetComponent<Player>();

                // if the player is currently moving
                if (playerScript.isMoving)
                {
                    // set this bool to true so that the detection doesn't yet decrease
                    increaseDetection = true;
                    //start increasing by the modifier dependant on what pose the player is in
                    _detectionAmount += playerScript.suspicionRate;

                    // make sure the detection amount can't be higher than the max
                    if (_detectionAmount > _maxDetection)
                        _detectionAmount = _maxDetection;
                }
                // otherwise if the player ISN'T moving
                else if (!playerScript.isMoving)
                {
                    // set this to true so that the AI knows to start decreasing awareness
                    increaseDetection = false;
                }
            }
        }
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
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
        _startSearch = false;
    }

    void StartSearch()
    {
        if (Vector3.Distance(transform.position, _lastKnownPlayerPos) < 0.5f)
        {
            _startSearch = true;
            _foundPlayer = false;
            Debug.Log("Distance from AI to the last known player position is: " + Vector2.Distance(currentTarget.position, _lastKnownPlayerPos) + "AI needs to start searching");
            _curBehaviour = _BEHAVIOURS.Search;
        }
    }
}
