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
        //ViewCone();

        if (!increaseDetection)
        {
            _detectionAmount--;
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

    /*
    private void ViewCone()
    {
        noAngle = transform.forward * 10;
        Quaternion rightAngle = Quaternion.AngleAxis(meshScript.vertices[3].x * 4, new Vector3(0, 1, 0));
        Quaternion leftAngle = Quaternion.AngleAxis(meshScript.vertices[7].x * 4, new Vector3(0, 1, 0));
        Quaternion downAngle = Quaternion.AngleAxis(meshScript.vertices[1].y * 4, new Vector3(1, 0, 0));
        Quaternion topAngle = Quaternion.AngleAxis(meshScript.vertices[5].y * 4, new Vector3(1, 0, 0));

        Vector3 newAngle1 = rightAngle * noAngle;
        Vector3 newAngle2 = leftAngle * noAngle;
        Vector3 newAngle3 = downAngle * noAngle;
        Vector3 newAngle4 = topAngle * noAngle;

        //float radius = 3;
        //float height = _maxRange;
        float surface_area;
        float volume;
        surface_area = Mathf.PI * radius * (radius * Mathf.Sqrt(radius * radius + height * height));
        volume = (1.0f / 3) * Mathf.PI * radius * radius * height;
        //Debug.Log("Surface Area of cone is : " + surface_area);
        //Debug.Log("Volume of Cone is : " + volume);

        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, (transform.position.z + _maxRange)), new Color(0.5f, 0.0f, 0.5f), 1.0f);
        Debug.DrawRay(visionDetectionOrigin.transform.position, newAngle1, Color.yellow);
        Debug.DrawRay(visionDetectionOrigin.transform.position, newAngle2, Color.yellow);
        Debug.DrawRay(visionDetectionOrigin.transform.position, newAngle3, Color.yellow);
        Debug.DrawRay(visionDetectionOrigin.transform.position, newAngle4, Color.yellow);

        Debug.DrawRay(visionDetectionOrigin.transform.position, noAngle, Color.yellow);



        RaycastHit hit;

        if (Physics.Raycast(visionDetectionOrigin.transform.position, newAngle1, out hit, _maxRange))
        {
            SeePlayer(hit);
        }
        else if (Physics.Raycast(visionDetectionOrigin.transform.position, newAngle2, out hit, _maxRange))
        {
            SeePlayer(hit);
        }
        else if (Physics.Raycast(visionDetectionOrigin.transform.position, newAngle3, out hit, _maxRange))
        {
            SeePlayer(hit);
        }
        else if (Physics.Raycast(visionDetectionOrigin.transform.position, newAngle4, out hit, _maxRange))
        {
            SeePlayer(hit);
        }
        else if (Physics.Raycast(visionDetectionOrigin.transform.position, noAngle, out hit, _maxRange))
        {
            SeePlayer(hit);
        }
        
    }
    */

    void SeePlayer(RaycastHit hit)
    {
        if (hit.transform.tag == "Player")
        {
            _player = hit.collider.gameObject;
            _detectionAmount++;
        }
        else
        {
            _detectionAmount--;

            if (_detectionAmount <= 0)
                _detectionAmount = 0;
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
        if (other.transform.tag == "Player")
        {
            GameObject player = other.gameObject;

            //if(player == null)
                _player = player;

            Player playerScript = player.GetComponent<Player>();
            if (playerScript.isMoving)
            {
                increaseDetection = true;
                _detectionAmount += playerScript.suspicionRate;

                if (_detectionAmount > _maxDetection)
                    _detectionAmount = _maxDetection;

                // increase suspicion based on the current suspicion rate
            }
            else
            {
                increaseDetection = false;
                //_detectionAmount--;
                //if (_detectionAmount < 0)
                //    _detectionAmount = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            increaseDetection = false;
            //_detectionAmount--;
            //if (_detectionAmount < 0)
            //    _detectionAmount = 0;
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        GameObject player = collision.gameObject;
    //        Player playerScript = player.GetComponent<Player>();
    //        if (playerScript.isMoving)
    //        {
    //            _detectionAmount += playerScript.suspicionRate;
    //            if (_detectionAmount > _maxDetection)
    //                _detectionAmount = _maxDetection;

    //            // increase suspicion based on the current suspicion rate
    //        }
    //        else
    //        {
    //            _detectionAmount--;
    //            if (_detectionAmount < 0)
    //                _detectionAmount = 0;
    //        }
    //    }
    //}
}
