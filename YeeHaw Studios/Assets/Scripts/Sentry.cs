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
        // Update where the viewcone raycasts should be shot from
        //ViewCone();

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
        }
        else if (_detectionAmount < _maxDetection)
        {
            _foundPlayer = false;
        }

        if (_foundPlayer)
        {
            Chase(_player.transform);
            _agent.speed = 3.0f;
        }
        else if (!_foundPlayer)
        {
            Patrol();
            _agent.speed = 1.0f;
        }
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

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Vector3 direction = other.transform.position - transform.position;
    //        float angle = Vector3.Angle(direction, transform.forward);

    //        if (angle < fovAngle * 0.5f)
    //        {
    //            RaycastHit hit;

    //            if (Physics.Raycast(transform.position/* + transform.up*/, direction.normalized, out hit, col.radius))
    //            {
    //                if (hit.collider.gameObject.tag == "Player")
    //                {
    //                    Debug.Log("FOUND THE PLAYER");
    //                    foundPlayer = true;
    //                    player = hit.collider.gameObject;
    //                    GetComponent<Renderer>().material = alerted;
    //                }
    //                else
    //                {
    //                    foundPlayer = false;
    //                    GetComponent<Renderer>().material = passive;
    //                }
    //            }
    //            else
    //            {
    //                foundPlayer = false;
    //                GetComponent<Renderer>().material = passive;
    //            }
    //        }
    //    }
    //}


    //private void ViewCone()
    //{
    //    // get the forward direction and multiply by how long you want the raycasts to be
    //    _noAngle = transform.forward * 10;

    //    // each of these are the calculated angles from the (North East South West) corners of the generated mesh 
    //    Quaternion rightAngle = Quaternion.AngleAxis(_meshScript.vertices[3].x * 4, Vector3.up);
    //    Quaternion leftAngle = Quaternion.AngleAxis(_meshScript.vertices[7].x * 4, Vector3.up);
    //    Quaternion downAngle = Quaternion.AngleAxis(_meshScript.vertices[5].y * 4, Vector3.right);
    //    Quaternion topAngle = Quaternion.AngleAxis(_meshScript.vertices[1].y * 4, Vector3.right);

    //    // The angle is then multiplied by the forward direction so that it points in front of the AI
    //    Vector3 newRightAngle = rightAngle * _noAngle;
    //    Vector3 newLeftAngle = leftAngle * _noAngle;
    //    Vector3 newBottomAngle = downAngle * _noAngle;
    //    Vector3 newTopAngle = topAngle * _noAngle;

    //    //Debug.Log("Right raycast angle/direction: " +newRightAngle);
    //    //Debug.Log("Left raycast angle/direction: " + newLeftAngle);
    //    //Debug.Log("Bottom raycast angle/direction: " + newBottomAngle);
    //    Debug.Log("Top raycast x: " + newTopAngle.x);
    //    Debug.Log("Top raycast y: " + newTopAngle.y);

    //    float radius = 3;
    //    float height = _maxRange;
    //    float surface_area;
    //    float volume;
    //    surface_area = Mathf.PI * radius * (radius * Mathf.Sqrt(radius * radius + height * height));
    //    volume = (1.0f / 3) * Mathf.PI * radius * radius * height;
    //    //Debug.Log("Surface Area of cone is : " + surface_area);
    //    //Debug.Log("Volume of Cone is : " + volume);

    //    Debug.DrawRay(visionDetectionOrigin.transform.position, newRightAngle, Color.yellow);
    //    Debug.DrawRay(visionDetectionOrigin.transform.position, newLeftAngle, Color.yellow);
    //    Debug.DrawRay(visionDetectionOrigin.transform.position, newBottomAngle, Color.green);
    //    Debug.DrawRay(visionDetectionOrigin.transform.position, newTopAngle, Color.green);

    //    Debug.DrawRay(visionDetectionOrigin.transform.position, _noAngle, Color.yellow);



    //    RaycastHit hit;

    //    if (Physics.Raycast(visionDetectionOrigin.transform.position, newRightAngle, out hit, _maxRange))
    //    {
    //        SeePlayer(hit);
    //    }
    //    else if (Physics.Raycast(visionDetectionOrigin.transform.position, newLeftAngle, out hit, _maxRange))
    //    {
    //        SeePlayer(hit);
    //    }
    //    else if (Physics.Raycast(visionDetectionOrigin.transform.position, newBottomAngle, out hit, _maxRange))
    //    {
    //        SeePlayer(hit);
    //    }
    //    else if (Physics.Raycast(visionDetectionOrigin.transform.position, newTopAngle, out hit, _maxRange))
    //    {
    //        SeePlayer(hit);
    //    }
    //    else if (Physics.Raycast(visionDetectionOrigin.transform.position, _noAngle, out hit, _maxRange))
    //    {
    //        SeePlayer(hit);
    //    }

    //}


    void SeePlayer(RaycastHit hit)
    {
        if (hit.collider.tag == "Player")
        {
            increaseDetection = true;
            _player = hit.collider.gameObject;
            //_detectionAmoun++;
            _detectionAmount += _maxDetection * 0.5f;
        }
        else
        {
            increaseDetection = false;
            //_detectionAmount--;

            //if (_detectionAmount <= 0)
            //    _detectionAmount = 0;
        }
    }

    void HeardSound()
    {
        Debug.Log("The guard heard that");
        Chase(audioTarget);
    }

    public GameObject PlayerTarget
    {
        get { return _player; }
        set { _player = value; }
    }

    public float DetectionAmount
    {
        get { return _detectionAmount; }
    }

    public float MaxDetectionAmount
    {
        get { return _maxDetection; }
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
