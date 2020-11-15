using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnmMovement : MonoBehaviour
{
    public bool pathFinding;
    public Transform enmTransform;
    public LayerMask playerDetectionLayer;
    public float enmSpeed, gizmosRadius;
    public Color gizmosColor;

    GameObject player;
    private Transform target;
    private int nodeIndex = 0;

    private void Start()
    {
        target = PathNode.nodeTransform[0];
        enmTransform = gameObject.transform;
    }

    void FixedUpdate()
    {
        Collider[] PathFinderTrigger = Physics.OverlapSphere(enmTransform.position, gizmosRadius, playerDetectionLayer);
        if (PathFinderTrigger.Length != 0)
        {
            player = PathFinderTrigger[0].gameObject;
            pathFinding = true;
        }
        else
        {
            pathFinding = false;
        }
    }

    void Update()
    {
        if (pathFinding == true)
        {
            gameObject.GetComponent<PathNode>().enabled = false;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = player.transform.position - transform.position;
            Debug.Log("trigger in");
        }
        else if (pathFinding == false)
        {
            gameObject.GetComponent<PathNode>().enabled = true;
            gameObject.GetComponent<PathFinder>().enabled = false;

            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enmSpeed * Time.deltaTime, Space.World);
            Debug.Log("trigger off");

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

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(enmTransform.position, gizmosRadius);
    }
}