﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnmMovement : MonoBehaviour
{
    public bool pathFinding;
    public bool neutral, hostile, allie;
    public Transform enmTransform;
    public LayerMask playerDetectionLayer;
    public float enmSpeed, targetTreshold, gizmo1Radius;
    public Color gizmo1Color;

    GameObject player;
    NavMeshAgent enmNavMesh;
    GameObject choosenObjective = null;
    private Transform target;
    private int nodeIndex = 0;
    private float enmHealth, damage;
    private GameObject[] firmeTarget;

    public bool isAttracted;
    public Transform attractTarget;

    void Start()
    {
        this.target = PathNode.nodeTransform[0];
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;
        enmHealth = gameObject.GetComponent<StatEnm>().enmHealth;
        damage = gameObject.GetComponent<StatEnm>().damage;
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
        //neutral
        if (neutral == true)//(enmHealth >= -2 && enmHealth <= 2)
        {
            Debug.Log("neutral entity");
        }
        //allie
        else if (allie == true)//(enmHealth > 2) //enmHealth <= max positif
        {
            Debug.Log("allie entity");
            ObjectiveSelection();
            //choosenObjective.GetComponent<FirmeScript>().TakeDamage(damage);
        }
        //bad enm
        else if (hostile == true)//(enmHealth < 2) //enmHealth >= max negatif
        {
            if(isAttracted == false)
            {
                Debug.Log("hostile entity");
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
            else
            {
                enmNavMesh.destination = attractTarget.position;
            }
        }
    }

    void ObjectiveSelection()
    {
        float minDistance = Mathf.Infinity;
        firmeTarget = GameObject.FindGameObjectsWithTag("Objectives");
        Debug.Log(firmeTarget.Length);
        for (int i = 0; i < firmeTarget.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, firmeTarget[i].transform.position);
            if (dist < minDistance)
            {
                choosenObjective = firmeTarget[i];
                minDistance = dist;
            }
        }
        enmNavMesh.destination = choosenObjective.transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position, gizmo1Radius);
    }
}