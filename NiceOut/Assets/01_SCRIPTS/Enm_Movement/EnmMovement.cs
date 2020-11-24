using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnmMovement : MonoBehaviour
{
    public bool pathFinding;
    public Transform enmTransform;
    public LayerMask playerDetectionLayer;
    public float enmSpeed, targetTreshold, gizmo1Radius;
    public Color gizmo1Color;

    GameObject player;
    NavMeshAgent enmNavMesh;
    private Transform target;
    private int nodeIndex = 0;

    void Start()
    {
        target = PathNode.nodeTransform[0];
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;
    }

    void FixedUpdate() 
    {
        Collider[] PathFinderTrigger = Physics.OverlapSphere(enmTransform.position, gizmo1Radius, playerDetectionLayer);
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
            enmNavMesh.destination = player.transform.position;
        }
        else if (pathFinding == false)
        {
            enmNavMesh.destination = target.position;

            if (Vector3.Distance(transform.position, target.position) <= targetTreshold)
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
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position, gizmo1Radius);
    }
}