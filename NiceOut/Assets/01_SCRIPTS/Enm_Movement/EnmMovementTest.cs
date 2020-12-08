using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnmMovementTest : MonoBehaviour
{
    Switch_Mode pauser;

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
    private float enmHealth;
    [SerializeField]
    private int damage;
    Vector4 healthValues = new Vector4(0, 45, 55, 100);
    private GameObject[] firmeTarget;

    NavMeshPath navMeshPath;

    private float Xmax, Xmin, Zmin, Zmax, randomTransformPickerTimer, Xpos, Zpos, delayBeforeGo;

    public float attackRadius, damageCooldown;
    public Color attackRadiusDebugColor;
    private GameObject attackTarget;

    //Trap Influence
    public bool isAttracted;
    public Transform attractTarget;

    public bool isSlowed, isFastened, isStunned, hasDot;
    public float modifiedSpeed;

    [Header("Affichage")]
    //Visuel
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Gradient healthColor;
    [SerializeField]
    private TextMeshProUGUI stateText;
    [SerializeField]
    ParticleSystem convertBadEnemyParticle, convertGoodEnemyParticle, convertedToGood, convertedToBad, parfumed;
    [SerializeField]
    Material[] mats = new Material[3];
    float healthPercentage;

    void Start()
    {
        //pauser = player.GetComponent<Switch_Mode>();
        target = PathNode.nodeTransform[0];
        enmTransform = gameObject.transform;
        enmNavMesh = GetComponent<NavMeshAgent>();
        enmNavMesh.speed = enmSpeed;

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

        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
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
        /*if(pauser.realPause == true)
        {
            
        }*/
        //neutral
        if (neutral)//neutral == true
        {
            gameObject.layer = 13;
            ChangeStatus("Se Balade");
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
            ChangeStatus("Attaque les firmes");
            //choosenObjective.GetComponent<FirmeScript>().TakeDamage(damage);
        }
        //bad enm
        else if (hostile)//hostile == true
        {
            gameObject.layer = 13;

            if (damageCooldown < 1)
            {
                damageCooldown += Time.deltaTime;
            }

            if (isAttracted == false)
            {
                if (pathFinding == true)
                {
                    enmNavMesh.destination = player.transform.position;
                    Collider[] playerTarget = Physics.OverlapSphere(transform.position, attackRadius, playerDetectionLayer);

                    if (playerTarget.Length == 0)
                    {
                        return;
                    }
                    else
                    {
                        foreach (Collider c in playerTarget)
                        {
                            attackTarget = c.gameObject;
                        }
                        if (damageCooldown >= 1)
                        {
                            if (attackTarget != false)
                            {
                                Attack(attackTarget);
                            }
                        }
                    }
                    ChangeStatus("Attaque le joueur");
                }
                else if (pathFinding == false)
                {
                    ChangeStatus("Attaque les alliés");
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

            if (isSlowed || isFastened)
            {
                enmNavMesh.speed = modifiedSpeed;
            }
            else if (enmNavMesh.speed != enmSpeed)
            {
                enmNavMesh.speed = enmSpeed;
            }
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

    public IEnumerator DamagesOverTime(int _damage, int _duration, float _range, int _index)//DOT parfum
    {
        Debug.Log(_index);
        if (_index != 0)
        {
            parfumed.Play();
            hasDot = true;
            if (_duration > 0)
            {
                if (hostile == true || neutral == true)
                {
                    float minDist = Mathf.Infinity;
                    GameObject target = null;

                    Collider[] transferTarget = Physics.OverlapSphere(transform.position + Vector3.up, _range, 13);
                    foreach (Collider c in transferTarget)
                    {
                        if (c.GetComponent<EnmMovement>().hasDot == false)
                        {
                            float dist = Vector3.Distance(transform.position, c.transform.position);
                            if (dist <= minDist)
                            {
                                target = c.gameObject;
                                minDist = dist;
                            }
                        }

                    }
                    if (target != null)
                    {
                        _index -= 1;
                        _duration -= 1;
                        _damage -= 1;
                        if (_damage < 1)
                        {
                            _damage = 1;
                        }
                        if (_duration < 1)
                        {
                            _duration = 1;
                        }
                        if (_index < 1)
                        {
                            _duration = 0;
                        }
                        target.GetComponent<EnmMovement>().StartCoroutine(DamagesOverTime(_damage, _duration, _range, _index));
                        target = null;
                    }

                    yield return new WaitForSecondsRealtime(1);
                    DamageBadEnemy(_damage);
                    _duration -= 1;
                }
                else
                {
                    _duration = 0;
                }
                Debug.Log(_duration);
            }
            else
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
        if (enmHealth <= healthValues.x)
        {
            enmHealth = healthValues.x;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertGoodEnemyParticle.Play();
        UpdateEnemyState();
    }

    public void DamageBadEnemy(int takenDamage)
    {
        enmHealth += takenDamage;
        if (enmHealth >= healthValues.w)
        {
            enmHealth = healthValues.w;
        }
        healthPercentage = enmHealth / healthValues.w;
        healthImage.color = healthColor.Evaluate(healthPercentage);
        healthImage.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
        convertBadEnemyParticle.Play();
        UpdateEnemyState();
    }

    void Attack(GameObject _target)
    {
        if (_target != null && GetComponent<EnmMovement>().hostile == true)
        {
            _target.GetComponent<StatsPlayer>().DamagePlayer(damage);
            damageCooldown = 0;
        }
        else return;
    }

    void UpdateEnemyState()
    {
        if (enmHealth < healthValues.y)
        {
            if (hostile == false)
            {
                hostile = true;
                GetComponentInChildren<MeshRenderer>().material = mats[0];
                convertedToBad.Play();
                neutral = false;
                allie = false;
            }
        }
        else if (enmHealth > healthValues.z)
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
        else if (enmHealth >= healthValues.y && enmHealth <= healthValues.z)
        {
            if (neutral == false)
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

    void ChangeStatus(string _statusText)
    {
        stateText.text = _statusText;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmo1Color;
        Gizmos.DrawWireSphere(enmTransform.position, gizmo1Radius);
        Gizmos.DrawWireSphere(enmTransform.position + Vector3.up, 2);

        Gizmos.color = attackRadiusDebugColor;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}