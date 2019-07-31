using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentry : MonoBehaviour
{

    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    public Transform[] targetList;
    public Transform currentTarget;
    int lastTarget;
    
    public float fovAngle = 110f;
    //Vector3 direction;
    private SphereCollider col;


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

    // Update is called once per frame
    void Update()
    {

        //agent.transform.LookAt(agent.steeringTarget);
        agent.updateRotation = true;
        
        

        if (Vector3.Distance(destination, currentTarget.position) > 1.0f)
        {
            //destination = target.position;
            destination = currentTarget.position;
            agent.destination = destination;
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (currentTarget != targetList[targetList.Length-1])
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
        //if (Vector3.Distance(destination, target.position) > 1.0f)
         
    }
    void FieldOfView()
    {
       //RaycastHit hit;
        
        //direction = other.transform.position
        //float angle = Vector3.Angle(direction, transform.forward);
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
                    }
                }
            }
        }
    }
}
