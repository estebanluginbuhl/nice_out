using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnmMovement : MonoBehaviour
{
    public float enmSpeed;
    public bool pathFinding;

    private Transform target;
    private int nodeIndex = 0;

    private void Start()
    {
        target = PathNode.nodeTransform[0];
    }

    void Update()
    {
        if (pathFinding == true)
        {
            gameObject.GetComponent<PathNode>().enabled = false;
            //NavMeshAgent agent = GetComponent<NavMeshAgent>();
            //agent.destination = target.position - transform.position;
        }
        else if (pathFinding == false)
        {
            gameObject.GetComponent<PathNode>().enabled = true;
            gameObject.GetComponent<PathFinder>().enabled = false;

            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enmSpeed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.5f)
            {
                if (nodeIndex >= PathNode.nodeTransform.Length - 1)
                {
                    Destroy(gameObject);
                    return;
                }

                nodeIndex++;
                target = PathNode.nodeTransform[nodeIndex];
            }
        }
        else
        {
            return;
        }
    }

    //define if the enemy use the Navmesh system or not
    public void OnTriggerEnter(Collider PathFinderTriggerIn)
    {
        if (PathFinderTriggerIn.tag == "Player")
        {
            pathFinding = true;
            Debug.Log("trigger in");
        }
    }

    public void OnTriggerExit(Collider PathFinderTriggerOut)
    {
        if (PathFinderTriggerOut.tag == "Player")
        {
            pathFinding = false;
            Debug.Log("trigger off");
        }
    }
}