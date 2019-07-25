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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destination = agent.destination;

        currentTarget = targetList[0];
        lastTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
}
