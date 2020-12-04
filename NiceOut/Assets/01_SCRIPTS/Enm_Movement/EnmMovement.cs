using System.Collections;
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

    //Trap Influence
    public bool isAttracted;
    public Transform attractTarget;

    public bool isSlowed, isFastened, isStunned, hasDot;
    public float modifiedSpeed;

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
            gameObject.layer = 0;

            Debug.Log("neutral entity");
        }
        //allie
        else if (allie == true)//(enmHealth > 2) //enmHealth <= max positif
        {
            gameObject.layer = 0;

            Debug.Log("allie entity");
            ObjectiveSelection();
            //choosenObjective.GetComponent<FirmeScript>().TakeDamage(damage);
        }
        //bad enm
        else if (hostile == true)//(enmHealth < 2) //enmHealth >= max negatif
        {
            gameObject.layer = 13;

            if (isAttracted == false)
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
                if (attractTarget != null)
                {
                    if (Vector3.Distance(transform.position, target.position) <= targetTreshold)
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

            if(isSlowed || isFastened)
            {
                enmNavMesh.speed = modifiedSpeed;
                Debug.Log("modified");
            }
            else if(enmNavMesh.speed != enmSpeed)
            {
                enmNavMesh.speed = enmSpeed;
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

    public IEnumerator ModifieSpeed(float _ModifieTime,float _ModifiedSpeed, bool _stun)//Slow/Accélération/Stuns
    {
        isStunned = _stun;
        isSlowed = true;
        modifiedSpeed = _ModifiedSpeed;
        yield return new WaitForSecondsRealtime(_ModifieTime);
        isSlowed = false;
        isStunned = false;
    }

    public IEnumerator DamagesOverTime(int _damage, int _duration, float _range, int _index)//DOT parfum
    {
        if(_index != 0)
        {
            hasDot = true;
            while(_duration > 0)
            {
                if (hostile == true)
                {
                    float minDist = Mathf.Infinity;
                    GameObject target = null;

                    Collider[] transferTarget = Physics.OverlapSphere(transform.position, _range, 13);
                    foreach (Collider c in transferTarget)
                    {
                        float dist = Vector3.Distance(transform.position, c.transform.position);
                        if (dist <= minDist)
                        {
                            target = c.gameObject;
                            minDist = dist;
                        }
                    }
                    if (target != null)
                    {
                        _index -= 1;
                        target.GetComponent<EnmMovement>().StartCoroutine(DamagesOverTime(_damage, _duration, _range, _index));
                    }

                    yield return new WaitForSecondsRealtime(1);
                    _duration -= 1;
                    GetComponent<StatEnm>().badEnm(_damage);
                }
                else
                {
                    _duration = 0;
                }
            }
            if(_duration <= 0)
            {
                hasDot = false;
            }
        }
        else
        {
            hasDot = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position, gizmo1Radius);
    }
}