using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour
{

    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    private Vector3[] playerLocations;
    public Transform[] targetList;
    public Transform currentTarget;
    int lastTarget;
    
    public float fovAngle = 110f;
    //Vector3 direction;
    private SphereCollider col;

    bool foundPlayer = false;

    GameObject player = null;

    Vector3 lastKnownPlayerPos;

    public Material passive;
    public Material alerted;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destination = agent.destination;

        currentTarget = targetList[0];
        lastTarget = 0;
        col = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        agent.updateRotation = true;

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
        if (Vector3.Distance(destination, currentTarget.position) > 1.0f)
        {
            //destination = target.position;
            destination = currentTarget.position;
            agent.destination = destination;
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fovAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position/* + transform.up*/, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.Log("FOUND THE PLAYER");
                        foundPlayer = true;
                        player = hit.collider.gameObject;
                        GetComponent<Renderer>().material = alerted;
                    }
                    else
                    {
                        foundPlayer = false;
                        GetComponent<Renderer>().material = passive;
                    }
                }
                else
                {
                    foundPlayer = false;
                    GetComponent<Renderer>().material = passive;
                }
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //agent.transform.LookAt(agent.steeringTarget);
    //    agent.updateRotation = true;


    //    if (!foundPlayer)
    //    {
    //        if (Vector3.Distance(destination, currentTarget.position) > 1.0f)
    //        {
    //            //destination = target.position;
    //            destination = currentTarget.position;
    //            agent.destination = destination;
    //        }
    //        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
    //        {
    //            if (currentTarget != targetList[targetList.Length - 1])
    //            {
    //                lastTarget++;
    //                currentTarget = targetList[lastTarget];
    //            }
    //            else
    //            {
    //                lastTarget = 0;
    //                currentTarget = targetList[lastTarget];
    //            }
    //        }
    //    }
    //    else if (foundPlayer)
    //    {
    //        if (Vector3.Distance(destination, player.transform.position) < col.radius)
    //        {
    //            destination = player.transform.position;
    //            agent.destination = destination;
    //            lastKnownPlayerPos = player.transform.position;
    //        }
    //        else
    //        {
    //            destination = lastKnownPlayerPos;

    //            if(!agent.pathPending && agent.remainingDistance < 0.1f)
    //                foundPlayer = false;
    //            //if(transform.position == lastKnownPlayerPos)
    //        }
    //    }
    //    //if (Vector3.Distance(destination, target.position) > 1.0f)

    //}
    //void FieldOfView()
    //{
    //   //RaycastHit hit;

    //    //direction = other.transform.position
    //    //float angle = Vector3.Angle(direction, transform.forward);
    //}
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
    //                }
    //                else
    //                    foundPlayer = false;
    //            }
    //        }
    //    }
    //}
}
