using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour
{

    float _detectionAmount = 0.0f;
    float _maxDetection = 100.0f;

    public Transform target;
    private Vector3 _destination;
    NavMeshAgent agent;
    private Vector3[] playerLocations;
    public Transform[] targetList;
    public Transform currentTarget;
    int lastTarget;
    public GameObject visionDetectionOrigin;
    BuildMesh meshScript;

    public float fovAngle = 110f;
    //Vector3 direction;
    private SphereCollider col;

    bool foundPlayer = false;

    GameObject player = null;

    Vector3 lastKnownPlayerPos;

    public Material passive;
    public Material alerted;

    private float _maxRange = 150.0f;
    private float _maxAngle = 45.0f;

    // might have to be vector3's
    public float radius;
    public float height;

    public Vector3 radiusVert;
    public Vector3 distanceVert;

    private Vector3 noAngle;

    public GameObject face;


    // Start is called before the first frame update
    void Start()
    {
        meshScript = GetComponentInChildren<BuildMesh>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        _destination = agent.destination;

        currentTarget = targetList[0];
        lastTarget = 0;
        col = GetComponent<SphereCollider>();
        agent.updateRotation = true;
        radius = 45.0f;
    }

    private void Update()
    {
        ViewCone();

        Debug.Log("detection amount: " + _detectionAmount);
        if (_detectionAmount >= _maxDetection)
        {
            foundPlayer = true;
        }
        else if (_detectionAmount <= 0)
        {
            foundPlayer = false;
        }

        if (foundPlayer)
        {
            Chase();
            agent.speed = 3.0f;
        }
        else if (!foundPlayer)
        {
            Patrol();
            agent.speed = 1.0f;
        }
    }

    public void Chase()
    {
        agent.destination = player.transform.position;
    }

    void Patrol()
    {
        if (Vector3.Distance(_destination, currentTarget.position) > 1.0f)
        {
            //destination = target.position;
            _destination = currentTarget.position;
            agent.destination = _destination;
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (currentTarget != targetList[targetList.Length - 1])
            {
                lastTarget++;
                currentTarget = targetList[lastTarget];
            }
            else
            {
                lastTarget = 0;
                currentTarget = targetList[lastTarget];
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

    void SeePlayer(RaycastHit hit)
    {
        if (hit.transform.tag == "Player")
        {
            player = hit.collider.gameObject;
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
    }

    public GameObject PlayerTarget
    {
        get { return player; }
        set { player = value; }
    }
}
