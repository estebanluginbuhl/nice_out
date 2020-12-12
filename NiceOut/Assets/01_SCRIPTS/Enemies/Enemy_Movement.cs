using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Enemy_Movement : MonoBehaviour
{
    public Switch_Mode pauser;

    public bool pathFinding;
    [Header("status bad enm = 2")]
    [Header("status allie = 1")]
    [Header("status neutral = 0")]
    int status;
    public Transform enmTransform;
    [SerializeField]
    LayerMask playerDetectionLayer;
    public float enmSpeed, targetTreshold, targetTresholdNeutral, gizmo1Radius, resetTransformPicker, maxWaitingTime;
    public Color gizmo1Color;

    [HideInInspector]
    public NavMeshAgent enmNavMesh;
    [HideInInspector]
    public Transform neutralTarget;
    [HideInInspector]
    public Transform enemyTarget;
    NavMeshHit navHit;
    GameObject player;
    GameObject choosenObjective = null;
    private int nodeIndex = 0;

    private Vector3 randomPosition;
    public bool start = true;

    private GameObject[] firmeTarget;

    private float randomTransformPickerTimer, delayBeforeGo;

    [Header("Controles")]
    //Trap Influence
    public bool isAttracted;
    public Transform attractTarget;

    public bool isSlowed, isFastened, isStunned;
    public float modifiedSpeed;

    [SerializeField]
    private TextMeshProUGUI stateText;
    void Start()
    {
        pauser = GameObject.Find("PFB_Player_Controller").GetComponent<Switch_Mode>();
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;
        //target = PathNode.nodeTransform[0];
        randomPosition = Random.insideUnitSphere * gizmo1Radius;
        status = GetComponent<Enemy_Stats>().status;
    }

    void FixedUpdate()
    {
        if(status == 2)
        {
            Collider[] PathFinderTrigger = Physics.OverlapSphere(enmTransform.position, gizmo1Radius, playerDetectionLayer);
            if (PathFinderTrigger.Length != 0)
            {
                if (player == false)
                {
                    player = PathFinderTrigger[0].gameObject;
                    pathFinding = true;
                }
            }
            else
            {
                pathFinding = false;
            }
        }
    }

    void Update()
    {
        if (start == true && status == 0)
        {
            if (neutralTarget != null)
            {
                neutralTarget.position = transform.position;
                start = false;
            }
        }
        status = GetComponent<Enemy_Stats>().status;
        switch (status)
        {
            //neutral   status 0
            #region
            case 0:
                status = 0;
                enemyTarget = null;
                if(neutralTarget != null)
                {
                    neutralTarget.position = transform.position;
                }
                ChangeStatus("Se Balade");
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
                    randomPosition = Random.insideUnitSphere * gizmo1Radius;
                    randomPosition.y = transform.position.y;

                    if (Vector3.Distance(transform.position, neutralTarget.position) <= targetTresholdNeutral)
                    {
                        delayBeforeGo = Random.Range(1, maxWaitingTime);
                        randomTransformPickerTimer = resetTransformPicker;

                        if (NavMesh.SamplePosition(transform.position + randomPosition, out navHit, gizmo1Radius, NavMesh.AllAreas))
                        {
                            neutralTarget.position = navHit.position;
                            if(neutralTarget != null)
                            {
                                enmNavMesh.destination = neutralTarget.position;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                break;
            #endregion
            //allie     status 1
            #region
            case 1:
                status = 1;
                ObjectiveSelection();
                ChangeStatus("Attaque les firmes");
                break;
            #endregion
            //bad enm   status 2
            #region
            case 2:
                status = 2;

                if (isAttracted == false)
                {
                    if (pathFinding == true)
                    {
                        ChangeStatus("Attaque le joueur");
                        enmNavMesh.destination = player.transform.position;
                    }
                    else if (pathFinding == false)
                    {
                        if (enemyTarget == null)
                        {
                            ChangeStatus("Attaque les alliés");
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
                                else
                                {
                                    return;
                                }
                            }

                            if (temporaryTarget != null)
                            {
                                enemyTarget = temporaryTarget;
                            }
                            else
                            {
                                return;
                            }


                            /*enmNavMesh.destination = target.position;

                            if (Vector3.Distance(transform.position, target.position) <= targetTreshold)
                            {
                                if (nodeIndex >= PathNode.nodeTransform.Length - 1)
                                {
                                    return;
                                }
                                nodeIndex++;
                                target = PathNode.nodeTransform[nodeIndex];
                            }*/
                        }
                        else
                        {
                            if (enemyTarget != null)
                            {
                                if(enemyTarget.GetComponent<Enemy_Stats>().status != 2)
                                {
                                    enmNavMesh.destination = enemyTarget.position;
                                }
                                else
                                {
                                    enemyTarget = null;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (attractTarget != null)
                    {
                        if (Vector3.Distance(transform.position, enemyTarget.position) <= targetTreshold)
                        {
                            enmNavMesh.destination = attractTarget.position;
                        }
                    }
                    else
                    {
                        isAttracted = false;
                        return;
                    }
                }

                if (isSlowed || isFastened)
                {
                    enmNavMesh.speed = modifiedSpeed;
                }
                else if (enmNavMesh.speed != enmSpeed)
                {
                    enmNavMesh.speed = enmSpeed;
                }
                break;
            #endregion

            default:
                return;
        }
    }

    void ObjectiveSelection()
    {
        float minDistance = Mathf.Infinity;
        firmeTarget = GameObject.FindGameObjectsWithTag("Objectives");
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
            enmNavMesh.destination = choosenObjective.transform.position;
    }

    public IEnumerator ModifieSpeed(float _ModifieTime, float _ModifiedSpeed, bool _stun)//Slow/Accélération/Stuns
    {
        isStunned = _stun;
        isSlowed = true;
        modifiedSpeed = _ModifiedSpeed;
        yield return new WaitForSecondsRealtime(_ModifieTime);
        isSlowed = false;
        isStunned = false;
    }

    void ChangeStatus(string _statusText)//A changer : mettre des points de couleur
    {
        stateText.text = _statusText;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position + Vector3.up * 2.25f, gizmo1Radius);
    }
}