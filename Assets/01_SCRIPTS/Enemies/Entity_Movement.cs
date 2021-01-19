using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Entity_Movement : MonoBehaviour
{
    public bool pathFinding;
    [Header("status bad enm = 2")]
    [Header("status allie = 1")]
    [Header("status neutral = 0")]
    [SerializeField]
    int status;
    [SerializeField]
    LayerMask playerDetectionLayer;
    public float enmSpeed, targetTresholdEnemy, targetTresholdNeutral, targetTresholdAlly, gizmo1Radius, resetTransformPicker, minWaitingTime, maxWaitingTime;
    public Color gizmo1Color;

    [HideInInspector]
    public NavMeshAgent entityNavMesh;

    [HideInInspector]
    public Vector3 neutralTarget;
    bool hasANeutralTarget;
    [HideInInspector]
    public Transform enemyTarget;
    bool hasAnEnemyTarget;

    GameObject player;
    GameObject choosenObjective = null;
    private int nodeIndex = 0;

    private float randomTransformPickerTimer, delayBeforeGo;

    [Header("Controles")]
    //Trap Influence
    public bool isAttracted;
    public Vector3 attractTarget;

    public bool speedIsModified, isStunned;
    public float modifiedSpeed;

    [SerializeField]
    private TextMeshProUGUI stateText;
    void Start()
    {
        entityNavMesh = GetComponent<NavMeshAgent>();
        entityNavMesh.speed = enmSpeed;
        //target = PathNode.nodeTransform[0];
        //randomPosition = Random.insideUnitSphere * gizmo1Radius;
        status = GetComponent<Entity_Stats>().status;
        if(status == 0)
        {
            neutralTarget = GetNewNeutralTarget();
            hasANeutralTarget = true;
        }
    }

    void FixedUpdate()
    {
        if(status == 2)
        {
            Collider[] PathFinderTrigger = Physics.OverlapSphere(transform.position, gizmo1Radius, playerDetectionLayer);
            if (PathFinderTrigger.Length != 0)
            {
                if (player == false)
                {
                    player = PathFinderTrigger[0].gameObject;
                }
                neutralTarget = transform.position;
                pathFinding = true;
            }
            else
            {
                pathFinding = false;
            }
        }
    }

    void Update()
    {
        status = GetComponent<Entity_Stats>().status;
        switch (status)
        {
            //neutral   status 0
            #region
            case 0:
                ChangeStatus("Se Balade");
                if (hasANeutralTarget == true)
                {
                    if (Vector3.Distance(transform.position, neutralTarget) <= targetTresholdNeutral)
                    {
                        delayBeforeGo = Random.Range(minWaitingTime, maxWaitingTime);
                        hasANeutralTarget = false;
                        return;
                    }
                    else
                    {
                        entityNavMesh.destination = neutralTarget;
                    }
                }
                else
                {
                    if(delayBeforeGo > 0)
                    {
                        delayBeforeGo -= Time.deltaTime;
                    }
                    else
                    {
                        neutralTarget = GetNewNeutralTarget();
                        hasANeutralTarget = true;
                    }
                }
/*
                if (hasANeutralTarget == false)
                {
                    neutralTarget = transform.position;
                    hasANeutralTarget = true;
                }
                if(randomTransformPickerTimer > 0)
                {
                    randomTransformPickerTimer -= Time.deltaTime;
                }
                if (delayBeforeGo > 0)
                {
                    delayBeforeGo -= Time.deltaTime;
                }

                if (randomTransformPickerTimer <= 0 && delayBeforeGo <= 0)
                {

                    if (Vector3.Distance(transform.position, neutralTarget) <= targetTresholdNeutral)
                    {
                        delayBeforeGo = Random.Range(1, maxWaitingTime);
                        randomTransformPickerTimer = resetTransformPicker;
                        hasANeutralTarget = false;

                        Vector3 randomPosition = transform.position + Random.insideUnitSphere * gizmo1Radius;
                        randomPosition.y = transform.position.y;
                        if (NavMesh.SamplePosition(randomPosition, out navHit, gizmo1Radius, NavMesh.AllAreas))
                        {
                            neutralTarget = navHit.position;
                            hasANeutralTarget = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        entityNavMesh.destination = neutralTarget;
                    }

                }*/
                break;
            #endregion
            //allie     status 1
            #region
            case 1:
                ClosestFirmeSelection();
                ChangeStatus("Attaque les firmes");
                break;
            #endregion
            //bad enm   status 2
            #region
            case 2:
                if (speedIsModified)
                {
                    entityNavMesh.speed = modifiedSpeed;
                }
                else if (entityNavMesh.speed != enmSpeed)
                {
                    entityNavMesh.speed = enmSpeed;
                }
                if (isAttracted == false)
                {
                    if (pathFinding == true)
                    {
                        ChangeStatus("Attaque le joueur");
                        entityNavMesh.destination = player.transform.position;
                    }
                    else
                    {
                        if (hasAnEnemyTarget == false)
                        {

                            float minDist = Mathf.Infinity;
                            Transform temporaryTarget = null;
                            GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("enemyTarget");
                            for (int i = 0; i < possibleTargets.Length; i++)//trouve la cible la plus proche
                            {
                                float dist = Vector3.Distance(transform.position, possibleTargets[i].transform.position);
                                if (dist <= minDist)
                                {
                                    temporaryTarget = possibleTargets[i].transform;
                                    minDist = dist;
                                }
                            }
                            if (temporaryTarget != null)
                            {
                                enemyTarget = temporaryTarget;
                                hasAnEnemyTarget = true;
                                return;
                            }
                            else//Si il n'y a plus d'entité convertible, applique le deplacement aleatoire
                            {
                                if (hasANeutralTarget == true)
                                {
                                    if (Vector3.Distance(transform.position, neutralTarget) <= targetTresholdNeutral)
                                    {
                                        delayBeforeGo = Random.Range(minWaitingTime, maxWaitingTime);
                                        hasANeutralTarget = false;
                                        return;
                                    }
                                    else
                                    {
                                        ChangeStatus("Se Balade");
                                        entityNavMesh.destination = neutralTarget;
                                    }
                                }
                                else
                                {
                                    if (delayBeforeGo > 0)
                                    {
                                        delayBeforeGo -= Time.deltaTime;
                                    }
                                    else
                                    {
                                        neutralTarget = GetNewNeutralTarget();
                                        hasANeutralTarget = true;
                                    }
                                }
                                hasAnEnemyTarget = false;
                                return;
                            }
                        }
                        else
                        {
                            if(enemyTarget != null)
                            {
                                if (enemyTarget.GetComponent<Entity_Stats>().status != 2)
                                {
                                    if (Vector3.Distance(transform.position, enemyTarget.position) > targetTresholdEnemy)
                                    {
                                        ChangeStatus("Attaque les alliés");
                                        entityNavMesh.destination = enemyTarget.position;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    hasAnEnemyTarget = false;
                                    return;
                                }
                            }
                            else
                            {
                                hasAnEnemyTarget = false;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, attractTarget) > targetTresholdEnemy)
                    {
                        entityNavMesh.destination = attractTarget;
                        return;
                    }
                    else
                    {
                        entityNavMesh.destination = transform.position;
                        return;
                    }
                }
                break;
            #endregion

            default:
                return;
        }
    }

    void ClosestFirmeSelection()
    {
        float minDistance = Mathf.Infinity;
        GameObject[] firmeTarget = GameObject.FindGameObjectsWithTag("Objectives");
        for (int i = 0; i < firmeTarget.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, firmeTarget[i].transform.position);
            if (dist < minDistance)
            {
                choosenObjective = firmeTarget[i];
                minDistance = dist;
            }
        }
        if (choosenObjective != null)
            entityNavMesh.destination = choosenObjective.transform.position;
    }    
    Vector3 GetNewNeutralTarget()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * gizmo1Radius;
        NavMeshHit navHit;
        randomPosition.y = transform.position.y;
        if (NavMesh.SamplePosition(randomPosition, out navHit, gizmo1Radius, NavMesh.AllAreas))
        {
            neutralTarget = navHit.position;
            hasANeutralTarget = true;
        }
        return neutralTarget;
    }

    void NodalTargetSelection()
    {
        entityNavMesh.destination = enemyTarget.position;

        if (Vector3.Distance(transform.position, enemyTarget.position) <= targetTresholdEnemy)
        {
            if (nodeIndex >= PathNode.nodeTransform.Length - 1)
            {
                return;
            }
            nodeIndex++;
            enemyTarget = PathNode.nodeTransform[nodeIndex];
        }
    }

    public IEnumerator ModifieSpeed(float _ModifieTime, float _ModifiedSpeed, bool _stun)//Slow/Accélération/Stuns
    {
        isStunned = _stun;
        speedIsModified = true;
        modifiedSpeed = _ModifiedSpeed;
        yield return new WaitForSecondsRealtime(_ModifieTime);
        speedIsModified = false;
        isStunned = false;
    }

    void ChangeStatus(string _statusText)//A changer : mettre des points de couleur
    {
        stateText.text = _statusText;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 2.25f, gizmo1Radius);
    }
}