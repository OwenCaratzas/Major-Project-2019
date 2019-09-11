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

    // the different behaviour states
    private enum _BEHAVIOURS { Patrol, Search, Chase};

    // current behaviour
    private _BEHAVIOURS _curBehaviour;
    #endregion

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
            _detectionAmount -= _maxDetection * 0.005f;
            if (_detectionAmount < 0)
                _detectionAmount = 0;
        }

        //Debug.Log("detection amount: " + _detectionAmount);
        if (_detectionAmount >= _maxDetection)
        {
            _foundPlayer = true;
            _curBehaviour = _BEHAVIOURS.Chase;
        }
        else if (_detectionAmount < _maxDetection)
        {
            _foundPlayer = false;
            _curBehaviour = _BEHAVIOURS.Search;
        }
        else if (_detectionAmount <= 0)
        {
            _foundPlayer = false;
            _curBehaviour = _BEHAVIOURS.Patrol;
        }

        //if (_foundPlayer)
        //{
        //    Chase(_player.transform);
        //    _agent.speed = 3.0f;
        //}
        //else if (!_foundPlayer)
        //{
        //    Patrol();
        //    _agent.speed = 1.0f;
        //}
    }

    public void Chase(Transform targetPosition)
    {
        _agent.destination = targetPosition.position;
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
        Chase(audioTarget);
        _curBehaviour = _BEHAVIOURS.Chase;
    }

    void PatrolBehaviour()
    {
        //"Main" for everything patrol related
        _agent.speed = 1.0f;
        Patrol();
    }

    void SearchBehaviour()
    {
        //"Main" for everything search related
        _agent.speed = 2.0f;
    }

    void ChaseBehaviour()
    {
        //"Main" for everything chase related
        _agent.speed = 3.0f;
        Chase(_player.transform);
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
}
