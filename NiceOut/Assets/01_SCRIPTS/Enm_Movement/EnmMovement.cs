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
    public float enmSpeed, targetTreshold, targetTresholdNeutral, gizmo1Radius, resetTransformPicker, resetWaitingTime;
    public Color gizmo1Color;

    GameObject player;
    NavMeshAgent enmNavMesh;
    GameObject choosenObjective = null;
    private Transform target;
    private int nodeIndex = 0;
    [SerializeField]
    private float enmHealth, damage;
    Vector2 healthValues = new Vector2(100, 10);
    private GameObject[] firmeTarget;

    NavMeshPath navMeshPath;

    private float Xmax, Xmin, Zmin, Zmax, randomTransformPickerTimer, Xpos, Zpos, delayBeforeGo;

    //Trap Influence
    public bool isAttracted;
    public Transform attractTarget;

    public bool isSlowed, isFastened, isStunned, hasDot;
    public float modifiedSpeed;
    [SerializeField]
    ParticleSystem convertBadEnemyParticle, convertGoodEnemyParticle, convertedToGood, convertedToBad, parfumed;
    [SerializeField]
    Material[] mats = new Material[3];

    void Start()
    {
        target = PathNode.nodeTransform[0];
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;
        damage = gameObject.GetComponent<StatEnm>().damage;

        randomTransformPickerTimer = resetTransformPicker;

        Xmax = transform.position.x + (gizmo1Radius * 2);
        Xmin = transform.position.x - (gizmo1Radius * 2);
        Zmax = transform.position.z + (gizmo1Radius * 2);
        Zmin = transform.position.z - (gizmo1Radius * 2);
        Xpos = Random.Range(Xmin, Xmax);
        Zpos = Random.Range(Zmin, Zmax);
        target.position = new Vector3(Xpos, target.position.y, Zpos);
        enmNavMesh.destination = target.position;

        delayBeforeGo = 0;
        UpdateEnemyState();
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
        if (neutral)//neutral == true
        {
            gameObject.layer = 13;

            randomTransformPickerTimer -= Time.deltaTime;
            delayBeforeGo -= Time.deltaTime;
            if (randomTransformPickerTimer <= 0 && Vector3.Distance(transform.position, target.position) <= targetTresholdNeutral && delayBeforeGo <= 0)
            {
                Xmax = transform.position.x + (gizmo1Radius * 2);
                Xmin = transform.position.x - (gizmo1Radius * 2);
                Zmax = transform.position.z + (gizmo1Radius * 2);
                Zmin = transform.position.z - (gizmo1Radius * 2);

                Xpos = Random.Range(Xmin, Xmax);
                Zpos = Random.Range(Zmin, Zmax);

                target.position = new Vector3(Xpos, target.position.y, Zpos);
                enmNavMesh.destination = target.position;
                navMeshPath = new NavMeshPath();

                delayBeforeGo = resetWaitingTime;

                if (delayBeforeGo <= 0 && CalculateNewPath() == true)
                {
                    randomTransformPickerTimer = resetTransformPicker;
                    Debug.Log("calculate path true");
                }
                else if (CalculateNewPath() == false)
                {
                    Debug.Log("calculate path false");
                }
            }
        }
        //allie
        else if (allie)//allie == true
        {
            gameObject.layer = 0;
            ObjectiveSelection();
            //choosenObjective.GetComponent<FirmeScript>().TakeDamage(damage);
        }
        //bad enm
        else if (hostile)//hostile == true
        {
            gameObject.layer = 13;

            if (isAttracted == false)
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
        if(choosenObjective != null)
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
        Debug.Log(_index);
        if(_index != 0)
        {
            parfumed.Play();
            hasDot = true;
            while(_duration > 0)
            {
                if (hostile == true || neutral == true)
                {
                    float minDist = Mathf.Infinity;
                    GameObject target = null;

                    Collider[] transferTarget = Physics.OverlapSphere(transform.position + Vector3.up, _range, 13);
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
                        _duration -= 1;
                        _damage -= 1;

                        if(_damage < 1)
                        {
                            damage = 1;
                        }
                        if(_duration < 1)
                        {
                            _duration = 1;
                        }
                        target.GetComponent<EnmMovement>().StartCoroutine(DamagesOverTime(_damage, _duration, _range, _index));
                    }

                    yield return new WaitForSecondsRealtime(1);
                    _duration -= 1;
                    DamageBadEnemy(_damage);
                }
                else
                {
                    _duration = 0;
                }
            }
            if(_duration <= 0)
            {
                hasDot = false;
                parfumed.Stop();
            }
        }
        else
        {
            hasDot = false;
            parfumed.Stop();
        }
    }

    public void DamageGoodEnemy(int takenDamage)
    {
        enmHealth -= takenDamage;
        if(enmHealth <= -healthValues.x)
        {
            enmHealth = -healthValues.x;
        }
        convertGoodEnemyParticle.Play();
        UpdateEnemyState();
    }

    public void DamageBadEnemy(int takenDamage)
    {
        enmHealth += takenDamage;
        if (enmHealth >= healthValues.x)
        {
            enmHealth = healthValues.x;
        }
        convertBadEnemyParticle.Play();
        UpdateEnemyState();
    }

    void UpdateEnemyState()
    {
        if(enmHealth < -healthValues.y)
        {
            if(hostile == false) 
            {
                hostile = true;
                Debug.Log(GetComponentInChildren<MeshRenderer>().material);
                GetComponentInChildren<MeshRenderer>().material = mats[0];
                convertedToBad.Play();
                neutral = false;
                allie = false;
            }
        }
        else if (enmHealth > healthValues.y)
        {
            if (allie == false)
            {
                allie = true;
                GetComponentInChildren<MeshRenderer>().material = mats[2];
                convertedToGood.Play();
                neutral = false;
                hostile = false;
            }
        }
        else
        {
            if(neutral == false)
            {
                neutral = true;
                GetComponentInChildren<MeshRenderer>().material = mats[1];
                allie = false;
                hostile = false;
            }
        }
    }

    bool CalculateNewPath()
    {
        enmNavMesh.CalculatePath(target.position, navMeshPath);
        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position, gizmo1Radius);
        Gizmos.DrawWireSphere(enmTransform.position + Vector3.up, 2);
    }
}